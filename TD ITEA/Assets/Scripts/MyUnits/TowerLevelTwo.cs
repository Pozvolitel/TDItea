using CustomEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLevelTwo : MonoBehaviour, ITakeOfExperianse
{
    private GameObject[] _enemy;
    [SerializeField] private GameObject _pivot;
    [SerializeField] private Transform[] _shootPoint;
    private float _timeSpawn = 1.5f;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private bool isShoot = false;
    private Transform _closest;
    [SerializeField] private int _damage;
    [SerializeField] private int _experience;
    [SerializeField] private GameObject _newLevelPrefab;
    private int shootPoint;
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
        if (_experience >= 3000)
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
            if (!isShoot)
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

    private int ShootPoint()
    {
        if(!isShoot && shootPoint == 1)
        {
            shootPoint = 0;
        }
        else if(!isShoot && shootPoint == 0)
        {
            shootPoint = 1;
        }
        return shootPoint;
    }

    IEnumerator SpawnBullet()
    {
        isShoot = true;
        yield return new WaitForSeconds(_timeSpawn);
        GameObject Bullet = Instantiate(_bullet, _shootPoint[0].position, _shootPoint[0].rotation);
        Bullet.GetComponent<Bullet>().SetDamage(_damage);
        Bullet.GetComponent<Bullet>().ThisTower = this.gameObject;
        yield return new WaitForSeconds(_timeSpawn);
        GameObject BulletTwo = Instantiate(_bullet, _shootPoint[1].position, _shootPoint[1].rotation);
        BulletTwo.GetComponent<Bullet>().SetDamage(_damage);
        BulletTwo.GetComponent<Bullet>().ThisTower = this.gameObject;
        isShoot = false;
    }
}
