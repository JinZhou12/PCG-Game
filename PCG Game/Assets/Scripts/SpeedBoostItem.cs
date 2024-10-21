using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostItem : Item
{
    [SerializeField] private float speedIncrease = 2f;
    [SerializeField] private AudioClip speedBoosting;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void ApplyEffect(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.IncreaseSpeed(speedIncrease);
            Debug.Log("1111");
            //Play sound fx
            audioSource.clip = speedBoosting;
            audioSource.Play();
            //Debug.Log("Increasing speed by: " + speedIncrease + ", current speed: " + speedIncrease);
        }
        Destroy(gameObject);
    }
}
