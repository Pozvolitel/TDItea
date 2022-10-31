using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private float _speed = 150f;
    private int _damage;
    public int Damage => _damage;
    public GameObject ThisEnemy;

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
        InterfaceUnitHP takeDamage = other.transform.GetComponent<InterfaceUnitHP>();

        if (takeDamage != null)
        {
            other.transform.GetComponent<InterfaceUnitHP>().TakeDamage(_damage, ThisEnemy);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
