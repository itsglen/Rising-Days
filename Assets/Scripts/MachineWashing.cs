using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineWashing : MonoBehaviour
{

    private Animator animator;
    private BoxCollider2D[] colliders;
    private BoxCollider2D collider;

    void Start()
    {
        animator = GetComponent<Animator>();
        colliders = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D collider in colliders)
        {
            if (collider.isTrigger)
            {
                this.collider = collider;
            }
        }

        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt((transform.position.y - 0.5f) * 100f) * -1;
        GameManager.instance.OnBuildModeChange += RespondToBuildMode;
    }

    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && !animator.GetBool("running"))
        {
            StartCoroutine(StartWashingCycle());
        }
    }

    IEnumerator StartWashingCycle()
    {
        animator.SetBool("running", true);
        yield return new WaitForSeconds(60);
        animator.SetBool("running", false);
    }

    private void RespondToBuildMode()
    {
        collider.enabled = !GameManager.instance.buildMode;
    }

}
