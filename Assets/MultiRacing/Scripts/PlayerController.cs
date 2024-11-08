using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IControllable
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _steeringAngle;
    [SerializeField] private float _steeringSpeed;

    [Header("Random color for car")]
    [SerializeField] private GameObject _body;
    [SerializeField] private Material[] _materials;

    private Vector2 _wheelAngle;
    private float _currentYRotation = 0f;

    [SyncVar(hook = nameof(SetRandomMaterial))]
    [SerializeField] private int _bodyColorIndex = 0;

    public override void OnStartLocalPlayer()
    {
        System.Random rnd = new System.Random();
        _bodyColorIndex = rnd.Next(_materials.Length);
        //_body.GetComponent<MeshRenderer>().material.mainTexture = _materials[_bodyColorIndex].mainTexture;
    }

    private void SetRandomMaterial(int oldColorIndex, int newColorIndex)
    {
        Debug.Log("Color changed");
        _body.GetComponent<MeshRenderer>().material.mainTexture = _materials[newColorIndex].mainTexture;
    }

    public void Steering(Vector2 direction)
    {
        _wheelAngle = direction;
        if (direction != Vector2.zero)
        {
            Vector3 movement = new Vector3(direction.x, 0f, 0f);
            transform.Translate(movement * _movementSpeed * Time.deltaTime, Space.World);

            _currentYRotation = Mathf.LerpAngle(_currentYRotation, direction.x * _steeringAngle, Time.deltaTime * _steeringAngle);
            transform.localRotation = Quaternion.Euler(0, _currentYRotation, 0);
        }
        else
        {
            _currentYRotation = Mathf.Lerp(_currentYRotation, 0, Time.deltaTime * _steeringSpeed);
            transform.localRotation = Quaternion.Euler(0, _currentYRotation, 0);
        }
    }

    public Vector2 GetWheelAngle()
    {
        return _wheelAngle;
    }
}
