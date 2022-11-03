using UnityEngine;

public class GannerHelth : MonoBehaviour, ITakeStoneEnemy
{
    private ScoreExperienceManager _scoreExperienceManager = new ScoreExperienceManager();

    [SerializeField] private int _health;
    [SerializeField] private GameObject _thisKill;
    [SerializeField] private int _experience;
    [SerializeField] private SlidersCanvas _slidersCanvas;
    private GameManager _gameManager;
    private int _score;

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
        if (_health <= 0)
        {
            _scoreExperienceManager.AddScore(_experience, _thisKill);            
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        _gameManager.AddRandomScore(_experience);
    }
}
