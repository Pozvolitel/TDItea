using CustomEvents;
using System.Collections;
using UnityEngine;

public class TowerLevelOne : MonoBehaviour, ITakeOfExperianse
{
    private GameObject[] _enemy;
    [SerializeField] private GameObject _pivot;
    [SerializeField] private Transform _shootPoint;
    private float _timeSpawn = 3f;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private bool isShoot = false;
    private Transform _closest;
    [SerializeField] private int _damage;
    [SerializeField] private int _experience;
    [SerializeField] private GameObject _newLevelPrefab;
    public int Experience => _experience;

    void Start()
    {        
        EventAggregator.Subscrible<AttackTag>(NewAttackTag);
    }

    private void OnDestroy()
    {
        EventAggregator.UnSubscrible<AttackTag>(NewAttackTag);
        gameObject.tag = "Tower";
    }

    public void NewExperience(int experience)
    {
        _experience += experience;
        if(_experience >= 1000)
        {
            Instantiate(_newLevelPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        _enemy = GameObject.FindGameObjectsWithTag("EnemyTank");
        if (FindClosestEnemy() != null && Vector3.Distance(transform.position, FindClosestEnemy().position) < 20f)
        {
            _pivot.transform.LookAt(FindClosestEnemy());
            if(!isShoot)
            {
                StartCoroutine(SpawnBullet());
            }
            EventAggregator.Post(this, new AttackTag());
        }
        else
        {
            gameObject.tag = "Tower";
        }
    }
    private Transform FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in _enemy)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                _closest = go.transform;
                distance = curDistance;
            }
        }
        return _closest;
    }

    private void NewAttackTag(object sender, AttackTag aventData)
    {
        gameObject.tag = "TowerActive";
    }

    IEnumerator SpawnBullet()
    {
        isShoot = true;
        yield return new WaitForSeconds(_timeSpawn);
        GameObject Bullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
        Bullet.GetComponent<Bullet>().SetDamage(_damage);
        Bullet.GetComponent<Bullet>().ThisTower = this.gameObject;
        isShoot = false;
    }
}
