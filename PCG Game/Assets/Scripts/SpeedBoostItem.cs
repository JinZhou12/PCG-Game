using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostItem : Item
{
    [SerializeField] private float speedIncrease = 2f;

    public override void ApplyEffect(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.IncreaseSpeed(speedIncrease);
            //Debug.Log("Increasing speed by: " + speedIncrease + ", current speed: " + speedIncrease);
        }
        Destroy(gameObject);
    }
}
