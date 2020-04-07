using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AddComponentMenu("Systems/Input System")]
public class InputSystem : GameSystem
{

    [SerializeField]
    private BoolEvent input;

    [SerializeField]
    private BoolEvent pauzeEvent;

    [SerializeField]
    private bool gamePauzed = false;

    public override void OnLoadSystem()
    {
        // pauzeEvent?.AddListener(OnGamePauzed);
    }

    private void OnGamePauzed(bool state)
    {
        gamePauzed = state;

        if (state)
        {
            input.Invoke(false);
        }
    }

    public override void OnFixedTick()
    {
        if (!gamePauzed)
            input.Invoke(true);
    }

    public override void OnTick()
    {
        if (!gamePauzed)
        {
            // Key events can be captured here
        }
    }

}