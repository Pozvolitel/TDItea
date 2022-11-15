using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGanner : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private int _count = 0;
    [SerializeField] private Transform[] _targetPoint;
    [SerializeField] private GameObject[] _tower;
    private Transform _closest;
    private bool isShoot;
    private float _timeSpawn;
    private float _timeShoot = 3f;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;
    private int _damage;
    private GameManager _gameManager;
    [SerializeField] private Transform _pivot;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.AddEnemyObj(this.gameObject);
        _damage = GetComponent<ItemObject>().Damage;
        _timeSpawn = GetComponent<ItemObject>().TimeSpawn;
    }

    private void OnDestroy()
    {
        if(_gameManager != null)
        FindObjectOfType<GameManager>().RemoveEnemyObj(this.gameObject);
    }

    private void Update()
    {
        _tower = GameObject.FindGameObjectsWithTag("PlayerActive");
        if (FindClosestEnemy() != null && Vector3.Distance(transform.position, FindClosestEnemy().position) < 10f)
        {
            _navMeshAgent.isStopped = true;
            Vector3 targetRotation = FindClosestEnemy().transform.position - transform.position;
            _pivot.transform.rotation = Quaternion.Lerp(_pivot.transform.rotation, Quaternion.LookRotation(targetRotation), 7f * Time.deltaTime);
            _timeShoot -= Time.deltaTime;
            if (!isShoot && _timeShoot > 0)
            {
                StartCoroutine(SpawnBullet());
            }
            else if(_timeShoot <= 0)
            {
                StartCoroutine(ReloadTimeYield());
            }
        }
        else
        {
            _navMeshAgent.isStopped = false;
            MoveToTarget();
        }
    }

    private Transform FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in _tower)
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

    private void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, _targetPoint[_count].position) < 3f)
        {
            if (_count < _targetPoint.Length)
            {
                _count++;
            }
        }

        if (_count < _targetPoint.Length)
        {
            _navMeshAgent.SetDestination(_targetPoint[_count].position);
        }
        else
        {
            _count = _targetPoint.Length;
            _navMeshAgent.isStopped = true;
        }
    }
    IEnumerator SpawnBullet()
    {
        isShoot = true;
        yield return new WaitForSeconds(_timeSpawn);
        if (FindClosestEnemy() != null)
        {
            _damage = GetComponent<ItemObject>().Damage;
            _timeSpawn = GetComponent<ItemObject>().TimeSpawn;
            GameObject Bullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
            Bullet.GetComponent<BulletGanner>().SetDamage(_damage);
            Bullet.GetComponent<BulletGanner>().ThisEnemy = this.gameObject;
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
