using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineWashing : MonoBehaviour
{

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(2) && !animator.GetBool("running"))
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

}
