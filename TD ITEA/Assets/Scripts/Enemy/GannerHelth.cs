using UnityEngine;

public class GannerHelth : MonoBehaviour, ITakeStoneEnemy
{
    private ScoreExperienceManager _scoreExperienceManager = new ScoreExperienceManager();

    private int _health;
    private GameObject _thisKill;
    private int _experience;
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
        if (_health <= 0)
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
