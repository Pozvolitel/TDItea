using UnityEngine;

public class Bullet : MonoBehaviour
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
        ITakeDamage takeDamage = other.transform.GetComponent<ITakeDamage>();

        if (takeDamage != null)
        {
            other.transform.GetComponent<ITakeDamage>().TakeOnDamage(_damage, ThisTower);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
