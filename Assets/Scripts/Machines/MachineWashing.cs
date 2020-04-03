using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineWashing : MonoBehaviour, IMachine
{

    private bool occupied = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator StartWashingCycle()
    {
        occupied = true;
        animator.SetBool("running", true);
        yield return new WaitForSeconds(60);
        animator.SetBool("running", false);
        occupied = false;
    }

    public void Interact()
    {
        StartCoroutine(StartWashingCycle());
    }

    public bool IsOccupied()
    {
        return occupied;
    }

}
