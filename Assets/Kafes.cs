using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kafes : MonoBehaviour
{
   [SerializeField] private Transform parcalar;
   private Vector3 openVector,closeVector;
   public bool ac;
   
   private void Start()
   {
      openVector = new Vector3(parcalar.transform.localPosition.x, parcalar.transform.localPosition.y, -15.8f);
      closeVector = parcalar.transform.localPosition;
      if (ac)
      {
         StartCoroutine(OpenParcalar());
      }
      else
      {
         StartCoroutine(CloseParcalar());
      }
   }

   private void Update()
   {
      if (ac)
      {
         StartCoroutine(OpenParcalar());
      }
      else
      {
         StartCoroutine(CloseParcalar());
      }
   }


   public IEnumerator OpenParcalar()
   {
      parcalar.transform.localPosition = Vector3.Lerp(parcalar.transform.localPosition, openVector, Time.deltaTime * 5);
      yield return null;
   }
   public IEnumerator CloseParcalar()
   {
      parcalar.transform.localPosition = Vector3.Lerp(parcalar.transform.localPosition, closeVector, Time.deltaTime * 5);
      yield return null;
   }
   
}
