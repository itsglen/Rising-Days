using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = new GameManager();
    public Vector3 playerColliderPosition = new Vector3(0, 0, 0);

    public delegate void OnPlayerPositionChangeDelegate();
    public event OnPlayerPositionChangeDelegate OnPlayerColliderPositionChange;

    // Position is the bottom center of BoxCollider2D
    public void SetPlayerColliderPosition(Vector3 position)
    {
        playerColliderPosition = position;
        OnPlayerColliderPositionChange();
    }

}
