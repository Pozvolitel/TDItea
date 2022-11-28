using UnityEngine;

public class BulletArtilert : MonoBehaviour
{
    private float _radius = 8f;
    public GameObject ThisTower;
    private int _damage;
    public int Damage => _damage;

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
                if (rig.transform.GetComponent<ITakeScissorsEnemy>() != null)
                {
                    rig.transform.GetComponent<ITakeScissorsEnemy>().TakeOnDamage(_damage / 2, ThisTower);
                    Destroy(gameObject);
                }
                else if (rig.transform.GetComponent<ITakePaperEnemy>() != null)
                {
                    rig.transform.GetComponent<ITakePaperEnemy>().TakeOnDamage(_damage, ThisTower);
                    Destroy(gameObject);
                }
                else if (rig.transform.GetComponent<ITakeStoneEnemy>() != null)
                {
                    rig.transform.GetComponent<ITakeStoneEnemy>().TakeOnDamage(_damage * 3, ThisTower);
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
