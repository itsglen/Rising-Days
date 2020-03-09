using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Position refers to the center point of transform
    private Transform transform;
    private BoxCollider2D collider;

    private bool canPlace = false;

    public GameObject washingMachine;
    // A normal object can be placed directly on ground or above a ground object
    private GameObject objectOnTile = null;
    // A ground object can have a normal object (like a washing machine) placed above it and can always be placed under a normal object
    private GameObject groundObjectOnTile = null;

    void Start()
    {
        transform = GetComponent<Transform>();
        collider = GetComponent<BoxCollider2D>();
        GameManager.instance.OnPlayerColliderPositionChange += PollMouseConditions;
        GameManager.instance.OnBuildModeChange += RespondToBuildMode;
    }

    void Update()
    {
        if (canPlace && Input.GetMouseButtonDown(0) && objectOnTile == null)
        {
            GameObject instantiatedObject = Instantiate(washingMachine);
            Vector3 objectPosition = GetComponent<Transform>().position;
            objectPosition.y += 0.25f;
            instantiatedObject.GetComponent<Transform>().position = objectPosition;
            objectOnTile = instantiatedObject;
        }
    }

    void OnMouseOver()
    {
        if ((Utilities.instance.IsTransformWithinObject(GameManager.instance.playerColliderPosition, transform.position) || objectOnTile != null) && (Vector2.Distance(GameManager.instance.playerColliderPosition, transform.position) < 1))
        {
            canPlace = false;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (Vector2.Distance(GameManager.instance.playerColliderPosition, GetComponent<Transform>().position) < 1)
        {
            canPlace = true;
            GetComponent<SpriteRenderer>().color = Color.green;
        }

        if (Input.GetMouseButtonDown(1) && objectOnTile != null)
        {
            Destroy(objectOnTile);
            objectOnTile = null;
        }
    }

    void OnMouseExit()
    {
        canPlace = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void PollMouseConditions()
    {
        if (Vector2.Distance(GameManager.instance.playerColliderPosition, GetComponent<Transform>().position) > 2)
        {
            canPlace = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void RespondToBuildMode()
    {
        collider.enabled = GameManager.instance.buildMode;
    }

}
