using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
   [SerializeField] private EnemyMover[] enemiesOnRoom;
   [SerializeField] private Kafes[] kafesler;
   [HideInInspector]public bool playerOnRoom;
   
   private void Update()
   {
      if (playerOnRoom && !AllEnemiesDead())
      {
         foreach (var kafes in kafesler)
         {
            kafes.ac = false;
         }     
      }
      if (AllEnemiesDead())
      {
         foreach (var kafes in kafesler)
         {
            kafes.ac = true;
         }
      }
   }

   bool AllEnemiesDead()
   {
      foreach (var enemyMover in enemiesOnRoom)
      {
         if (enemyMover.EnemyState != ENEMYSTATE.DEATH)
            return false;
      }
      return true;
   }
}
