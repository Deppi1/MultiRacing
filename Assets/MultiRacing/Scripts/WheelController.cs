using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public GameObject WheelGameobject;

    [SerializeField] private float _angle = 30;

    private PlayerController _playerController;
    private List<GameObject> _wheelsPivot = new List<GameObject>();
    private List<GameObject> _wheels = new List<GameObject>();


    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _wheelsPivot = FindChildsInGameobject(WheelGameobject);

        foreach (GameObject wheels in _wheelsPivot)
        {
            foreach (Transform wheel in wheels.transform)
            {
                _wheels.Add(wheel.gameObject);
            }
        }
    }

    private void Update()
    {
        WheelControl(_wheels, _wheelsPivot);
    }

    private List<GameObject> FindChildsInGameobject(GameObject parent)
    {
        List<GameObject> childs = new List<GameObject>();

        foreach (Transform child in parent.transform)
        {
            childs.Add(child.gameObject);
        }

        return childs;
    }

    private void WheelControl(List<GameObject> wheels, List<GameObject> wheelPivots)
    {
        for (int i = 0; i < wheels.Count; i++)
        {
            if (i == 0 || i == 1)
            {
                float targetAngle = _playerController.GetWheelAngle().x * _angle;
                float clampedAngle = Mathf.Clamp(targetAngle, -_angle, _angle);

                wheelPivots[i].transform.localRotation = Quaternion.Euler(0, clampedAngle, 0);
            }

            wheels[i].transform.Rotate(GameParameters.Instance.RoadSpeed * 100 * Time.deltaTime, 0, 0);
        }
    }
}
