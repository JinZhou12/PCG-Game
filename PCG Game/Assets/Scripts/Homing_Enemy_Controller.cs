using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Homing_Enemy_Controller : MonoBehaviour
{
    //Class for Homing enemies

    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float health;

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
        if (this.gameObject != null)
        {
            direction = (playerController.transform.position - this.transform.position).normalized;

            HandleMovement();

            HandleDeath();
        }
    }

    public void HandleMovement()
    {
        this.transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    public void HandleDeath()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public float GetDamage()
    {
        return damage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= collision.gameObject.GetComponent<Bullet_Controller>().GetDamage();
        }
    }
}
