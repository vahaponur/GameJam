using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpEmission : MonoBehaviour
{
   private MeshRenderer _meshRenderer;
   private Material mat;
   private Color startColor;

   private void Start()
   {
      _meshRenderer = GetComponent<MeshRenderer>();
      mat = _meshRenderer.material;

      startColor = mat.GetColor("_EmissionColor");
   }

   private void Update()
   {
       LerpCol();
   }

   void LerpCol()
   {
       float intensity = 0.3f*Mathf.Sin(4*Time.time+1)+0.8f;
       mat.SetColor("_EmissionColor",startColor*intensity);
   }
}
