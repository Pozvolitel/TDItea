using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ArtyleryTank : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private int _count = 0;
    [SerializeField] private Transform[] _targetPoint;
    [SerializeField] private GameObject[] _tower;
    private Transform _closest;
    private bool isShoot;
    private float _timeSpawn = 5f;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _pivot;
    [SerializeField] private int _damage;
    private float _bulletVelocity;
    private float _garavity = Physics.gravity.y;
    [SerializeField] private float _power;
    private GameManager _gameManager;
    [SerializeField] private Item _item;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.AddEnemyObj(this.gameObject);
        _damage = _item.Damage;
        _timeSpawn = _item.TimeSpawn;
    }

    private void OnDestroy()
    {
        _gameManager.RemoveEnemyObj(this.gameObject);
    }

    private void Update()
    {
        _tower = GameObject.FindGameObjectsWithTag("PlayerActive");
        if (FindClosestEnemy() != null && Vector3.Distance(transform.position, FindClosestEnemy().position) < 50f && Vector3.Distance(transform.position, FindClosestEnemy().position) > 10f)
        {
            _navMeshAgent.isStopped = true;
            Vector3 targetRotation = FindClosestEnemy().transform.position - transform.position;
            _pivot.transform.rotation = Quaternion.Lerp(_pivot.transform.rotation, Quaternion.LookRotation(targetRotation), 7f * Time.deltaTime);
            if (!isShoot)
            {
                Shot();
                StartCoroutine(SpawnBullet());
            }
        }
        else
        {
            _navMeshAgent.isStopped = false;
            MoveToTarget();
        }
    }

    private void Shot()
    {
        Vector3 fromTo = FindClosestEnemy().position - _pivot.transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float angleinRadians = _power * Mathf.PI / 180;

        float v2 = (_garavity * x * x) / (2 * (y - Mathf.Tan(angleinRadians) * x) * Mathf.Pow(Mathf.Cos(angleinRadians), 2));
        _bulletVelocity = Mathf.Sqrt(Mathf.Abs(v2));
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
            Rigidbody Bullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation).GetComponent<Rigidbody>();
            Bullet.velocity = _shootPoint.forward * _bulletVelocity;
            Bullet.GetComponent<BulletArtEnemy>().SetDamage(_damage);
            Bullet.GetComponent<BulletArtEnemy>().ThisEnemy = this.gameObject;
        }
        isShoot = false;
    }
}
