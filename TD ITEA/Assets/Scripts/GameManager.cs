using CustomEvents;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreManager _scoreManager = new ScoreManager();
    private SpawnEnemy _spawnEnemy;
    private int _score = 1;
    private List<GameObject> _enemyObj = new List<GameObject>();
    private int[] _newScore;

    private void Start()
    {
        _scoreManager.SetScore(0);
        _spawnEnemy = FindObjectOfType<SpawnEnemy>();
    }

    public void AddRandomScore(int score)
    {
        _scoreManager.AddScore(score);
    }

    public void AddEnemyObj(GameObject obj)
    {
        _enemyObj.Add(obj);
    }

    public void RemoveEnemyObj(GameObject obj)
    {
        _enemyObj.Remove(obj);
        if (_enemyObj.Count == 0)
        {
            _score++;
            if (_score == 2) _spawnEnemy.InstationPrefab(new int[] { 1, 1, 1, 1, 1 });
            else if (_score == 3) _spawnEnemy.InstationPrefab(new int[] { 0, 0, 0, 0, 0 });
        }
    }
}

public class ScoreManager
{
    public int Score { get; private set; }

    public void AddScore(int score)
    {
        Score += score;
        PostScoreChangeEvent();
    }

    public void SetScore(int score)
    {
        Score = score;
        PostScoreChangeEvent();
    }

    private void PostScoreChangeEvent()
    {
        EventAggregator.Post(this, new ScorePointChanged() { ScorePoint = Score });
    }
}
