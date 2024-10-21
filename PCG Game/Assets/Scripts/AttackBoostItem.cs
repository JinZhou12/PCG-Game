using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackBoostItem : Item
{
    [SerializeField] private float attackIncrease = 2f;

    public override void ApplyEffect(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.IncreaseAttack(attackIncrease);
            Debug.Log("1111");
            //audioSource.SetScheduledEndTime(AudioSettings.dspTime + duration);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider){

        if (collider.CompareTag("Player")){

            ApplyEffect(collider.gameObject);

        }

    }
}
