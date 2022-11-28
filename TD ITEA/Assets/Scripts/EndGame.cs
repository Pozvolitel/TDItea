using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private LifeController _lifeController;
    private int _damage = 100000;
    public int Damage => _damage;
    public GameObject ThisTower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.transform.GetComponent<ITakeScissorsEnemy>() != null)
            {
                other.transform.GetComponent<ITakeScissorsEnemy>().TakeOnDamage(_damage * 3, ThisTower);
            }
            else if (other.transform.GetComponent<ITakePaperEnemy>() != null)
            {
                other.transform.GetComponent<ITakePaperEnemy>().TakeOnDamage(_damage / 2, ThisTower);
            }
            else if (other.transform.GetComponent<ITakeStoneEnemy>() != null)
            {
                other.transform.GetComponent<ITakeStoneEnemy>().TakeOnDamage(_damage, ThisTower);
            }
            _lifeController.AddLife(-1);
        }
    }
}
