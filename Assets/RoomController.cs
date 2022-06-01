using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomController : MonoBehaviour
{
   [SerializeField] private EnemyMover[] enemiesOnRoom;
   [SerializeField] private Kafes[] kafesler;
   [HideInInspector]public bool playerOnRoom;
   [SerializeField] private GameObject lightObjects;
   [SerializeField] private Light[] _lights;
   [SerializeField] private Color startColor;
   private float totalCanInRoom;
   private float lerpRate = 0;
   private List<EnemyHealthHandler> _enemyHealthHandlers;
   private float currentCanInRoom;
   [SerializeField] private Color finishColor;
   [SerializeField] private PlayerHealthAward[] _healthAwards;

   private void Start()
   {
      _enemyHealthHandlers = new List<EnemyHealthHandler>();
      foreach (var enemyMover in enemiesOnRoom)
      {
         _enemyHealthHandlers.Add(enemyMover.gameObject.GetComponent<EnemyHealthHandler>());
      }

      totalCanInRoom = _enemyHealthHandlers.Sum(x => x.Health);
      Debug.Log(totalCanInRoom);
      ArrangeLights();
   }

   void ArrangeLights()
   {
      if (lightObjects)
      {
         _lights = lightObjects.transform.GetComponentsInChildren<Light>();
      }

      if (_lights != null)
      {
         foreach (var light1 in _lights)
         {
            light1.color = startColor;
         }
      }
   }
   private void Update()
   {
      KafesYonet();
      currentCanInRoom = _enemyHealthHandlers.Sum(x => x.Health);
      float rate = (totalCanInRoom - currentCanInRoom) / totalCanInRoom;
      foreach (Light light1 in _lights)
      {
         light1.color = Color.Lerp(startColor, finishColor, rate);
      }
      
   }

   bool SandiklarAcildi()
   {
      foreach (var playerHealthAward in _healthAwards)
      {
         if (playerHealthAward.opened == false)
         {
            return false;
         }
      }

      return true;
   }
   void KafesYonet()
   {
      if (playerOnRoom && !AllEnemiesDead() )
      {
         foreach (var kafes in kafesler)
         {
            kafes.ac = false;
         }     
      }
      if (AllEnemiesDead() && SandiklarAcildi())
      {
         foreach (var kafes in kafesler)
         {
            kafes.ac = true;
         }
      }
   }

   public bool AllEnemiesDead()
   {
      foreach (var enemyMover in enemiesOnRoom)
      {
         if (enemyMover.EnemyState != ENEMYSTATE.DEATH)
            return false;
      }
      return true;
   }
}
