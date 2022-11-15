using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private float _timeSpawn;
    [SerializeField] private int _level;
    [SerializeField] private int _experience;

    public int Level => _level;
    public int Damage => _damage;
    public int Health => _health;
    public float TimeSpawn => _timeSpawn;
    public int Experience => _experience;

    public void LevelUp(int level)
    {
        _level = level;

        if(_level == 2)
        {
            _damage *= 2;
            _health *= 2;
            _timeSpawn /= 2;
            _experience *= 2;
            GetComponent<ITakeHealth>().NewHealth(_health);
        }
        else if (_level == 3)
        {
            _damage *= 2;
            _health *= 2;
            _timeSpawn /= 2;
            _experience *= 2;
            GetComponent<ITakeHealth>().NewHealth(_health);
        }
    }
}
