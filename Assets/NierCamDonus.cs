using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NierCamDonus : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToClose;
    [SerializeField]private Transform firstPoint;
    [SerializeField]private Transform lastPoint;

    public Transform FirstPoint => firstPoint;
    public Transform LastPoint => lastPoint;

    public void ControlMeshRenderers(bool sit)
    {
        foreach (var o in objectsToClose)
        {
            if (o.GetComponent<MeshCollider>())
            {
                o.GetComponent<MeshRenderer>().enabled = sit;
            }
            else
            {
                o.SetActive(sit);
            }
        }
    }
 
}
