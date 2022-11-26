using UnityEngine;

public class BulletArtEnemy : MonoBehaviour
{
    private float _radius = 6f;
    private int _damage;
    public int Damage => _damage;
    public GameObject ThisEnemy;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void Explosion()
    {
        Collider[] overlappedCollider = Physics.OverlapSphere(transform.position, _radius);
        for (int i = 0; i < overlappedCollider.Length; i++)
        {
            Rigidbody rig = overlappedCollider[i].attachedRigidbody;
            if (rig)
            {
                if (rig.transform.GetComponent<ITakePaperUnit>() != null)
                {
                    rig.transform.GetComponent<ITakePaperUnit>().TakeDamage(_damage, ThisEnemy);
                    Destroy(gameObject);
                }
                else if (rig.transform.GetComponent<ITakeScissorsUnit>() != null)
                {
                    rig.transform.GetComponent<ITakeScissorsUnit>().TakeDamage(_damage / 2, ThisEnemy);
                    Destroy(gameObject);
                }
                else if (rig.transform.GetComponent<ITakeStoneUnit>() != null)
                {
                    rig.transform.GetComponent<ITakeStoneUnit>().TakeDamage(_damage * 3, ThisEnemy);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Explosion();
    }
}
