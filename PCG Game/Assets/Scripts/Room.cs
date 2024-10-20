using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Order: right, top, left, down
    [SerializeField] private Transform[] playerSpawnPositions;
    private bool[] doorStatus = new bool[4];

    // References
    RoomManager roomManager;
    private List<Enemy> enemies = new List<Enemy>();
    private GameObject[] doors = new GameObject[4];
    public Door[] doorTransitions;

    private void Awake() {
        roomManager = RoomManager.current; 
        doors[0] = transform.Find("CoverRight").gameObject;
        doors[1] = transform.Find("CoverTop").gameObject;
        doors[2] = transform.Find("CoverLeft").gameObject;
        doors[3] = transform.Find("CoverBottom").gameObject;
    }

    public void SetDoor(bool[] doorPos){
        for (int i = 0; i < doorStatus.Length; i++) {
            doorStatus[i] = doorPos[i];
        }
    }

    public bool[] GetDoor(){
        return doorStatus;
    }

    private void Update() {
        if (enemies.Count == 0){
            OpenDoors();
        }
    }

    private void OpenDoors(){
        for (int i = 0; i < doorStatus.Length; i++) {
            // open door if there is one
            if (doorStatus[i]){
                doors[i].SetActive(false);
            }
        }
    }

    public void SetPlayerLocation(int direction){
        roomManager.player.transform.position = playerSpawnPositions[direction].position;
    }

    // TODO
    public void GenerateEnemies(){
    
    }

    // TODO
    public void GenerateObjects(){
    
    }
}
