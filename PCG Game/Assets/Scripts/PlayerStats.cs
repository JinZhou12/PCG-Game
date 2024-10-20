using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float attackPower = 10f;  //Attack
    public float speed = 5f;         //Speed

    //Increase attack
    public void IncreaseAttack(float amount)
    {
        attackPower += amount;
        Debug.Log("Increasing attack by: " + amount + ", current attack: " + attackPower);
    }

    //Increasing speed
    public void IncreaseSpeed(float amount)
    {
        speed += amount;
        Debug.Log("Increasing speed by: " + amount + ", current speed: " + speed);
    }

    public void DecreaseAttack(float amount)
    {
        attackPower = Mathf.Max(0, attackPower - amount);
        Debug.Log("攻击力减少: " + amount + "，当前攻击力: " + attackPower);
    }

    public void DecreaseSpeed(float amount)
    {
        speed = Mathf.Max(0, speed - amount);
        Debug.Log("速度减少: " + amount + "，当前速度: " + speed);
    }
}
