using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviour
{
    // public parameters
    [System.NonSerialized] public static RoomManager current;
    [System.NonSerialized] public Room[][] map;
    [System.NonSerialized] public int Right = 0;
    [System.NonSerialized] public int Top = 1;
    [System.NonSerialized] public int Left = 2;
    [System.NonSerialized] public int Bottom = 3;
    [Header("Map Settings")]
    [SerializeField] private int mapSize = 5;
    [SerializeField] private int maxRoom = 8;
    [SerializeField] private float roomSpawnChance = 0.5f;
    
    // private Parameters
    private Vector2[] offsets = {new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1)}; // right, top, left, down
    private Vector2 currRoomPos;
    private int roomCount;
    // References
    public GameObject roomPrefab;
    public GameObject player;
    public MinimapCreator minimapCreator;
    
    private void Awake() {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Abraham");
        GenerateMap();
    }

    private void GenerateMap(){
        // Initialize empty map
        map = new Room[mapSize][];
        for (int i = 0; i < mapSize; i++){
            map[i] = new Room[mapSize];
        }
        
        // Generating map
        Vector2 startPos = new Vector2(Random.Range(0, mapSize), Random.Range(0, mapSize));
        roomCount = 0;
        GenerateRoom(startPos);
    }

    private void GenerateRoom(Vector2 roomPos){
        GameObject newRoomObj = Object.Instantiate(roomPrefab);
        Room room = newRoomObj.GetComponent<Room>();

        // Generate everything in room, First room don't need enemies, and deactivate all but first room
        if (roomCount != 0){
            room.GenerateEnemies();
            newRoomObj.SetActive(false);
        } else{
            player.transform.position = new Vector3(0, 0, 0); 
            currRoomPos = roomPos;
        }
        room.GenerateObjects();
        roomCount += 1;
        map[(int)roomPos.y][(int)roomPos.x] = room;

        // Randomizing doors: right, top, left, down
        bool[] doors = {false, false, false, false}; 
        for (int i = 0; i < 4; i++){

            Vector2 newPos = roomPos + offsets[i];
            // if newPos exceeds map boundary, no new room
            if (newPos.x < 0 || newPos.x >= mapSize || newPos.y < 0 || newPos.y >= mapSize) continue;
            // If room already created in position create the door to the room
            if (map[(int)newPos.y][(int)newPos.x] != null){
                doors[i] = true;
                continue;
            } 
            // if max room count reached, no more generation
            if (roomCount >= maxRoom) continue;

            // Create new room in location 
            bool hasRoom = roomSpawnChance > Random.Range(0, 1);
            doors[i] = hasRoom;
            if (hasRoom){
                GenerateRoom(newPos);
            }
        }
        room.SetDoor(doors);

        room.SetMapTile(minimapCreator.GenerateNewMapTile(roomPos, doors).GetComponent<MinimapTile>());
        if (roomCount == 1) room.ActivateRoom();
    }

    public void ToNewRoom(int direction){
        Vector2 newRoomPos = currRoomPos + offsets[direction];
        Room nextRoom = map[(int)newRoomPos.y][(int)newRoomPos.x];

        // deactivate current room
        map[(int)currRoomPos.y][(int)currRoomPos.x].DeactivateRoom();
        currRoomPos = newRoomPos;

        // activate next room and place player in corresponding position
        nextRoom.ActivateRoom();
        nextRoom.SetPlayerLocation((direction + 2) % 4);
    }
}
