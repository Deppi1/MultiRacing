using Mirror;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    private GameObject _roadGeneratorObject;
    private RoadGenerator _roadGenerator;
    private GameObject _roadPrefab;
    private NetworkManager _networkManager;

    [SetUp]
    public void Setup()
    {
        // ������� ��������� GameParameters, ���� �� ��� �� ����������
        if (GameParameters.Instance == null)
        {
            GameObject gameParametersObject = new GameObject();
            gameParametersObject.AddComponent<GameParameters>(); // ������������, ��� � GameParameters ���� Singleton-����������
        }

        _roadGeneratorObject = new GameObject();
        _roadGenerator = _roadGeneratorObject.AddComponent<RoadGenerator>();

        _roadPrefab = new GameObject();
        _roadPrefab.transform.localScale = new Vector3(1, 1, 10);
        _roadGenerator._roadPrefab = _roadPrefab;
        _roadGenerator.maxRoadCount = 3;
    }

    [UnityTest]
    public IEnumerator ResetLevel_CreatesCorrectRoadCount()
    {
        _roadGenerator.ResetLevel();
        yield return null;

        // ���������, ��� ���������� �������� � _roads ��������� � maxRoadCount
        Assert.AreEqual(_roadGenerator.maxRoadCount, _roadGenerator.GetRoads().Count);
    }

    [TearDown]
    public void Teardown()
    {
        // ������� ����� ������� �����
        Object.Destroy(_roadGeneratorObject);
        Object.Destroy(_roadPrefab);
    }
}
