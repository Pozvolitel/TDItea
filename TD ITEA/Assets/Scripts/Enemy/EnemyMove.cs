using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private int _count = 0;
    [SerializeField] private Transform[] _targetPoint;
    [SerializeField] private GameObject[] _tower;
    private Transform _closest;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        _tower = GameObject.FindGameObjectsWithTag("TowerActive");
        if (FindClosestEnemy() != null && Vector3.Distance(transform.position, FindClosestEnemy().position) < 20f)
        {
            _navMeshAgent.isStopped = true;
            transform.LookAt(FindClosestEnemy());
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
}
