using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Turret_Enemy_Controller : MonoBehaviour
{
    private Vector2 direction;

    [SerializeField] private float attackSpeed;

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

        HandleShoot();
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
}
