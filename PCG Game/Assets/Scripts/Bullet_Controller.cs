using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float bulletDamage;
    [SerializeField] private bool canPhase = false;
    private float timeAlive = 0;

    private Vector2 direction;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Abraham");

        Player_Controller playerController = player.GetComponent<Player_Controller>();

        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        direction = playerController.GetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject != null)
        {
            HandleMovement();

            BulletDeath();
        }
    }

    public void BulletDeath()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetBulletSpeed(float newBulletSpeed)
    {
        bulletSpeed = newBulletSpeed;
    }

    public void HandleMovement()
    {
        rb.velocity = direction * bulletSpeed;
    }

    public float GetDamage()
    {
        return bulletDamage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.gameObject.tag == "Player"))
        {
            if (!canPhase)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
