using UnityEngine;

public class ArtileryHelth : MonoBehaviour, ITakePaperEnemy
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

        if (_health <= 0)
        {
            IsDestroy(_experience);            
            Destroy(gameObject, 0.1f);
        }
    }

    private void IsDestroy(int isKill)
    {
        _score += isKill;
        if (_score == _experience)
        {
            _scoreExperienceManager.AddScore(_experience, _thisKill);
            FindObjectOfType<GameManager>().AddRandomScore(_experience);
            FindObjectOfType<GameManager>().RemoveEnemyObj(this.gameObject);
        }
    }
}
