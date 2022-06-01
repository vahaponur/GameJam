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
    private Transform player;
    [SerializeField] private Transform rewardMain;
    private List<GameObject> rewards;
    [SerializeField] private ParticleSystem disstortion;
    private HealthController hpC;

private void Start()
    {
        rewards = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hpC = player.GetComponent<HealthController>();
        foreach (Transform t in rewardMain)
        {
            if (t.gameObject.name != "rewards02")
            {
                rewards.Add(t.gameObject);
            }
        }
    }

    IEnumerator AllObjectsToPlayer()
    {
        yield return new WaitForSeconds(1);
        foreach (var VARIABLE in rewards)
        {
            StartCoroutine(GoToPlayer(VARIABLE.transform));
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator GoToPlayer(Transform t)
    {
        var posToGo = new Vector3(player.position.x, player.position.y + 0.5f, player.position.z);
        t.position = Vector3.Slerp(t.position, posToGo, Time.deltaTime * 10);
        if (Vector3.Distance(t.position,posToGo)<0.2f)
        {
            t.gameObject.SetActive(false);
        }
        yield return null;
    }

    private void Update()
    {
        if (_roomController.AllEnemiesDead() && !opened)
        {
            disstortion.PlayWithLogic();
        }

        if (opened)
        {
            disstortion.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")&&_roomController.AllEnemiesDead() && Input.GetKeyDown(KeyCode.E)&&!opened)
        {
            StartCoroutine(OPEN());
            pr.PlayWithLogic();
            hpC.health += healthToGive;
            hpC.health = Mathf.Clamp(hpC.health, 0, 100);
            
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
        StartCoroutine(AllObjectsToPlayer());

    }


}
