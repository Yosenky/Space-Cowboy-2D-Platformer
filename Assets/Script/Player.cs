using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
   public Rigidbody2D rb;
   public float strength = 300f, velocity=5f, runSpeed=10f;
   public float bulletSpeed = 10;
   public GameObject bullet;
   public bool onFloor=false, isRunning=false;
   public int jumping=0;
   public int currentHealth = 0, maxHealth=250;
   public HealthBar healthBar;
   GameObject bulletClone;
   public Button mainMenuButton;
   public Text deathText;

   void Awake(){
    rb = GetComponent<Rigidbody2D>();
    currentHealth=maxHealth;
    Time.timeScale = 1f;

    rb.constraints = RigidbodyConstraints2D.FreezeRotation;

   }
   void Update(){
     if (currentHealth <= 0){
       playerDead();


     } else{
       jump();
       move();

       //press J or click to shoot
       if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)){
             Fire();
       }

       //damage player test
       if (Input.GetKeyDown(KeyCode.X)){
          DamagePlayer(10);
       }
     }
    


   }

     //jump method press Space to jump
     void jump(){
        //check if it's the first jump
        if(Input.GetKeyDown(KeyCode.Space)&&onFloor&&jumping==0){
          rb.AddForce(transform.up*strength);
          onFloor=false;
          jumping++;
        //check for second jump, while player in the air
        }else if(Input.GetKeyDown(KeyCode.Space)&&!onFloor&&jumping==1){
          rb.velocity= new Vector2(rb.velocity.x,0f);
          rb.AddForce(transform.up*strength);
          jumping=0;  
        }
     }
     //create bullets and give them velocity
     void Fire()
     {
         bulletClone =  Instantiate(bullet, new Vector2(transform.position.x+0.8f, transform.position.y-.3f), transform.rotation);
         bulletClone.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed,0.0f);
         //bulletClone.GetComponent<Rigidbody2D>().AddForce(bulletClone.GetComponent<Rigidbody2D>().velocity,(ForceMode.Force));
     }

     void move(){
        //if A is press down move left
        if (Input.GetKeyDown(KeyCode.A)) 
         {
          //if holding shift, increase speed
          if(Input.GetKey(KeyCode.RightShift)){
              isRunning=true;
              rb.velocity = new Vector2 (runSpeed*-1,rb.velocity.y);
            }else
              rb.velocity = new Vector2 (velocity * -1,rb.velocity.y);  
         }

        //
        if (Input.GetKeyUp(KeyCode.A)) 
        {
            rb.velocity = new Vector2 (0,0);
        }
        //same as above
        if (Input.GetKeyDown(KeyCode.D)) 
        {
              rb.velocity = new Vector2 (velocity,rb.velocity.y);
        }
        if (Input.GetKeyUp(KeyCode.D)) 
        {
            rb.velocity = new Vector2 (0,0);
        }
     }

     private void OnTriggerEnter2D(Collider2D other){
        //check if the object is floor and set jump to first jump
        if (other.gameObject.tag == "Floor"){
            onFloor=true;
            jumping=0;
        }
        if (other.gameObject.tag == "Bullet"){
            DamagePlayer(10);
        }

        if (other.gameObject.tag == "Enemy"){
            DamagePlayer(30);
        }

        if (other.gameObject.tag == "Boss"){
            DamagePlayer(50);
        }

        if (other.gameObject.tag == "Portal") {
            //Load scene 2 in File -> Build Settings (Final Boss Scene
            SceneManager.LoadScene(2);
            //Unload scene 1 in File -> Build Settings (Level)
            SceneManager.UnloadSceneAsync(1);
        }

        if (other.gameObject.tag == "bottomBound"){
          playerDead();
        }
     }

     
     private void DamagePlayer(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
     }

     void playerDead(){
       Time.timeScale = 0f;
       rb.constraints = RigidbodyConstraints2D.FreezeAll;
       gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * 90);
       deathText.gameObject.SetActive(true);
       mainMenuButton.gameObject.SetActive(true);
     }

     public void loadMainMenu(){
       //Load scene 2 in File -> Build Settings (Final Boss Scene
            SceneManager.LoadScene(0);
            //Unload scene 1 in File -> Build Settings (Level)
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
     }
 


}
