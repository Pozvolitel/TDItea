using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GannerVSU : MonoBehaviour
{
    private GameObject[] _enemy;
    [SerializeField] private Transform _shootPoint;
    private float _timeSpawn = 0.05f;
    private float _timeShoot = 3f;
    [SerializeField] private GameObject _bullet;
    private bool isShoot = false;
    private Transform _closest;
    [SerializeField] private int _damage;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform[] _targetPoint;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    private void OnDestroy()
    {
        gameObject.tag = "Player";
    }

    void Update()
    {
        if(_targetPoint[0] != null)
        {
            _navMeshAgent.SetDestination(_targetPoint[0].position);
        }
        else if (_targetPoint[1] != null)
        {
            _navMeshAgent.SetDestination(_targetPoint[1].position);
        }
        else
        {
            _navMeshAgent.isStopped = true;
        }
        _enemy = GameObject.FindGameObjectsWithTag("EnemyTank");

        if (FindClosestEnemy() != null && Vector3.Distance(transform.position, FindClosestEnemy().position) < 25f)
        {
            Vector3 targetRotation = FindClosestEnemy().transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetRotation), 7f * Time.deltaTime);
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

    IEnumerator SpawnBullet()
    {
        isShoot = true;
        yield return new WaitForSeconds(_timeSpawn);
        if (FindClosestEnemy() != null)
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

    public void TransformNextTarget()
    {
        _navMeshAgent.SetDestination(_targetPoint[1].position);
    }
}
