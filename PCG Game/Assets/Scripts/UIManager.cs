using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public RectTransform Minimap;
    public RectTransform Health;

    void Start()
    {
        int width = Screen.width;
        int height = Screen.height;

        Health.anchoredPosition = new Vector2( -width/2 + 80, height/2 - 100);
        Minimap.anchoredPosition = new Vector2( width/2 - 220, height/2 - 200);
    }
}
