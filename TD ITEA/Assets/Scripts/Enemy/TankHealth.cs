using CustomEvents;
using UnityEngine;

public class TankHealth : MonoBehaviour, ITakeScissorsEnemy
{
    private ScoreExperienceManager _scoreExperienceManager = new ScoreExperienceManager();

    [SerializeField] private int _health;
    [SerializeField] private GameObject _thisKill;
    [SerializeField] private int _experience;
    [SerializeField] private SlidersCanvas _slidersCanvas;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _slidersCanvas.HpValue(_health);
    }

    public void TakeOnDamage(int damage, GameObject thisKill)
    {
        _health -= damage;
        _slidersCanvas.HpValue(_health);
        _thisKill = thisKill;
        if(_health <= 0)
        {
            _scoreExperienceManager.AddScore(_experience, _thisKill);
            _gameManager.AddRandomScore(_experience);
            Destroy(gameObject);
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
