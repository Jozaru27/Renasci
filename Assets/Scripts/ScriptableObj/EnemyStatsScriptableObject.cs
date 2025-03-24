using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStatsScriptableObject : ScriptableObject
{
    [Header("General Info")]
    public string enemyName;

    [Header("Character Stats")]
    public float life;
    public float mainDamage;
    public float secondaryDamage;
    public float heal;
    public float movementSpeed;
    public float actionSpeed;
    public float pushForce;
    public float detectionDistance;
    public float pushedForce;
}
