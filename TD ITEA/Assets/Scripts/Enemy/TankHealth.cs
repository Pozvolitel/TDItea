using CustomEvents;
using UnityEngine;

public class TankHealth : MonoBehaviour, ITakeDamage
{
    private ScoreExperienceManager _scoreExperienceManager = new ScoreExperienceManager();

    [SerializeField] private int _health;
    [SerializeField] private GameObject _thisKill;
    [SerializeField] private int _experience;

    public void TakeOnDamage(int damage, GameObject thisKill)
    {
        _health -= damage;
        _thisKill = thisKill;
        if(_health <= 0)
        {
            _scoreExperienceManager.AddScore(_experience, _thisKill);
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
