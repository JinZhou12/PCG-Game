using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Room : MonoBehaviour
{
    // Order: right, top, left, down
    [SerializeField] private int minEnemyCount = 1;
    [SerializeField] private int maxEnemyCount;
    [SerializeField] private Transform[] playerSpawnPositions;
    [SerializeField] private Transform[] enemySpawnPositions;
    private bool[] doorStatus = new bool[4];
    private int enemyCount;

    // References
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] bossPrefabs;
    private List<Enemy> enemies = new List<Enemy>();
    private GameObject[] doors = new GameObject[4];
    private MinimapTile minimapTile;
    public Door[] doorTransitions;

    private void Awake() {
        doors[0] = transform.Find("CoverRight").gameObject;
        doors[1] = transform.Find("CoverTop").gameObject;
        doors[2] = transform.Find("CoverLeft").gameObject;
        doors[3] = transform.Find("CoverBottom").gameObject;
        if (maxEnemyCount == 0) maxEnemyCount = enemySpawnPositions.Length;
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
        if (SceneManager.GetActiveScene().name == "Start Screen"){
            doors[1].gameObject.SetActive(false);
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
        RoomManager.current.player.transform.position = playerSpawnPositions[direction].position;
    }

    public void SetMapTile(MinimapTile tile){
        minimapTile = tile;
    }

    public void ActivateRoom(){
        gameObject.SetActive(true);
        minimapTile.ActivateTile();
    }

    public void DeactivateRoom(){
        gameObject.SetActive(false);
        minimapTile.DeactivateTile();
    }


    public void GenerateEnemies(){
        enemyCount = Random.Range(minEnemyCount, maxEnemyCount);
        List<Transform> availablePositions = new List<Transform>(enemySpawnPositions);

        for (int i=0; i < enemyCount; i++){
            int enemy = Random.Range(0, enemyPrefabs.Length);
            int enemyPos = Random.Range(0, availablePositions.Count);
            GameObject newEnemy = Object.Instantiate(enemyPrefabs[enemy], availablePositions[enemyPos].position, Quaternion.identity, transform);
            newEnemy.GetComponent<Enemy>().SetRoom(gameObject.GetComponent<Room>());

            enemies.Add(newEnemy.GetComponent<Enemy>());
            availablePositions.RemoveAt(enemyPos);
        }
    }

    public void RemoveEnemy(Enemy enemy){
        enemies.Remove(enemy);
    }

    // TODO
    public void GenerateObjects(){
    
    }
}
