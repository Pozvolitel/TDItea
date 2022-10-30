using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    void TakeOnDamage(int damage, GameObject thisKill);
}
