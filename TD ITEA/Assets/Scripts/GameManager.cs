using CustomEvents;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private ScoreManager _scoreManager = new ScoreManager();
    private LevelManager _scoreLevel = new LevelManager();
    private SpawnEnemy _spawnEnemy;
    private int _score = 1;
    [SerializeField] private List<GameObject> _enemyObj = new List<GameObject>();
    private bool _newLevel = true;
    private float _newTime = 20f;
    private float _timeSpawn = 20f;
    private float _timeSec = 0;
    private float _timeMin = 0;
    [SerializeField] private TextMeshProUGUI _sec;
    [SerializeField] private TextMeshProUGUI _min;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _tochki;
    [SerializeField] private CaseButton[] _caseButtons;
    public bool Build { get; private set; }

    private void Start()
    {
        _scoreManager.SetScore(6000);
        _scoreLevel.SetScore(_score);
        _spawnEnemy = FindObjectOfType<SpawnEnemy>();
    }

    public void AddRandomScore(int score)
    {
        _scoreManager.AddScore(score);
    }

    private void AddLevelScore(int score)
    {
        _scoreLevel.SetScore(score);
    }

    public void AddEnemyObj(GameObject obj)
    {
        _enemyObj.Add(obj);
    }

    private void Update()
    {
        if (!_newLevel)
        {
            _sec.gameObject.SetActive(true);
            _timeSec += Time.deltaTime;
            _sec.text = Mathf.Round(_timeSec).ToString();
            if (_timeSec >= 59)
            {
                _min.gameObject.SetActive(true);
                _tochki.gameObject.SetActive(true);
                _timeSec = 0;
                _timeMin++;
                _min.text = _timeMin.ToString();
            }
        }
        else
        {
            _timeSec = 0;
            _timeMin = 0;
            _min.gameObject.SetActive(false);
            _tochki.gameObject.SetActive(false);
            _sec.gameObject.SetActive(false);
        }
        ProgrammLevel();
    }

    private void ProgrammLevel()
    {
        if (_newLevel)
        {
            Build = true;
            for (int i = 0; i < _caseButtons.Length; i++)
            {
                _caseButtons[i].ColorButton(1);
            }
            _timeSpawn -= Time.deltaTime;
            _timer.gameObject.SetActive(true);
            _timer.text = Mathf.Round(_timeSpawn).ToString();
            if (_timeSpawn <= 0)
            {
                Build = false;
                for (int i = 0; i < _caseButtons.Length; i++)
                {
                    _caseButtons[i].ColorButton(0.5f);
                }
                _timer.gameObject.SetActive(false);
                if(_score == 1)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 0, 2, 0, });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 2)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 1, 1, 1 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 3)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 0, 0, 1, 2, 2 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 4)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 0, 1, 2, 2, 3, 1, 2, 2, 3 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 5)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 6)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 1, 1, 1, 3, 3, 1, 1, 3, 3 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 7)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 4, 4, 4, 6, 6, 4, 4, 6, 6 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 8)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 9)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 6, 6, 6, 7, 7, 6, 6, 7, 7 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 10)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 8, 8, 8, 8, 8, 3, 3, 3, 8, 8, 8, 8, 3, 3, 3 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 11)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 8, 8, 8, 8, 8, 4, 4, 4, 8, 8, 8, 8, 4, 4, 4 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 12)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 8, 8, 8, 8, 8, 5, 5, 5, 5, 8, 8, 8, 5, 5, 5 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 13)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 8, 8, 8, 8, 8, 6, 6, 6, 8, 8, 8, 8, 6, 6, 6 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 14)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 8, 8, 8, 8, 8, 7, 7, 7, 8, 8, 8, 8, 7, 7, 7 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 15)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 9, 9, 9, 9, 9, 6, 6, 6, 9, 8, 8, 8, 6, 6, 6 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 16)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 10, 10, 10, 9, 9, 7, 7, 7, 9, 10, 8, 8, 10, 10, 10 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 17)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 9, 9, 9, 9, 9, 6, 6, 6, 9, 8, 8, 8, 6, 6, 6, 9, 9, 9, 9, 9, 6, 6, 6, 9, 8, 8, 8, 6, 6, 6 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 18)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 8, 8, 8, 8, 8, 7, 7, 7, 8, 8, 8, 8, 7, 7, 7, 8, 8, 8, 8, 7, 7, 7, 8, 8, 8, 8, 7, 7, 7 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 19)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 8, 10, 10, 10, 9, 9, 7, 7, 7, 9, 10, 8, 8, 10, 10, 10, 10, 10, 10, 9, 9, 7, 7, 7, 9, 10, 8, 8, 10, 10, 10 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                else if (_score == 20)
                {
                    _spawnEnemy.InstationPrefab(new int[] { 9, 11, 11, 11, 10, 11, 9, 9, 9, 10, 11, 9, 9, 11, 11, 11, 11, 11, 11, 10, 10, 11, 9, 9, 10, 10, 9, 9, 11, 11, 11 });
                    _newLevel = false;
                    _timeSpawn = _newTime;
                }
                //else if (_score == 21)
                //{
                //    _spawnEnemy.InstationPrefab(new int[] { 9, 11, 11, 11, 10, 11, 9, 12, 9, 10, 11, 9, 9, 11, 11, 11, 11, 11, 11, 10, 10, 11, 9, 12, 10, 10, 9, 9, 11, 11, 11 });
                //    _newLevel = false;
                //    _timeSpawn = _newTime;
                //}
                //else if (_score == 22)
                //{
                //    _spawnEnemy.InstationPrefab(new int[] { 12, 11, 11, 11, 10, 11, 12, 12, 12, 10, 11, 9, 9, 11, 11, 11, 11, 11, 11, 10, 10, 11, 12, 12, 10, 10, 12, 12, 11, 11, 11 });
                //    _newLevel = false;
                //    _timeSpawn = _newTime;
                //}
                //else if (_score == 23)
                //{
                //    _spawnEnemy.InstationPrefab(new int[] { 12, 11, 11, 11, 10, 11, 12, 12, 12, 10, 11, 12, 12, 11, 11, 11, 11, 11, 11, 10, 10, 11, 12, 12, 10, 10, 12, 12, 11, 11, 11 });
                //    _newLevel = false;
                //    _timeSpawn = _newTime;
                //}
                //else if (_score == 24)
                //{
                //    _spawnEnemy.InstationPrefab(new int[] { 12, 11, 11, 11, 13, 11, 12, 12, 12, 13, 11, 13, 13, 11, 11, 11, 11, 11, 11, 13, 13, 11, 12, 12, 13, 13, 12, 12, 11, 11, 11 });
                //    _newLevel = false;
                //    _timeSpawn = _newTime;
                //}
                //else if (_score == 25)
                //{
                //    _spawnEnemy.InstationPrefab(new int[] { 12, 11, 11, 11, 13, 11, 12, 12, 12, 13, 11, 13, 13, 11, 11, 11, 11, 11, 11, 13, 13, 11, 12, 12, 13, 13, 12, 12, 11, 11, 11, 12, 11, 11, 11, 13, 11, 12, 12, 12, 13, 11, 13, 13, 11, 11, 11, 11, 11, 11, 13, 13, 11, 12, 12, 13, 13, 12, 12, 11, 11, 11 });
                //    _newLevel = false;
                //    _timeSpawn = _newTime;
                //}
            }
        }
    }

    public void RemoveEnemyObj(GameObject obj)
    {
        _enemyObj.Remove(obj);
        if (_enemyObj.Count == 0)
        {
            _score++;
            AddLevelScore(_score);
            _newLevel = true;            
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

    public void RemoveScore(int score)
    {
        Score -= score;
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

public class LevelManager
{
    public int ScoreLevel { get; private set; }

    //public void AddScore(int score)
    //{
    //    ScoreLevel += score;
    //    PostScoreChangeEvent();
    //}

    public void SetScore(int score)
    {
        ScoreLevel = score;
        PostScoreChangeEvent();
    }

    private void PostScoreChangeEvent()
    {
        EventAggregator.Post(this, new LevelNumber() { Level = ScoreLevel });
    }
}
