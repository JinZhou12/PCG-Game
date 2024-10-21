using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Room room;

    public void SetRoom(Room currRoom){
        room = currRoom;
    }

    private void OnDestroy() {
        if (gameObject != null)
        {
            room.RemoveEnemy(this);
        }
    }
}
