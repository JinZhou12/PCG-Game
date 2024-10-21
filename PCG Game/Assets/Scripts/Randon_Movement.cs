using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Randon_Movement : MonoBehaviour
{
    [Header("Enemy Specs")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;

    private Vector2 direction;
    private Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private float timeForNewDirection = 0;
    [SerializeField] private float newDirectionInterval;

    // Start is called before the first frame update
    void Start()
    {
        health = Random.Range(1, 6); //enemies with random movement have 1 to 5 health

        CreateNewDirection();
    }

    // Update is called once per frame
    void Update()
    {
        timeForNewDirection += Time.deltaTime;

        if (timeForNewDirection >= newDirectionInterval)
        {
            CreateNewDirection();
            timeForNewDirection = 0;
        }

        HandleMovement();
    }
    private void HandleDeath()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void CreateNewDirection()
    {
        direction = directions[Random.Range(0, directions.Length)];
    }

    private void HandleMovement()
    {
        this.transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet")
        {
            Debug.Log("bonk :(");
            direction = -direction;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            health -= collision.gameObject.GetComponent<Bullet_Controller>().GetDamage();
        }
    }
}
