using System.Collections;
using UnityEngine;

public class TowerArtilery : MonoBehaviour
{
    private GameObject[] _enemy;
    [SerializeField] private Transform _pivot;
    [SerializeField] private Transform _shootPoint;
    private float _timeSpawn;
    [SerializeField] private GameObject _bullet;
    private bool isShoot = false;
    private Transform _closest;
    private int _damage;
    [SerializeField] private float _power;
    private float _garavity = Physics.gravity.y;
    private float _bulletVelocity;

    private void Start()
    {
        _damage = GetComponent<ItemObject>().Damage;
        _timeSpawn = GetComponent<ItemObject>().TimeSpawn;
    }

    private void OnDestroy()
    {
        gameObject.tag = "Player";
    }

    void Update()
    {
        _enemy = GameObject.FindGameObjectsWithTag("EnemyTank");
        if (FindClosestEnemy() != null && Vector3.Distance(transform.position, FindClosestEnemy().position) < 70f)
        {
            Vector3 targetRotation = FindClosestEnemy().transform.position - transform.position;
            _pivot.transform.rotation = Quaternion.Lerp(_pivot.transform.rotation, Quaternion.LookRotation(targetRotation), 7f * Time.deltaTime);

            if (!isShoot)
            {
                Shot();
                gameObject.tag = "PlayerActive";
                StartCoroutine(SpawnBullet());
            }
        }
        else
        {
            gameObject.tag = "Player";
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
            Rigidbody Bullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            Bullet.velocity = _shootPoint.forward * _bulletVelocity;
            Bullet.GetComponent<BulletArtilert>().SetDamage(_damage);
            Bullet.GetComponent<BulletArtilert>().ThisTower = this.gameObject;
        }
        isShoot = false;
    }
}
