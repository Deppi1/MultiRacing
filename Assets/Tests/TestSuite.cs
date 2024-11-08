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
        // Создаем экземпляр GameParameters, если он еще не существует
        if (GameParameters.Instance == null)
        {
            GameObject gameParametersObject = new GameObject();
            gameParametersObject.AddComponent<GameParameters>(); // Предполагаем, что у GameParameters есть Singleton-реализация
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

        // Проверяем, что количество объектов в _roads совпадает с maxRoadCount
        Assert.AreEqual(_roadGenerator.maxRoadCount, _roadGenerator.GetRoads().Count);
    }

    [TearDown]
    public void Teardown()
    {
        // Очистка после каждого теста
        Object.Destroy(_roadGeneratorObject);
        Object.Destroy(_roadPrefab);
    }
}
