using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGanner : MonoBehaviour
{
    [SerializeField] private Item _item;
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
    [SerializeField] private Animator _anim;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.AddEnemyObj(this.gameObject);
        _damage = _item.Damage;
        _timeSpawn = _item.TimeSpawn;
    }

    //private void OnDestroy()
    //{
    //    if (FindObjectOfType<GameManager>() != null)
    //        FindObjectOfType<GameManager>().RemoveEnemyObj(this.gameObject);
    //}

    private void Update()
    {
        _tower = GameObject.FindGameObjectsWithTag("PlayerActive");
        if (FindClosestEnemy() != null && Vector3.Distance(transform.position, FindClosestEnemy().position) < 15f)
        {
            _anim.SetBool("Walk", false);
            _navMeshAgent.isStopped = true;
            Vector3 targetRotation = FindClosestEnemy().transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetRotation), 7f * Time.deltaTime);
            _pivot.transform.LookAt(_closest);
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
            _anim.SetBool("Walk", true);
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
            Vector3 targetRotation = _targetPoint[_count].position - transform.position;
            _pivot.transform.rotation = Quaternion.Lerp(_pivot.transform.rotation, Quaternion.LookRotation(targetRotation), 7f * Time.deltaTime);
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
            Bullet.GetComponent<BulletGanner>().SetDamage(_damage);
            Bullet.GetComponent<BulletGanner>().ThisEnemy = this.gameObject;
            _anim.SetTrigger("Attack");
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
