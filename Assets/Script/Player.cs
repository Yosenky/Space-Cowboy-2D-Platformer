using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public Rigidbody2D rb;
   private float strength = 250f, velocity=5f;
   public float bulletSpeed = 10;
   public Rigidbody bullet;
   private bool jumping=false;

   void Awake(){
    rb = GetComponent<Rigidbody2D>();
   }
   void Update(){
     jump();
     move();

     if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)){
             Fire();
     }

   }

     void jump(){
        if(Input.GetKeyDown(KeyCode.Space)&&!jumping){
          rb.AddForce(transform.up*strength);
          jumping=true;
        }
     }
     void Fire()
     {
         Rigidbody bulletClone = (Rigidbody) Instantiate(bullet, transform.position, transform.rotation);
         bulletClone.velocity = new Vector2(bulletSpeed,0.0f);
         bulletClone.AddForce(bulletClone.velocity,(ForceMode.Force));
     }

     void move(){
        if (Input.GetKeyDown (KeyCode.A)) 
         {
             rb.velocity = new Vector2 (velocity * -1,rb.velocity.y);
         }
        if (Input.GetKeyUp (KeyCode.A)) 
        {
            rb.velocity = new Vector2 (0,0);
        }
        if (Input.GetKeyDown (KeyCode.D)) 
        {
            rb.velocity = new Vector2 (velocity, rb.velocity.y);
        }
        if (Input.GetKeyUp (KeyCode.D)) 
        {
            rb.velocity = new Vector2 (0,0);
        }
     }
 


}
