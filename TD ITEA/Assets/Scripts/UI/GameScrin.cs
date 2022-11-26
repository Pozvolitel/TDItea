using CustomEvents;
using TMPro;
using UnityEngine;

public class GameScrin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _level;

    private void Awake()
    {
        EventAggregator.Subscrible<ScorePointChanged>(ScorePoint);
        EventAggregator.Subscrible<LevelNumber>(ScoreLevel);
    }

    private void OnDestroy()
    {
        EventAggregator.UnSubscrible<ScorePointChanged>(ScorePoint);
        EventAggregator.UnSubscrible<LevelNumber>(ScoreLevel);
    }

    private void ScoreLevel(object sender, LevelNumber eventData)
    {
        _level.text = eventData.Level.ToString();
    }

    private void ScorePoint(object sender, ScorePointChanged eventData)
    {
        _scoreText.text = eventData.ScorePoint.ToString();
    }
}
