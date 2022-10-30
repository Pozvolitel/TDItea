using CustomEvents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreManager _scoreManager = new ScoreManager();

    private void Start()
    {
        _scoreManager.SetScore(0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TestAddRandomScore();
        }
    }
    private void TestAddRandomScore()
    {
        var score = UnityEngine.Random.Range(10, 101);
        _scoreManager.AddScore(score);
        //Debug.Log($"[{GetType().Name}][TestAddRandomScore] was added: {score}");
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
