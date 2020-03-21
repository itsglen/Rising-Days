using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // The object that the NPC is trying to go towards
    private GameObject targetObject = null;
    private MachineI targetObjectScript = null;
    private bool canBeginPathing = false;
    private bool isPathing = false;
    private bool moving = false;
    private bool doneMovingX = false;
    private bool doneMovingY = false;
    private bool pathingComplete = false;
    private bool interacted = false;
    //private GameObject targetNode = null;
    private List<Vector3> traversed = new List<Vector3>();

    private float defaultSpeed = 0.025f;

    private float dir_x;
    private float dir_y;

    private string TILE = "Tile";

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = true;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        /*
        if (targetObject != null && canBeginPathing && !isPathing) { BeginPathing(); }
        if (pathingComplete && !interacted) { InteractWithObject(); }
        if (moving) { MoveNPC(); }
        */
    }

    void Update()
    {
        HandleMovementAnimations();
        spriteRenderer.sortingOrder = Mathf.RoundToInt((transform.position.y - 0.5f) * 100f) * -1;

        if (Input.GetKeyDown("o"))
        {
            canBeginPathing = true;
        }

        if (Input.GetKeyDown("m"))
        {
            DrawWalkableArea();
        }


        if (!targetObject && GameManager.instance.storeObjects.Count > 0 && !isPathing && canBeginPathing)
        {
            targetObject = GameManager.instance.storeObjects[0];
            targetObjectScript = targetObject.GetComponent<MachineI>();
            FindPath();
        }
    }

    private void DrawWalkableArea()
    {
        foreach (GameObject tile in GameManager.instance.tiles)
        {
            if (tile.GetComponent<Tile>().objectOnTile == null)
            {
                tile.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                tile.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    private void FindPath()
    {
        isPathing = true;

        Node startNode = GetStartingNode();
        Node targetNode = GetTargetNode();

        startNode.tile.GetComponent<SpriteRenderer>().color = Color.blue;
        targetNode.tile.GetComponent<SpriteRenderer>().color = Color.blue;

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                // RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

    }

    private List<Node> GetNeighbours(Node node)
    {
        List<Node> nodes = new List<Node>();
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        Vector3 upPosition = new Vector3(node.tile.transform.position.x, node.tile.transform.position.y + 0.5f, node.tile.transform.position.z);
        Vector3 downPosition = new Vector3(node.tile.transform.position.x, node.tile.transform.position.y - 0.5f, node.tile.transform.position.z);
        Vector3 leftPosition = new Vector3(node.tile.transform.position.x - 0.5f, node.tile.transform.position.y, node.tile.transform.position.z);
        Vector3 rightPosition = new Vector3(node.tile.transform.position.x + 0.5f, node.tile.transform.position.y, node.tile.transform.position.z);
        Vector3 upRightDiagonalPosition = new Vector3(node.tile.transform.position.x + 0.5f, node.tile.transform.position.y + 0.5f, node.tile.transform.position.z);
        Vector3 upLeftDiagonalPosition = new Vector3(node.tile.transform.position.x - 0.5f, node.tile.transform.position.y + 0.5f, node.tile.transform.position.z);
        Vector3 downRightDiagonalPosition = new Vector3(node.tile.transform.position.x + 0.5f, node.tile.transform.position.y - 0.5f, node.tile.transform.position.z);
        Vector3 downLeftDiagonalPosition = new Vector3(node.tile.transform.position.x - 0.5f, node.tile.transform.position.y - 0.5f, node.tile.transform.position.z);

        hits.AddRange(Physics2D.RaycastAll(upPosition, Vector2.zero));
        hits.AddRange(Physics2D.RaycastAll(leftPosition, Vector2.zero));
        hits.AddRange(Physics2D.RaycastAll(rightPosition, Vector2.zero));
        hits.AddRange(Physics2D.RaycastAll(upRightDiagonalPosition, Vector2.zero));
        hits.AddRange(Physics2D.RaycastAll(upLeftDiagonalPosition, Vector2.zero));
        hits.AddRange(Physics2D.RaycastAll(downPosition, Vector2.zero));
        hits.AddRange(Physics2D.RaycastAll(downRightDiagonalPosition, Vector2.zero));
        hits.AddRange(Physics2D.RaycastAll(downLeftDiagonalPosition, Vector2.zero));

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.tag == TILE)
            {
                hit.transform.gameObject.GetComponent<SpriteRenderer>().color = hit.transform.gameObject.GetComponent<SpriteRenderer>().color == Color.green ? Color.green : Color.yellow;
                nodes.Add(new Node(hit.transform.gameObject));
            }
        }

        return nodes;
    }

    private void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode.tile.transform.position != new Vector3(startNode.tile.transform.position.x, startNode.tile.transform.position.y - 0.5f, startNode.tile.transform.position.z))
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        foreach (Node step in path)
        {
            step.tile.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    private Node GetStartingNode()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.tag == TILE)
                return new Node(hit.transform.gameObject);
        }

        return null;
    }

    private Node GetTargetNode()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(targetObject.transform.position.x, targetObject.transform.position.y - 0.5f), Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.tag == TILE)
                return new Node(hit.transform.gameObject);
        }

        return null;
    }

    /*
    private void BeginPathing()
    {
        isPathing = true;
        doneMovingX = false;
        doneMovingY = false;
        targetNode = null;

        List<GameObject> nodes = new List<GameObject>();

        Vector3 upPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Vector3 downPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        Vector3 upRightDiagonalPosition = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);
        Vector3 upLeftDiagonalPosition = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, transform.position.z);
        Vector3 downRightDiagonalPosition = new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, transform.position.z);
        Vector3 downLeftDiagonalPosition = new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, transform.position.z);

        RaycastHit2D[] upHits = Physics2D.RaycastAll(upPosition, Vector2.zero);
        RaycastHit2D[] upRightDiagonalHits = Physics2D.RaycastAll(upRightDiagonalPosition, Vector2.zero);
        RaycastHit2D[] upLeftDiagonalHits = Physics2D.RaycastAll(upLeftDiagonalPosition, Vector2.zero);
        RaycastHit2D[] downHits = Physics2D.RaycastAll(downPosition, Vector2.zero);
        RaycastHit2D[] downRightDiagonalHits = Physics2D.RaycastAll(downRightDiagonalPosition, Vector2.zero);
        RaycastHit2D[] downLeftDiagonalHits = Physics2D.RaycastAll(downLeftDiagonalPosition, Vector2.zero);

        bool canAddUp = true;
        foreach (RaycastHit2D hit in upHits)
        {
            if (hit.transform.tag == TILE)
            {
                if (!hit.transform.gameObject.GetComponent<Tile>().objectOnTile)
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = hit.transform.gameObject.GetComponent<SpriteRenderer>().color == Color.green ? Color.green : Color.yellow;
                    nodes.Add(hit.transform.gameObject);
                }
            }
        }

        foreach (RaycastHit2D hit in upRightDiagonalHits)
        {
            if (hit.transform.tag == TILE)
            {
                if (!hit.transform.gameObject.GetComponent<Tile>().objectOnTile)
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = hit.transform.gameObject.GetComponent<SpriteRenderer>().color == Color.green ? Color.green : Color.yellow;
                    nodes.Add(hit.transform.gameObject);
                }
            }
        }

        foreach (RaycastHit2D hit in upLeftDiagonalHits)
        {
            if (hit.transform.tag == TILE)
            {
                if (!hit.transform.gameObject.GetComponent<Tile>().objectOnTile)
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = hit.transform.gameObject.GetComponent<SpriteRenderer>().color == Color.green ? Color.green : Color.yellow;
                    nodes.Add(hit.transform.gameObject);
                }
            }
        }

        foreach (RaycastHit2D hit in downLeftDiagonalHits)
        {
            if (hit.transform.tag == TILE)
            {
                if (!hit.transform.gameObject.GetComponent<Tile>().objectOnTile)
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = hit.transform.gameObject.GetComponent<SpriteRenderer>().color == Color.green ? Color.green : Color.yellow;
                    nodes.Add(hit.transform.gameObject);
                }
            }
        }

        foreach (RaycastHit2D hit in downRightDiagonalHits)
        {
            if (hit.transform.tag == TILE)
            {
                if (!hit.transform.gameObject.GetComponent<Tile>().objectOnTile)
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = hit.transform.gameObject.GetComponent<SpriteRenderer>().color == Color.green ? Color.green : Color.yellow;
                    nodes.Add(hit.transform.gameObject);
                }
            }
        }

        foreach (RaycastHit2D hit in downHits)
        {
            if (hit.transform.tag == TILE)
            {
                if (!hit.transform.gameObject.GetComponent<Tile>().objectOnTile)
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = hit.transform.gameObject.GetComponent<SpriteRenderer>().color == Color.green ? Color.green : Color.yellow;
                    nodes.Add(hit.transform.gameObject);
                }
            }
        }

        // Remove already traversed nodes
        List<GameObject> nodesToRemove = new List<GameObject>();
        foreach (GameObject node in nodes)
        {
            foreach (Vector3 visitedPoint in traversed)
            {
                if (node.transform.position == visitedPoint)
                {
                    nodesToRemove.Add(node);
                }
            }
        }

        foreach (GameObject nodeToRemove in nodesToRemove)
        {
            GameObject foundNode = null;
            foreach (GameObject node in nodes)
            {
                if (nodeToRemove.transform.position == node.transform.position)
                {
                    foundNode = node;
                    break;
                }
            }

            if (foundNode)
                nodes.Remove(foundNode);
        }
       

        // Find the walkable node that is closest to target point with bias for vertical axis
        GameObject chosen = null;
        foreach (GameObject node in nodes)
        {
            if (!chosen || targetObject.transform.position.y - chosen.transform.position.y > targetObject.transform.position.y - node.transform.position.y || Vector2.Distance(chosen.transform.position, targetObject.transform.position) > Vector2.Distance(node.transform.position, targetObject.transform.position))
            {
                chosen = node;
            }
        }

        if (chosen)
        {
            chosen.GetComponent<SpriteRenderer>().color = Color.green;
            traversed.Add(chosen.transform.position);
            targetNode = chosen;
        }
        moving = true;
    }

    private void MoveNPC()
    {
        if (!targetNode || transform.position == targetNode.transform.position)
        {
            moving = false;
            BeginPathing();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetNode.transform.position, 1);
        }
    }
    */

    private float GetCalibratedSpeed(float x, float y)
    {
        if (Math.Abs(x) + Math.Abs(y) == 2)
            return defaultSpeed / (float)Math.Sqrt(2);

        return defaultSpeed;
    }

    private void HandleMovementAnimations()
    {
        bool isRunning = (dir_x != 0 || dir_y != 0);
        animator.SetBool("isRunning", isRunning);

        if (isRunning)
        {
            animator.SetFloat("x", dir_x);
            animator.SetFloat("y", dir_y);
        }
    }

    private void InteractWithObject()
    {
        if (targetObjectScript != null && !targetObjectScript.IsOccupied())
        {
            targetObjectScript.Interact();
        }
    }

}
