using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTile : MonoBehaviour
{
    private GameObject activeRoom;
    private GameObject inactiveRoom;
    // Start is called before the first frame update
    void Awake()
    {
        activeRoom = transform.Find("Active").gameObject;
        inactiveRoom = transform.Find("Inactive").gameObject;
    }

    public void ActivateTile(){
        activeRoom.SetActive(true);
        inactiveRoom.SetActive(false);
    }

    public void DeactivateTile(){
        activeRoom.SetActive(false);
        inactiveRoom.SetActive(true);
    }
}
