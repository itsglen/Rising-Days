using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Position refers to the center point of transform
    private Transform transform;
    // Collider for mouse collisions
    private BoxCollider2D collider;
    private bool canPlace = false;

    public GameObject washingMachine;
    // A normal object can be placed directly on ground or above a ground object
    private GameObject objectOnTile = null;
    // A ground object can have a normal object (like a washing machine) placed above it and can always be placed under a normal object
    private GameObject groundObjectOnTile = null;
    // Constant names for tags
    private string TILE = "Tile";
    private string BORDER = "Border";
    private string WALL = "Wall";

    void Start()
    {
        transform = GetComponent<Transform>();
        collider = GetComponent<BoxCollider2D>();
        GameManager.instance.OnBuildModeChange += RespondToBuildMode;
    }

    void OnMouseOver()
    {
        if (GameManager.instance.buildMode) { BuildModeLogic(); }
    }

    public void DestroyObjectOnTile()
    {
        Destroy(objectOnTile);
        objectOnTile = null;
        collider.isTrigger = false;
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void RespondToBuildMode()
    {
        // Mouse collision with tile is only enabled if in build mode
        collider.enabled = GameManager.instance.buildMode;
    }

    private void BuildModeLogic()
    {
        // Player cannot place if standing within tile or if there is an object on the tile already
        if (Utilities.instance.IsTransformWithinObject(GameManager.instance.playerColliderPosition, transform.position) || IsObjectVerticallyAdjacent())
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (Vector2.Distance(GameManager.instance.playerColliderPosition, GetComponent<Transform>().position) < 1)
        {
            GetComponent<SpriteRenderer>().color = Color.green;

            // Place an object 
            if (Input.GetMouseButtonDown(0) && objectOnTile == null)
            {
                GameObject instantiatedObject = Instantiate(washingMachine);
                Vector3 objectPosition = GetComponent<Transform>().position;
                objectPosition.y += 0.25f;
                instantiatedObject.GetComponent<Transform>().position = objectPosition;
                objectOnTile = instantiatedObject;
                objectOnTile.GetComponent<TileObjectController>().SetTileReference(gameObject);
                collider.enabled = false;
            }
        }
    }

    // Raycast one tile upwards and one tile downwards to see if object is placed on adjacent tile
    private bool IsObjectVerticallyAdjacent()
    {
        bool result = false;

        Vector3 upPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Vector3 downPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

        RaycastHit2D[] upHits = Physics2D.RaycastAll(upPosition, Vector2.zero);
        RaycastHit2D[] downHits = Physics2D.RaycastAll(downPosition, Vector2.zero);

        foreach (RaycastHit2D hit in upHits)
        {
            if (hit.transform.tag != TILE && hit.transform.tag != WALL)
            {
                result = true;
            }
        }

        foreach (RaycastHit2D hit in downHits)
        {
            if ((hit.transform.tag != TILE && hit.transform.position.y <= downPosition.y + 0.25f && hit.transform.position.y >= downPosition.y + 0.25f) || hit.transform.tag == BORDER)
            {
                result = true;
            }
        }

        return result;
    }

}
