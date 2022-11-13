using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private int _count = 0;
    [SerializeField] private Transform[] _targetPoint;
    [SerializeField] private GameObject[] _tower;
    private Transform _closest;
    private bool isShoot;
    [SerializeField] private float _timeSpawn;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _pivot;
    [SerializeField] private int _damage;
    private GameManager _gameManager;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.AddEnemyObj(this.gameObject);
    }

    private void OnDestroy()
    {
        _gameManager.RemoveEnemyObj(this.gameObject);
    }

    private void Update()
    {
        _tower = GameObject.FindGameObjectsWithTag("PlayerActive");
        if (FindClosestEnemy() != null && Vector3.Distance(transform.position, FindClosestEnemy().position) < 20f)
        {
            _navMeshAgent.isStopped = true;
            Vector3 targetRotation = FindClosestEnemy().transform.position - transform.position;
            _pivot.transform.rotation = Quaternion.Lerp(_pivot.transform.rotation, Quaternion.LookRotation(targetRotation), 7f * Time.deltaTime);
            if (!isShoot)
            {
                StartCoroutine(SpawnBullet());
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
            GameObject Bullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
            Bullet.GetComponent<BulletTank>().SetDamage(_damage);
            Bullet.GetComponent<BulletTank>().ThisEnemy = this.gameObject;
        }
        isShoot = false;
    }
}
