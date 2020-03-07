using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private bool canPlace = false;

    public GameObject washingMachine;
    private GameObject objectOnTile = null;

    void Start()
    {
        GameManager.instance.OnPlayerPositionChange += PollMouseConditions;
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
        if ((Utilities.instance.IsTransformWithinObject(GameManager.instance.playerPosition, GetComponent<Transform>().position)|| objectOnTile != null) && (Vector2.Distance(GameManager.instance.playerPosition, GetComponent<Transform>().position) < 2))
        {
            canPlace = false;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (Vector2.Distance(GameManager.instance.playerPosition, GetComponent<Transform>().position) < 2)
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
        if (Vector2.Distance(GameManager.instance.playerPosition, GetComponent<Transform>().position) > 2)
        {
            canPlace = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

}
