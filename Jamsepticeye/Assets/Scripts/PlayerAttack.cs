using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   private GameObject attackArea = default;

   private bool attacking = false; 

   private float timeToAttack = 0.25f;
   private float timer = 0f;

   void start()
   {
    attackArea = Transform.GetChild(0).GameObject;
   }

   void update()
   {
      if(Input.GetKeyDown(KeyCode.Mouse2))
     {
        Attack();
     }

     if(attacking)
     {
        timer += Time.deltaTime;
        if(timer >= timeToAttack)
        {
            timer = 0;
            attacking = false;
            attackArea.SetActive(attacking);
        }
    }


   }



   
}
