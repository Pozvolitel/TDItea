using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private LifeController _lifeController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            _lifeController.AddLife(-1);
        }
    }
}
