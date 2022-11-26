using UnityEngine;

public class BulletGanner : MonoBehaviour
{
    private float _speed = 70f;
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
        if (other.transform.GetComponent<ITakePaperUnit>() != null)
        {
            other.transform.GetComponent<ITakePaperUnit>().TakeDamage(_damage / 2, ThisEnemy);
            Destroy(gameObject);
        }
        else if (other.transform.GetComponent<ITakeScissorsUnit>() != null)
        {
            other.transform.GetComponent<ITakeScissorsUnit>().TakeDamage(_damage * 3, ThisEnemy);
            Destroy(gameObject);
        }
        else if (other.transform.GetComponent<ITakeStoneUnit>() != null)
        {
            other.transform.GetComponent<ITakeStoneUnit>().TakeDamage(_damage, ThisEnemy);
            Destroy(gameObject);
        }
    }    
}
