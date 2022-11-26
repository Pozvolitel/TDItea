using UnityEngine;

public class BlockHealth : MonoBehaviour, ITakeScissorsUnit
{
    private int _health = 2000;

    public void TakeDamage(int damage, GameObject thisKill)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
