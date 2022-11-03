using UnityEngine;

public class HealthGanner : MonoBehaviour, ITakeScissorsUnit
{
    private ExperienceManager _experienceManager = new ExperienceManager();
    [SerializeField] private int _health;
    [SerializeField] private GameObject _thisKill;
    [SerializeField] private int _experience;
    [SerializeField] private SlidersCanvas _slidersCanvas;

    private void Start()
    {
        _slidersCanvas.HpValue(_health);
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
