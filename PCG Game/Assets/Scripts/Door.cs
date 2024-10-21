using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == "Start Screen")
            {
                SceneManager.LoadScene("MapTest_test");
            }
            else
            {
                RoomManager roomManager = RoomManager.current;
                switch (gameObject.tag)
                {
                    case "DoorRight":
                        roomManager.ToNewRoom(roomManager.Right);
                        return;
                    case "DoorLeft":
                        roomManager.ToNewRoom(roomManager.Left);
                        return;
                    case "DoorTop":
                        roomManager.ToNewRoom(roomManager.Top);
                        return;
                    case "DoorBottom":
                        roomManager.ToNewRoom(roomManager.Bottom);
                        return;
                }
            }
        }
    }
}
