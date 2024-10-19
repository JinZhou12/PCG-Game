using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Transform pos;
    private SpriteRenderer sr;

    private Vector2 direction; //direction player is facing

    [SerializeField] private GameObject bulletPrefab;
    private Bullet_Controller bulletController;

    [Header("Player Stats")]
    [SerializeField] private float moveSpeed; //how fast player moves
    private float maxMoveSpeed = 10f; //max speed the player can get to

    [SerializeField] private float attackSpeed; //how long before next bullet; shorter is faster
    private float maxAttackSpeed = .25f; //max attack speed for player

    [SerializeField] private float health;

    [SerializeField] private float invulTime; //how long player has been immune to damage
    private float maxInvulTime = 2f; //how long the player is immune to damage after taking damage

    private bool isMoving = false;

    private float firingTime;
    private bool isShooting = false;
    private bool startCounting = false;

    // Start is called before the first frame update
    void Start()
    {
        invulTime = maxInvulTime;

        firingTime = attackSpeed + 1; //load one bullet in the chamber

        if (GetComponent<SpriteRenderer>() != null )
        {
            sr = GetComponent<SpriteRenderer>();
        }

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
        invulTime += Time.deltaTime;

        if (attackSpeed >= maxAttackSpeed)
        {
            attackSpeed = maxAttackSpeed;
        }

        if (moveSpeed >= maxMoveSpeed)
        {
            moveSpeed = maxMoveSpeed;
        }

        if (this.gameObject != null)
        {
            HandleMovement();

            HandleShoot();

            HandleImmune();

            HandleDeath();
        }
    }

    public void HandleImmune()
    {
        if (invulTime <= maxInvulTime)
        {
            StartCoroutine(Invincible());
        }
        else
        {
            StopCoroutine(Invincible());
            if (!sr.enabled)
            {
                sr.enabled = true;
            }
        }
    }

    public void HandleDeath()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
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
            startCounting = true;
        }
        else
        {
            isShooting = false;
        }

        if (startCounting)
        {
            firingTime += Time.deltaTime;
        }

        if (isShooting)
        {
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

    public Vector3 GetPlayerOrientation()
    {
        return pos.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && invulTime >= maxInvulTime)
        {
            health -= collision.gameObject.GetComponent<Homing_Enemy_Controller>().GetDamage();
            invulTime = 0; //reset invulnerability time when taking damage
        }
    }

    IEnumerator Invincible() //Create flashing effect when taking damage
    {
        if (sr.enabled)
        {
            sr.enabled = false;
        }
        else
        {
            sr.enabled = true;
        }

        yield return new WaitForSeconds(1f);
    }
}
