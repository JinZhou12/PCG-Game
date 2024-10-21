using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Room room;

    public void SetRoom(Room currRoom){
        room = currRoom;
    }

    private void OnDestroy() {
        room.RemoveEnemy(this);
    }
}
