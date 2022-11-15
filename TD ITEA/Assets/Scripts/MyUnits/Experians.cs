using CustomEvents;
using UnityEngine;

public class Experians : MonoBehaviour
{
    private int _experience;
    [SerializeField] private Item _item;
    [SerializeField] private SlidersCanvas _slidersCanvas;
    [SerializeField] private int _maxExp;
    [SerializeField] private GameObject _prefabNextLvl;
    public int MaxExp => _maxExp;

    public int Experience => _experience;

    void Start()
    {
        EventAggregator.Subscrible<Experience>(ScoreExperiencePoint);
        _maxExp = _item.MaxExp;
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
            if (_experience >= _maxExp && _item.Level != 3)
            {
                Instantiate(_prefabNextLvl, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
