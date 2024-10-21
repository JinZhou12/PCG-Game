using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestore : Item
{
    [SerializeField] private float healthRestored = 2f;

    public override void ApplyEffect(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.HealthRestore(healthRestored);
        }
        Destroy(gameObject);
    }
}
