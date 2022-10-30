using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int _health;
    [SerializeField] private GameObject _thisKill;
    [SerializeField] private int _experience;

    public void TakeOnDamage(int damage, GameObject thisKill)
    {
        _health -= damage;
        _thisKill = thisKill;
        if(_health <= 0)
        {
            _thisKill.GetComponent<ITakeOfExperianse>().NewExperience(_experience);
            Destroy(gameObject);
        }
    }
}
