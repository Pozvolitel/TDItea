using UnityEngine;

public class TigerHelth : MonoBehaviour, ITakeStoneEnemy
{
    private ScoreExperienceManager _scoreExperienceManager = new ScoreExperienceManager();

    private int _health;
    [SerializeField] private GameObject _thisKill;
    private int _experience;
    [SerializeField] private SlidersCanvas _slidersCanvas;
    private GameManager _gameManager;
    private int _score;
    [SerializeField] private GameObject _gunnerPrefab;
    private int _lengthGunner = 3;
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private Item _item;

    
    private void Start()
    {
        _experience = _item.ExperienceBonus;
        _health = _item.Health;
        _gameManager = FindObjectOfType<GameManager>();
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
            _gameManager.AddRandomScore(_experience);

            for (int i = 0; i < _lengthGunner; i++)
            {
                Instantiate(_gunnerPrefab, _spawnPoint[i].position, Quaternion.identity);
            }
        }
    }
}
