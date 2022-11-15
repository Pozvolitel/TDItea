using CustomEvents;
using UnityEngine;

public class HelthTowerUnit : MonoBehaviour, ITakeStoneUnit, ITakeHealth
{
    private ExperienceManager _experienceManager = new ExperienceManager();
    private int _health;
    private GameObject _thisKill;
    private int _experience;
    [SerializeField] private SlidersCanvas _slidersCanvas;

    private void Start()
    {
        _experience = GetComponent<ItemObject>().Experience;
        _health = GetComponent<ItemObject>().Health;
        _slidersCanvas.HpValue(_health);
    }

    public void NewHealth(int health)
    {
        _health = health;
    }

    public void TakeDamage(int damage, GameObject thisKill)
    {
        _health -= damage;

        _slidersCanvas.HpValue(_health);
        _thisKill = thisKill;
        if (_health <= 0)
        {
            _experienceManager.AddScore(_experience, _thisKill);
            Destroy(gameObject);
        }
    }
}

public class ExperienceManager
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
