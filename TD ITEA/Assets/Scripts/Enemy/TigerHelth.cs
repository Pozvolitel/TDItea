using UnityEngine;

public class TigerHelth : MonoBehaviour, ITakeStoneEnemy
{
    private ScoreExperienceManager _scoreExperienceManager = new ScoreExperienceManager();

    [SerializeField] private int _health;
    [SerializeField] private GameObject _thisKill;
    [SerializeField] private int _experience;
    [SerializeField] private SlidersCanvas _slidersCanvas;
    private GameManager _gameManager;
    private int _score;
    private TigerDestroy _tigerDestroy;
    [SerializeField] private GameObject _gunnerPrefab;
    private int _lengthGunner = 3;
    [SerializeField] private Transform[] _spawnPoint;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _slidersCanvas.HpValue(_health);
        _tigerDestroy = GetComponent<TigerDestroy>();
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
            Destroy(gameObject, 0.1f);
        }
    }

    private void IsDestroy(int isKill)
    {
        _score += isKill;
        if (_score == _experience)
        {
            _gameManager.AddRandomScore(_experience);

            for (int i = 0; i < _lengthGunner; i++)
            {
                Instantiate(_gunnerPrefab, _spawnPoint[i].position, Quaternion.identity);
            }
        }
    }
}
