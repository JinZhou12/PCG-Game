using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Randon_Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Vector2 direction;
    private Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private float timeForNewDirection = 0;
    [SerializeField] private float newDirectionInterval;

    // Start is called before the first frame update
    void Start()
    {
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
        if (collision.gameObject.tag != "Player")
        {
            Debug.Log("bonk :(");
            direction = -direction;
        }
    }
}
