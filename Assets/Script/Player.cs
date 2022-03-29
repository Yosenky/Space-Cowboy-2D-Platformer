using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public Rigidbody2D rb;
   private float strength = 250f, velocity=5f;
   private float bulletSpeed = 10;
   public Rigidbody bullet;
   private bool onFloor=false;
   private int jumping=0;
   public int currentHealth = 0, maxHealth=250;
   public HealthBar healthBar;

   void Awake(){
    rb = GetComponent<Rigidbody2D>();
    currentHealth=maxHealth;
   }
   void Update(){
     jump();
     move();

     if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)){
             Fire();
     }

     if (Input.GetKeyDown(KeyCode.X)){
        DamagePlayer(10);
     }

   }

     void jump(){
        if(Input.GetKeyDown(KeyCode.Space)&&onFloor&&jumping==0){
          rb.AddForce(transform.up*strength);
          onFloor=false;
          jumping++;
        }else if(Input.GetKeyDown(KeyCode.Space)&&!onFloor&&jumping==1){
          rb.AddForce(transform.up*strength);
          jumping=0;  
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

     private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Floor"){
            onFloor=true;
            jumping=0;
        }
     }

     private void DamagePlayer(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
     }
 


}
