using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlgilayici : MonoBehaviour
{
    [SerializeField] private RoomController _roomController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _roomController.playerOnRoom = true;
        }
    }
}
