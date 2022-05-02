using System;
using System.Collections;
using System.Collections.Generic;
using GameJam;
using UnityEngine;

public class Dissolver : MonoBehaviour
{
    #region Serialized Fields
    #endregion

    #region Private Fields

    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Material[] dissolveMats;
    #endregion

    #region Public Properties
    #endregion

    #region MonoBehaveMethods
    void Awake()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        dissolveMats = _skinnedMeshRenderer.materials;
    }

    void Start()
    {
        foreach (var mat in dissolveMats)
        {
            mat.SetFloat("_Cutoff",0);
        }
    }

   
    void Update()
    {
       SprintDeneme();
    }
    #endregion
    
    #region PublicMethods

    void SprintDeneme()
    {

        if (PlayerDataSingleton.Instance.PlayerState == PLAYERSTATE.SPRINT)
        {
            foreach (var mat in dissolveMats)
            {
                mat.SetFloat("_Cutoff",0.7f);
            }
        }
        else
        {
            foreach (var mat in dissolveMats)
            {
                mat.SetFloat("_Cutoff",0f);
            }
        }

     
    }
    
    #endregion
    
    #region PrivateMethods
    #endregion
}
