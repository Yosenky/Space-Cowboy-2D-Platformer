using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   private Vector3 direction;
   public float gravity = -9.8f;
   public float strength = 5f, velocity=5f;
   public float bulletSpeed = 10;
   public Rigidbody bullet;

   private void Update(){
     Jump();
     move();

     if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)){
             Fire();
     }

     direction.y += gravity * Time.deltaTime;
     transform.position += direction*Time.deltaTime;

   }

   private void Jump(){
    if (Input.GetKeyDown(KeyCode.Space))
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

   
     
     
     void Fire()
     {
         Rigidbody bulletClone = (Rigidbody) Instantiate(bullet, transform.position, transform.rotation);
         bulletClone.velocity = new Vector2(bulletSpeed,0.0f);
         bulletClone.AddForce(bulletClone.velocity,(ForceMode.Force));
     }
 


}
