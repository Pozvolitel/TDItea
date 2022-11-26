using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GannerVSU : MonoBehaviour
{
    private GameObject[] _enemy;
    [SerializeField] private Transform _shootPoint;
    private float _timeSpawn;
    private float _timeShoot = 3f;
    [SerializeField] private GameObject _bullet;
    private bool isShoot = false;
    private Transform _closest;
    private int _damage;
    private NavMeshAgent _navMeshAgent;
    private GameObject[] _targetPoint;
    [SerializeField] private Item _item;
    private Transform _closestTarget;
    private bool _isTarget = false;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _damage = _item.Damage;
        _timeSpawn = _item.TimeSpawn;
    }

    private void OnDestroy()
    {
        gameObject.tag = "Player";
    }

    void Update()
    {
        _enemy = GameObject.FindGameObjectsWithTag("EnemyTank");
        _targetPoint = GameObject.FindGameObjectsWithTag("TargetFree");         

        if(FindClosestTarget() && !_isTarget)
        {
            if (Vector3.Distance(transform.position, FindClosestTarget().position) > 5f)
            {
                _navMeshAgent.SetDestination(_closestTarget.position);
            }            
        }
        else
        {
            _navMeshAgent.isStopped = true;
        }

        if (FindClosestEnemy() != null && Vector3.Distance(transform.position, FindClosestEnemy().position) < 25f)
        {
            transform.LookAt(_closest);
            _timeShoot -= Time.deltaTime;
            if (!isShoot && _timeShoot > 0)
            {
                gameObject.tag = "PlayerActive";
                StartCoroutine(SpawnBullet());
            }
            else if (_timeShoot <= 0)
            {
                StartCoroutine(ReloadTimeYield());
            }
        }
        else
        {
            gameObject.tag = "Player";
        }
    }

    private Transform FindClosestTarget()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        if (_targetPoint != null)
        {
            foreach (GameObject go in _targetPoint)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    _closestTarget = go.transform;
                    distance = curDistance;
                }
            }
        }
        return _closestTarget;
    }

    private Transform FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        if (_enemy != null)
        {
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
        }
        return _closest;
    }

    IEnumerator SpawnBullet()
    {
        isShoot = true;
        yield return new WaitForSeconds(_timeSpawn);
        if (_closest != null)
        {
            GameObject Bullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
            Bullet.GetComponent<BulletVSU>().SetDamage(_damage);
            Bullet.GetComponent<BulletVSU>().ThisTower = this.gameObject;
        }
        isShoot = false;
    }

    IEnumerator ReloadTimeYield()
    {
        var _reloadTime = 2f;
        yield return new WaitForSeconds(_reloadTime);
        _timeShoot = 3f;
    }
}
