using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Turret_Enemy_Controller : Enemy
{
    private Vector2 direction;

    [Header("Enemy Specs")]
    [SerializeField] private float attackSpeed;
    [SerializeField] private float health;
    public GameObject[] dropItems; 
    public float dropChance = 0.5f;

    private float firingTime;

    private GameObject player;

    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioClip Hurting;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        firingTime = 0;
        player = GameObject.Find("Abraham");
        audioSource = GetComponent<AudioSource>();
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
            DropItem();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= collision.gameObject.GetComponent<Bullet_Controller>().GetDamage();
            audioSource.clip = Hurting;
            audioSource.Play();
        }
    }
    public void DropItem() {
        var _rand = Random.Range(0f,1f);
        if ( _rand <= dropChance)
        {
            int randomIndex = Random.Range(0, dropItems.Length);
            Instantiate(dropItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
