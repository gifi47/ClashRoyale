using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityType", menuName = "Entity/New Building")]
public class BuildingType : ScriptableObject
{
    // MAX STATS VALUES
    public float maxHealth = 100f;

    // =====================================
    // =====================================

    // BASE STATS VALUES
    public float baseHealth = 100f;
    public float baseAttackDelay = 0.3f;
    public float baseAttackEndDelay = 0.2f;
    public float baseAttackRange = 1.25f;

    public float baseDamage = 20f;

    // =====================================
    // =====================================
}
