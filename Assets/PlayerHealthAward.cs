using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthAward : MonoBehaviour
{
    public int healthToGive = 33;
    [SerializeField] Transform kapak;
    [SerializeField] RoomController _roomController;
    public bool opened;
    [SerializeField] ParticleSystem pr;
    private void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")&&_roomController.AllEnemiesDead() && Input.GetKeyDown(KeyCode.E)&&!opened)
        {
            StartCoroutine(OPEN());
            pr.PlayWithLogic();

            opened = true;
        }
    }

    IEnumerator OPEN()
    {
        while (kapak.localRotation.eulerAngles.x > -89)
        {
            OpenSandik();
            yield return null;
        }
     
    }
    void OpenSandik()
    {
        var rot = Quaternion.Euler(-90, kapak.localRotation.y, kapak.localRotation.z);
        kapak.localRotation = Quaternion.Lerp(kapak.localRotation,rot,Time.deltaTime*5);
        
    }


}
