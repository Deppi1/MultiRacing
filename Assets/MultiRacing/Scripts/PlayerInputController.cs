using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : NetworkBehaviour
{
    private GameInputs _gameInputs;
    private IControllable _controllable;

    private void Awake()
    {
        _gameInputs = new GameInputs();
        _gameInputs.Enable();
        _controllable = GetComponent<IControllable>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        SteeringWheel();
    }

    private void SteeringWheel()
    {
        Vector2 wheelAngle = _gameInputs.Gameplay.Movement.ReadValue<Vector2>();
        _controllable.Steering(wheelAngle);
    }
}
