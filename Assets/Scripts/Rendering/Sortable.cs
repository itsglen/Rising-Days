using UnityEngine;

/*
 * Any GameObject which contains this script will be sorted when an event listener is dispatched to Sort()
 * Depends on parent GameObject containing: SpriteRenderer
*/
public class Sortable : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Sort()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt((transform.position.y - 0.5f) * 100f) * -1;
    }

}
