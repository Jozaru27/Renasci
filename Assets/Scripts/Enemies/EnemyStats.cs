using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("General Info")]
    public string enemyName;

    public enum enemyTypes
    {
        Healer,
        SkeletonArcher,
        SkeletonMage,
        SkeletonWarrior,
        Snake,
        StoneGolem
    }
    public enemyTypes enemy;

    [Header("CharacterStats")]
    public float life;
    public float mainDamage;
    public float secondaryDamage;
    public float heal;
    public float movementSpeed;
    public float actionSpeed;
    public float pushForce;
    public float detectionDistance;
}
