using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int Damage;
    public int Health;
    public float TimeSpawn;
    public int Level;
    public int MaxExp;
    public int ExperienceBonus;
}
