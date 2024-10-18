using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Transform pos;

    private Vector2 direction; //direction player is facing

    [SerializeField] private GameObject bulletPrefab;
    private Bullet_Controller bulletController; 

    [SerializeField] private float moveSpeed; //how fast player moves
    [SerializeField] private float attackSpeed; //how long before next bullet; shorter is faster

    private bool isMoving = false;

    private float firingTime;
    private bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        firingTime = attackSpeed + 1; //load one bullet in the chamber

        if (GetComponent<Rigidbody2D>() != null)
            rb = GetComponent<Rigidbody2D>();

        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }

        if (GetComponent<Transform>() != null)
        {
            pos = GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        HandleShoot();
    }

    public void HandleMovement()
    {
        bool moveLeft = Input.GetKey(KeyCode.A);
        bool moveRight = Input.GetKey(KeyCode.D);
        bool moveUp = Input.GetKey(KeyCode.W);
        bool moveDown = Input.GetKey(KeyCode.S);

        if (moveLeft || moveRight || moveUp || moveDown)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            if (moveLeft)
            {
                pos.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                direction = Vector2.left;
            }
            if (moveRight)
            {
                pos.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                direction = Vector2.right;
            }
            if (moveUp)
            {
                pos.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                direction = Vector2.up;
            }
            if (moveDown)
            {
                pos.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                direction = Vector2.down;
            }
        }
        else
        {
            //rb.velocity = Vector3.zero;
        }
    }

    public void HandleShoot()
    {
        bool shootLeft = Input.GetKey(KeyCode.LeftArrow);
        bool shootRight = Input.GetKey(KeyCode.RightArrow);
        bool shootUp = Input.GetKey(KeyCode.UpArrow);
        bool shootDown = Input.GetKey(KeyCode.DownArrow);

        if (shootLeft || shootRight || shootUp || shootDown)
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
            firingTime = attackSpeed + 1;
        }

        if (isShooting)
        {
            firingTime += Time.deltaTime;
            if (firingTime >= attackSpeed)
            {
                firingTime = 0;
                if (shootLeft)
                {
                    direction = Vector2.left;
                    Instantiate(bulletPrefab, pos.position, Quaternion.identity);
                    bulletController = bulletPrefab.GetComponent<Bullet_Controller>();
                }
                else if (shootRight)
                {
                    direction = Vector2.right;
                    Instantiate(bulletPrefab, pos.position, Quaternion.identity);
                    bulletController = bulletPrefab.GetComponent<Bullet_Controller>();
                }
                else if (shootUp)
                {
                    direction = Vector2.up;
                    Instantiate(bulletPrefab, pos.position, Quaternion.identity);
                    bulletController = bulletPrefab.GetComponent<Bullet_Controller>();
                }
                else if (shootDown)
                {
                    direction = Vector2.down;
                    Instantiate(bulletPrefab, pos.position, Quaternion.identity);
                    bulletController = bulletPrefab.GetComponent<Bullet_Controller>();
                }
            }
        }
    }

    public Vector2 GetDirection()
    {
        return direction;
    }
}
