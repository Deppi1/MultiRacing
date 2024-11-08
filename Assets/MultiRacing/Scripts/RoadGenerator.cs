using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : NetworkBehaviour
{
    public GameObject _roadPrefab;
    public int maxRoadCount = 0;

    private float speed = 0;

    private List<GameObject> _roads = new List<GameObject>();

    private void Start()
    {
        ResetLevel();
        StartLevel();
    }

    
    private void Update()
    {
        speed = GameParameters.Instance.RoadSpeed;

        if (speed == 0)
            return;

        foreach (GameObject road in _roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        if (_roads.Count > 0 && _roads[0].transform.position.z < -15)
        {
            RepositionRoadSegment();
        }
    }

    [Server]
    private void RepositionRoadSegment()
    {
        GameObject firstRoad = _roads[0];
        _roads.RemoveAt(0);

        Vector3 newPosition = _roads[_roads.Count - 1].transform.position + new Vector3(0, 0, _roadPrefab.transform.localScale.z);
        firstRoad.transform.position = newPosition;

        _roads.Add(firstRoad);
    }

    [Server]
    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;

        if (_roads.Count > 0)
        {
            pos = _roads[_roads.Count - 1].transform.position + new Vector3(0,0, _roadPrefab.transform.localScale.z);
        }

        GameObject go = Instantiate(_roadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        _roads.Add(go);
        NetworkServer.Spawn(go);
    }

    [Server]
    public void StartLevel()
    {

    }

    [Server]
    public void ResetLevel()
    {
        speed = 0;
        while (_roads.Count > 0)
        {
            Destroy(_roads[0]);
            _roads.RemoveAt(0);
        }
        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public List<GameObject> GetRoads()
    {
        return _roads;
    }
}
