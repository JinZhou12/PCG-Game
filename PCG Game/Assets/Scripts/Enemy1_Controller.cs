using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy1_Controller : MonoBehaviour
{
    //Class for enemy 1 which will follow the player around.

    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;

    private Vector2 direction;

    private GameObject player;
    private Player_Controller playerController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Abraham");
        playerController = player.GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = (playerController.transform.position - this.transform.position).normalized;

        HandleMovement();
    }

    public void HandleMovement()
    {
        this.transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
