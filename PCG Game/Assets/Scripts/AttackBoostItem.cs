using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoostItem : Item
{
    public float attackIncrease = 10f;

    public override void ApplyEffect(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.IncreaseAttack(attackIncrease);
        }
        Destroy(gameObject);
    }
}
