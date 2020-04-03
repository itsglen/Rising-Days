using UnityEngine;

/*
 * Any GameObject which contains this script will be sorted on crate
 * Depends on parent GameObject containing: SpriteRenderer
*/
public class StaticSortable : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = Mathf.RoundToInt((transform.position.y - 0.5f) * 100f) * -1;
    }

}
