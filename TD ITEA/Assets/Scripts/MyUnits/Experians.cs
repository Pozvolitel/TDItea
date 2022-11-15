using CustomEvents;
using UnityEngine;

public class Experians : MonoBehaviour
{
    private int _experience;
    [SerializeField] private SlidersCanvas _slidersCanvas;
    [SerializeField] private int _maxExpLv1;
    [SerializeField] private int _maxExpLv2;
    public int MaxExpLv1 => _maxExpLv1;
    public int MaxExpLv2 => _maxExpLv2;

    public int Experience => _experience;

    void Start()
    {
        EventAggregator.Subscrible<Experience>(ScoreExperiencePoint);
    }

    private void OnDestroy()
    {
        EventAggregator.UnSubscrible<Experience>(ScoreExperiencePoint);
    }

    private void ScoreExperiencePoint(object sender, Experience eventData)
    {
        if (this.gameObject == eventData.WinObj)
        {
            _experience += eventData.ScoreExperience;
            _slidersCanvas.ExpValue(_experience);
            if (_experience >= _maxExpLv1 && GetComponent<ItemObject>().Level == 1)
            {                
                GetComponent<ItemObject>().LevelUp(2);
                _experience = 0;
            }
            else if (_experience >= _maxExpLv2 && GetComponent<ItemObject>().Level == 2)
            {
                GetComponent<ItemObject>().LevelUp(3);                
            }
            else if (GetComponent<ItemObject>().Level == 3)
            {
                
            }
        }
    }
}
