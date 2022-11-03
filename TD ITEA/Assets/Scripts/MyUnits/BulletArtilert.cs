using UnityEngine;
using UnityEngine.AI;

public class BulletArtilert : MonoBehaviour
{
    private float _radius = 400;
    private float _force = 15007;

    private void Explosion()
    {
        Collider[] overlappedCollider = Physics.OverlapSphere(transform.position, _radius);
        NavMeshAgent[] nav = FindObjectsOfType<NavMeshAgent>();
        for (int i = 0; i < overlappedCollider.Length; i++)
        {
            nav[i].enabled = false;
            Rigidbody rig = overlappedCollider[i].attachedRigidbody;
            if (rig)
            {
                rig.AddExplosionForce(_force, transform.position, _radius);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Explosion();
        Destroy(gameObject, 0.1f);
    }
}
