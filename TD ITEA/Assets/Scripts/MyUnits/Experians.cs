using CustomEvents;
using UnityEngine;

public class Experians : MonoBehaviour
{
    [SerializeField] private int _experience;
    [SerializeField] private SlidersCanvas _slidersCanvas;
    [SerializeField] private GameObject _newLevelPrefab;
    [SerializeField] private int _maxExp;
    public int Experience => _experience;

    void Start()
    {
        EventAggregator.Subscrible<Experience>(ScoreExperiencePoint);
    }

    private void OnDestroy()
    {
        EventAggregator.UnSubscrible<Experience>(ScoreExperiencePoint);
    }

    private void ScoreExperiencePoint(object sender, Experience aventData)
    {
        if (this.gameObject == aventData.WinObj)
        {
            _experience += aventData.ScoreExperience;
            _slidersCanvas.ExpValue(_experience);
            if (_experience >= _maxExp)
            {
                Instantiate(_newLevelPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
