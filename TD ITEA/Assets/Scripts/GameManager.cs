using CustomEvents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreManager _scoreManager = new ScoreManager();

    private void Start()
    {
        _scoreManager.SetScore(0);
    }

    public void AddRandomScore(int score)
    {
        _scoreManager.AddScore(score);
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
