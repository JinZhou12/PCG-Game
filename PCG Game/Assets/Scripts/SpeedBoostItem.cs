using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostItem : Item
{
    public float speedIncrease = 2f;

    public override void ApplyEffect(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.IncreaseSpeed(speedIncrease);
        }
        Destroy(gameObject);
    }
}
