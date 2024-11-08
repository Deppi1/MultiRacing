using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartServer : MonoBehaviour
{
    private NetworkManager _networkManager;

    private void Awake()
    {
        _networkManager = GetComponent<NetworkManager>();
        StartServers();
    }

    public void StartServers()
    {
        // ��������, ��� ������ ��� �� �������
        if (!_networkManager.isNetworkActive)
        {
            _networkManager.StartServer();
            Debug.Log("������ �������.");
        }
    }


}
