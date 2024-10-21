using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Randon_Movement : MonoBehaviour
{
    [Header("Enemy Specs")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;

    // public bool isBoss;
    private int direction;
    private Vector2[] directions = { Vector2.up, Vector2.left, Vector2.down, Vector2.right };

    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask collisionLayers;
    private bool[] canMove = {false, false, false, false};

    private float timeForNewDirection = 0;
    [SerializeField] private float newDirectionInterval;

    // Start is called before the first frame update
    void Start()
    {
        // health = Random.Range(1, 6); //enemies with random movement have 1 to 5 health

        direction = Random.Range(0, directions.Length);
    }

    // Update is called once per frame
    void Update()
    {
        DetectObstacle();
        HandleMovement();
    }

    private void DetectObstacle()
    {
        //Check all directions to make sure movement is possible
        canMove[1] = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), maxDistance, collisionLayers);
        //Debug.DrawRay(playerCenter.position, transform.TransformDirection(Vector2.left) * maxDistance, Color.red);
        canMove[3] = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), maxDistance, collisionLayers);
        canMove[0] = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), maxDistance, collisionLayers);
        canMove[2] = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), maxDistance, collisionLayers);
        /*IMPORTANT NOTE:
         * Physics2D.Raycast will return true when an object is detected and false when
         * no object is detected.
         * This is why I put !canMoveLeft in the HandleMovement() method
        */
    }

    private void HandleDeath()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void HandleMovement()
    {
        timeForNewDirection += Time.deltaTime;
        if (timeForNewDirection >= newDirectionInterval)
        {
            direction = Random.Range(0, directions.Length);
            timeForNewDirection = 0;
        }

        if (canMove[direction]) return; // true implies object
        this.transform.Translate(directions[direction] * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            // Debug.Log(collision.gameObject);
            direction = (direction + 2) % 4;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            // Debug.Log(collision.gameObject);
            health -= collision.gameObject.GetComponent<Bullet_Controller>().GetDamage();
            HandleDeath();
        }
    }
}
