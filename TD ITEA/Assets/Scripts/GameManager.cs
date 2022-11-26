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
            if (_score == 2) _spawnEnemy.InstationPrefab(new int[] { 0, 0, 0, 1, 1, 0, 0, 0, 1, 1 });
            else if (_score == 3) _spawnEnemy.InstationPrefab(new int[] { 0, 0, 1, 2, 2, 0, 1, 2, 2 });
            else if (_score == 4) _spawnEnemy.InstationPrefab(new int[] { 0, 1, 2, 2, 3, 1, 2, 2, 3 });
            else if (_score == 5) _spawnEnemy.InstationPrefab(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2 });
            else if (_score == 6) _spawnEnemy.InstationPrefab(new int[] { 1, 1, 1, 3, 3, 1, 1, 3, 3 });
            else if (_score == 7) _spawnEnemy.InstationPrefab(new int[] { 4, 4, 4, 6, 6, 4, 4, 6, 6 });
            else if (_score == 8) _spawnEnemy.InstationPrefab(new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5 });
            else if (_score == 9) _spawnEnemy.InstationPrefab(new int[] { 6, 6, 6, 7, 7, 6, 6, 7, 7 });
            else if (_score == 10) _spawnEnemy.InstationPrefab(new int[] { 8, 8, 8, 8, 8, 3, 3, 3, 8, 8, 8, 8, 3, 3, 3 });
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
