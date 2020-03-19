using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectController : MonoBehaviour
{

    private BoxCollider2D[] colliders;
    private BoxCollider2D trigger;
    private BoxCollider2D collision;
    private GameObject onTileReference = null;

    void Start()
    {
        foreach (BoxCollider2D collider in GetComponents<BoxCollider2D>())
        {
            if (collider.isTrigger) { trigger = collider; }
            else { collision = collider; }
        }

        GameManager.instance.OnBuildModeChange += RespondToBuildMode;
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt((transform.position.y - 0.5f) * 100f) * -1;
    }

    void OnMouseOver()
    {
        if (GameManager.instance.buildMode) { onTileReference.GetComponent<SpriteRenderer>().color = Color.red; }
    }

    void OnMouseExit()
    {
        onTileReference.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void SetTileReference(GameObject tile)
    {
        onTileReference = tile;
    }

    private void RespondToBuildMode()
    {
        trigger.enabled = !GameManager.instance.buildMode;
    }

}
