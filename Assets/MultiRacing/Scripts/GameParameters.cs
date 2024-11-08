using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : NetworkBehaviour
{
    [SerializeField] private float _secondsToIncreaseSpeed;

    [SyncVar]
    [SerializeField] private float _roadSpeed;

    #region Singleton
    /// <summary>
    /// Instance of our Singleton
    /// </summary>
    public static GameParameters Instance
    {
        get
        {
            return _instance;
        }
    }
    private static GameParameters _instance;

    public void InitializeSingletonGameParameters()
    {
        // Destroy any duplicate instances that may have been created
        if (_instance != null && _instance != this)
        {
            Debug.Log("destroying singleton");
            Destroy(this);
            return;
        }
        _instance = this;
    }
    #endregion

    private void Awake()
    {
        InitializeSingletonGameParameters();
    }

    private void Start()
    {
        StartCoroutine("ChangeSpeed");
    }

    IEnumerator ChangeSpeed()
    {
        for (; ; )
        {
            _roadSpeed += 1;
            yield return new WaitForSeconds(_secondsToIncreaseSpeed);
        }
    }

    public float RoadSpeed
    {
        get { return _roadSpeed; }
    }
}
