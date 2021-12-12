using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsBehaviour : MonoBehaviour
{
    // 0 - Up , 1 - Down, 2 - Right, 3 - Left
    public GameObject[] Doors;

    public bool[] StatusTest;

    //private void Start()
    //{
    //    UpdateRoom(StatusTest);
    //}

    public void UpdateRoom(bool[] status)
    {

        for (int i = 0; i < status.Length; i++)
        {
            Doors[i].SetActive(status[i]);
        }
    }
}
