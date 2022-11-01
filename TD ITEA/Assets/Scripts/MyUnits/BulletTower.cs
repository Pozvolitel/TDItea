using UnityEngine;

public class BulletTower : MonoBehaviour
{
    private float _speed = 150f;
    private int _damage;
    public int Damage => _damage;
    public GameObject ThisTower;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.transform.GetComponent<ITakeScissorsEnemy>() != null)
        {
            other.transform.GetComponent<ITakeScissorsEnemy>().TakeOnDamage(_damage * 3, ThisTower);
            Destroy(gameObject);
        }
        else if (other.transform.GetComponent<ITakePaperEnemy>() != null)
        {
            other.transform.GetComponent<ITakePaperEnemy>().TakeOnDamage(_damage / 2, ThisTower);
            Destroy(gameObject);
        }
        else if (other.transform.GetComponent<ITakeStoneEnemy>() != null)
        {
            other.transform.GetComponent<ITakeStoneEnemy>().TakeOnDamage(_damage, ThisTower);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
