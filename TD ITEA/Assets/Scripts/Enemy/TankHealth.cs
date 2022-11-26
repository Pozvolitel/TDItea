using CustomEvents;
using UnityEngine;

public class TankHealth : MonoBehaviour, ITakeScissorsEnemy
{
    private ScoreExperienceManager _scoreExperienceManager = new ScoreExperienceManager();

    [SerializeField] private int _health;
    [SerializeField] private GameObject _thisKill;
    [SerializeField] private int _experience;
    [SerializeField] private SlidersCanvas _slidersCanvas;
    private int _score;
    [SerializeField] private Item _item;

    private void Start()
    {
        _experience = _item.ExperienceBonus;
        _health = _item.Health;
    }

    public void TakeOnDamage(int damage, GameObject thisKill)
    {
        _health -= damage;
        _slidersCanvas.HpValue(_health);
        _thisKill = thisKill;
     
        if(_health <= 0)
        {
            IsDestroy(_experience);
            _scoreExperienceManager.AddScore(_experience, _thisKill);
            Destroy(gameObject);
        }
    }

    private void IsDestroy(int isKill)
    {
        _score += isKill;
        if (_score == _experience)
        {
            FindObjectOfType<GameManager>().AddRandomScore(_experience);
        }        
    }
}

public class ScoreExperienceManager
{
    public int ExperienceScore { get; private set; }
    public GameObject ObjectWin { get; private set; }

    public void AddScore(int scoreExperience, GameObject obj)
    {
        ObjectWin = obj;
        ExperienceScore += scoreExperience;
        PostScoreExperienceChangeEvent();
    }

    public void SetScore(int scoreExperience, GameObject obj)
    {
        ObjectWin = obj;
        ExperienceScore = scoreExperience;
        PostScoreExperienceChangeEvent();
    }

    private void PostScoreExperienceChangeEvent()
    {
        EventAggregator.Post(this, new Experience() { ScoreExperience = ExperienceScore, WinObj = ObjectWin });
    }
}
