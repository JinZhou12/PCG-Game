using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCreator : MonoBehaviour
{
    [SerializeField] RectTransform minimapCenter; // Reference to the minimap center RectTransform
    [SerializeField] GameObject mapTilePrefab; // Prefab of the map tile UI element
    [SerializeField] float tileWidth = 100f; // Width of each tile on the minimap
    [SerializeField] float tileHeight = 100f; // Height of each tile on the minimap

    // Method to generate a new map tile at the given room position and set door states
    public void GenerateNewMapTile(Vector2 roomPos, bool[] doors)
    {
        // Calculate the tile position based on room position and minimap center
        Vector2 tilePos = minimapCenter.anchoredPosition + new Vector2(roomPos.x * tileWidth, roomPos.y * tileHeight);

        // Instantiate the map tile at the calculated position
        GameObject newMapTile = Instantiate(mapTilePrefab, minimapCenter.parent);
        RectTransform newTileRect = newMapTile.GetComponent<RectTransform>();
        newTileRect.anchoredPosition = tilePos;

        // Activate door indicators based on the doors array [0,1,2,3] : right, top, left, down
        Transform[] doorIndicators = newMapTile.GetComponentsInChildren<Transform>(true);
        
        if (doors.Length == 4 && doorIndicators.Length >= 4)
        {
            for (int i = 0; i < 4; i++)
            {
                doorIndicators[i].gameObject.SetActive(doors[i]);
            }
        }

        newMapTile.gameObject.SetActive(true);
    }
}
