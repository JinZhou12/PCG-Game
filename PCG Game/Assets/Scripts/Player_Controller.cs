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
    private GameObject bulletSpawner;

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

    private bool facingRight;
    private Vector3 facingToRight;
    private Vector3 facingToLeft;

    [Header("Obstacle Detection")]
    [SerializeField] private float maxDistance;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    private bool canMoveUp = true;
    private bool canMoveDown = true;

    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform;
        facingToRight = pos.localScale;
        facingToLeft = new Vector3(facingToRight.x * -1, facingToRight.y, facingToRight.z);

        Transform bulletSpawner = this.gameObject.transform.GetChild(1);
        pos = bulletSpawner; //Get location of bullet spawner to use when shooting bullets

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

        if (attackSpeed <= maxAttackSpeed)
        {
            attackSpeed = maxAttackSpeed;
        }

        if (moveSpeed >= maxMoveSpeed)
        {
            moveSpeed = maxMoveSpeed;
        }

        if (this.gameObject != null)
        {
            DetectObstacle();

            HandleMovement();

            HandleShoot();

            HandleImmune();

            HandleDeath();
        }
    }

    private void DetectObstacle()
    {
        //Check all directions to make sure movement is possible
        canMoveLeft = Physics2D.Raycast(transform.position, Vector2.left, maxDistance, 0);
        Debug.Log(canMoveLeft);
        RaycastHit2D checkRight = Physics2D.Raycast(transform.position, Vector2.right, maxDistance, 0);
        RaycastHit2D checkUp = Physics2D.Raycast(transform.position, Vector2.up, maxDistance, 0);
        RaycastHit2D checkDown = Physics2D.Raycast(transform.position, Vector2.down, maxDistance, 0);
    }

    private void HandleImmune()
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
            if (moveLeft && canMoveLeft)
            {
                pos.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                direction = Vector2.left;
                facingRight = false;
            }
            if (moveRight && canMoveRight)
            {
                pos.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                direction = Vector2.right;
                facingRight = true;
            }
            if (moveUp && canMoveUp)
            {
                pos.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                direction = Vector2.up;
            }
            if (moveDown && canMoveDown)
            {
                pos.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                direction = Vector2.down;
            }
        }

        if (!facingRight)
        {
            this.transform.localScale = facingToLeft;
        }
        else
        {
            this.transform.localScale = facingToRight;
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
                }
                else if (shootRight)
                {
                    direction = Vector2.right;
                    Instantiate(bulletPrefab, pos.position, Quaternion.identity);
                }
                else if (shootUp)
                {
                    direction = Vector2.up;
                    Instantiate(bulletPrefab, pos.position, Quaternion.identity);
                }
                else if (shootDown)
                {
                    direction = Vector2.down;
                    Instantiate(bulletPrefab, pos.position, Quaternion.identity);
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
            Debug.Log("Ouch!");
            health -= 1; //collision.gameObject.GetComponent<Homing_Enemy_Controller>().GetDamage() [everything will do 1 damage for now]
            invulTime = 0; //reset invulnerability time when taking damage
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {

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
