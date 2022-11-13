using System.Collections;
using UnityEngine;

public class TowerLevelOne : MonoBehaviour
{
    private GameObject[] _enemy;
    [SerializeField] private GameObject _pivot;
    [SerializeField] private Transform _shootPoint;
    private float _timeSpawn = 2f;
    [SerializeField] private GameObject _bullet;
    private bool isShoot = false;
    private Transform _closest;
    [SerializeField] private int _damage;
    
    private void OnDestroy()
    {
        gameObject.tag = "Player";
    }    

    void Update()
    {
        _enemy = GameObject.FindGameObjectsWithTag("EnemyTank");
        if (FindClosestEnemy() != null && Vector3.Distance(transform.position, FindClosestEnemy().position) < 20f)
        {
            Vector3 targetRotation = FindClosestEnemy().transform.position - _pivot.transform.position;
           _pivot.transform.rotation = Quaternion.Lerp(_pivot.transform.rotation, Quaternion.LookRotation(targetRotation), 7f * Time.deltaTime);
            
            if (!isShoot)
            {
                gameObject.tag = "PlayerActive";
                StartCoroutine(SpawnBullet());
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
        if (_closest != null)
        {
            GameObject Bullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
            Bullet.GetComponent<BulletTower>().SetDamage(_damage);
            Bullet.GetComponent<BulletTower>().ThisTower = this.gameObject;
        }
        isShoot = false;
    }
}