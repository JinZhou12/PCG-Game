using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    /*
    public float attackPower = 10f;  //Attack
    public float speed = 5f;         //Speed
    */

    Player_Controller controller;

    private void Start(){
        controller = GetComponent<Player_Controller>();
        
    }

    //Increase attack
    public void IncreaseAttack(float amount)
    {
        controller.ChangeAttackSpeed(amount);
        
    }

    //Increasing speed
    public void IncreaseSpeed(float amount)
    {
        controller.ChangeMoveSpeed(amount);
    }

    public void HealthRestore(float amount)
    {
        controller.ChangeHealth(amount);
    }
}
