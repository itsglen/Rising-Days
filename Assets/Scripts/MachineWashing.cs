using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineWashing : MonoBehaviour
{

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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

}
