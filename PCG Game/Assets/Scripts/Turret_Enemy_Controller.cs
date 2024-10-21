using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Turret_Enemy_Controller : MonoBehaviour
{
    private Vector2 direction;

    [Header("Enemy Specs")]
    [SerializeField] private float attackSpeed;
    [SerializeField] private float health;

    private float firingTime;

    private GameObject player;

    [SerializeField] private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        firingTime = 0;
        player = GameObject.Find("Abraham");
    }

    // Update is called once per frame
    void Update()
    {
        firingTime += Time.deltaTime;

        if (gameObject != null)
        {
            HandleShoot();

            HandleDeath();
        }
    }
    private void HandleShoot()
    {
        direction = (player.transform.position - this.transform.position).normalized;

        if (firingTime >= attackSpeed)
        {
            Instantiate(bullet, this.transform.position, Quaternion.identity);
            firingTime = 0;
        }
    }
    private void HandleDeath()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= collision.gameObject.GetComponent<Bullet_Controller>().GetDamage();
        }
    }
}
