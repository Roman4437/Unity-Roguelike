using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Default Stats", menuName = "Enemy Default Stats")]
public class EnemyDefaultStats : ScriptableObject
{
    public float health;
    public float damageMitigation;
    public float damage;
    public float movementSpeed;
}