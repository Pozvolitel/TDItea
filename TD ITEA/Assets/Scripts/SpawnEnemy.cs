using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private int _scoreLevel;
    private float _timeSpawn = 1f;
    [SerializeField] private GameObject[] _prefabsPack;
    [SerializeField] private Transform _spawnPoint;
    private bool isShoot = true;
    private int[] _numberPrefab;

    private void Update()
    {
        if (!isShoot && _scoreLevel < _numberPrefab.Length)
        {
            StartCoroutine(SpawnPrefab());
        }
    }

    public void InstationPrefab(int[] newNumber)
    {
        isShoot = false;
        _scoreLevel = 0;
        _numberPrefab = newNumber;
    }    

    IEnumerator SpawnPrefab()
    {
        isShoot = true;
        yield return new WaitForSeconds(_timeSpawn);
        Instantiate(_prefabsPack[_numberPrefab[_scoreLevel]], _spawnPoint.position, Quaternion.identity);
        _scoreLevel++;
        isShoot = false;
    }
}
