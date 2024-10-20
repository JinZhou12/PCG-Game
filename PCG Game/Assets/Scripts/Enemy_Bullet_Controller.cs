using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Controller : MonoBehaviour
{
    private GameObject playerCenter; //turret will aim at the center of the player

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float bulletDamage;

    private float timeAlive = 0;

    private Vector2 direction;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        playerCenter = GameObject.Find("Abraham Center");

        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        direction = Vector3.Normalize(playerCenter.transform.position - this.transform.position);
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
        if (!(collision.gameObject.tag == "Enemy"))
        {
            Destroy(this.gameObject, .1f); //destroy after dealing damage
        }
    }
}
