using CustomEvents;
using TMPro;
using UnityEngine;

public class GameScrin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        EventAggregator.Subscrible<ScorePointChanged>(ScorePoint);
    }

    private void OnDestroy()
    {
        EventAggregator.UnSubscrible<ScorePointChanged>(ScorePoint);
    }
    private void ScorePoint(object sender, ScorePointChanged eventData)
    {
        _scoreText.text = eventData.ScorePoint.ToString();
    }
}
