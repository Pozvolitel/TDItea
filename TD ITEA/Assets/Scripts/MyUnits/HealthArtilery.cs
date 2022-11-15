using UnityEngine;

public class HealthArtilery : MonoBehaviour, ITakePaperUnit
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
