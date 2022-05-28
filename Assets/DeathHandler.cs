using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private EnemyMover _mover;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Material[] bodyMats;
    private float cutoffval = 0;
    private bool matBurned = false;

    private void Start()
    {
        _mover = GetComponent<EnemyMover>();
        bodyMats = _skinnedMeshRenderer.materials;
    }

    private void Update()
    {
        if (_mover.EnemyState == ENEMYSTATE.DEATH)
        {
            if (!matBurned)
            {
                StartCoroutine(LateMat());
                matBurned = true;

            }
            
        }
    }

    IEnumerator LateMat()
    {
        yield return new WaitForSeconds(2.3f);
        StartCoroutine(CloseMats());

    }

    IEnumerator CloseMats()
    {
        while (bodyMats[0].GetFloat("_Cutoff")<0.8)
        {
            foreach (var bodyMat in bodyMats)
            {
                bodyMat.SetFloat("_Cutoff",cutoffval);  
            }

            cutoffval += Time.deltaTime*0.3f;
            yield return new WaitForEndOfFrame(); 

        }
 
    }
}
