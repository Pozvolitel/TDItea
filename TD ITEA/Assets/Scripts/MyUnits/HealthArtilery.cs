using UnityEngine;

public class HealthArtilery : MonoBehaviour, ITakePaperUnit
{
    private ExperienceManager _experienceManager = new ExperienceManager();
    private int _health;
    private GameObject _thisKill;
    private int _experience;
    [SerializeField] private SlidersCanvas _slidersCanvas;
    [SerializeField] private Item _item;
    private int _score;

    private void Start()
    {
        _experience = _item.ExperienceBonus;
        _health = _item.Health;
    }

    public void TakeDamage(int damage, GameObject thisKill)
    {
        _health -= damage;
        _slidersCanvas.HpValue(_health);
        _thisKill = thisKill;
        if (_health <= 0)
        {
            IsDestroy(_experience);
            Destroy(gameObject);
        }
    }

    private void IsDestroy(int isKill)
    {
        _score += isKill;
        if (_score == _experience)
        {
            _experienceManager.AddScore(_experience, _thisKill);
        }
    }
}
