using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestore : Item
{
    [SerializeField] private float healthRestored = 2f;

    public override void ApplyEffect(GameObject player)
    {
        Player_Controller stats = player.GetComponent<Player_Controller>();
        // Do nothing is health is full
        if (stats.GetHealth() < 3){
            if (stats != null)
            {
                stats.ChangeHealth(healthRestored);
            }
            audioSource.Play();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider){

        if (collider.CompareTag("Player")){
            ApplyEffect(collider.gameObject);
        }

    }
}
