using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = new GameManager();
    public Vector3 playerColliderPosition = new Vector3(0, 0, 0);

    // When a placeable item is selected in the players inventory, the player will enter build mode. This will activate colliders on tiles and disable colliders on other objects
    public bool buildMode = false;

    public delegate void OnPlayerPositionChangeDelegate();
    public event OnPlayerPositionChangeDelegate OnPlayerColliderPositionChange;

    // Position is the bottom center of BoxCollider2D
    public void SetPlayerColliderPosition(Vector3 position)
    {
        playerColliderPosition = position;
    }

    public delegate void OnBuildModeChangedDelegate();
    public event OnBuildModeChangedDelegate OnBuildModeChange;

    // Position is the bottom center of BoxCollider2D
    public void ToggleBuildMode()
    {
        this.buildMode = !this.buildMode;
        Debug.Log("Build Mode: " + this.buildMode);
        OnBuildModeChange();
    }

}
