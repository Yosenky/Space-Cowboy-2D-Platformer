using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   private Vector3 direction;
   public float gravity = -9.8f;
   public float strength = 5f, velocity=5f;

   private void Update(){
     Jump();

   }

   private void Jump(){
    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }
   } 

   private void move(){
    if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector3.left * velocity;
        }
    if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector3.right * velocity;
        }
   }  

}
