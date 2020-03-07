using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = new GameManager();
    public Vector3 playerPosition = new Vector3(0, 0, 0);

    public delegate void OnPlayerPositionChangeDelegate();
    public event OnPlayerPositionChangeDelegate OnPlayerPositionChange;

    public void SetPlayerPosition(Vector3 position)
    {
        playerPosition = position;
        OnPlayerPositionChange();
    }

}
