using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossBehavior : MonoBehaviour
{

  public GameObject cactusBulletStage1; //Bullets shot out of cactus during stage 1
  public EdgeCollider2D edgeCollider; //Collider inside of the boss for the sake of spawning spikes inside of him

  public GameObject[] cactusBulletStage1Array; //Holds the cactus bullets once they have been instantiated
  public float[] cactusBulletStage1RotationArray; //Holds the cactus bullets' rotation
  public Rigidbody2D[] cactusBulletStage1RigidBodyArray; //Holds the cactus bullets' rigidbodies

  public SpriteRenderer[] spriteRenderers; //Sprite renderers, used to swap sprites
  public Sprite deletedBullets; //The dust particles for bullets when they are destroyed

  int numberOfBullets; //Number of bullets
  bool bulletsDestroyed = true; //False if bullets need to be destroyed, True if already destroyed

  public GameObject finalBoss;

  public int bossHealth;
  public int maxHealth = 100;
  public int currentHealth;

  public AudioSource victoryCheer;
  public AudioSource bossDamaged;


    void Start()
    {
      currentHealth = maxHealth;
      stageOneShoot();
      StartCoroutine(spawnNewBullets());
    }

    void Update()
    {
    }

    //Shooting script for stage 1
    void stageOneShoot()
    {
      //Instantiate bullets around cactus
      numberOfBullets = Random.Range(8,12);
      //numberOfBullets = 3; //Static number used in bug testing

      //initializing arrays
      cactusBulletStage1Array = new GameObject[numberOfBullets];
      cactusBulletStage1RotationArray = new float[numberOfBullets];
      cactusBulletStage1RigidBodyArray = new Rigidbody2D[numberOfBullets];
      spriteRenderers = new SpriteRenderer[numberOfBullets];

      StartCoroutine(spawnNewBullets());



      //Pause
      //Shoot slowly out from spawn
    }

    IEnumerator spawnNewBullets(){
      if(!bulletsDestroyed)
        {
          yield return new WaitForSeconds(1);
          changeBulletSprites();
          yield return new WaitForSeconds(1);
          destroyBullets();
          bulletsDestroyed = true;
        }

      for(int i = 0; i < numberOfBullets; i++)
      {
        float bulletRotation = 85 - (170/(numberOfBullets-1)) * i; //Rotation of the bullets as they are spawned in, creates a semicircle around cactus

        cactusBulletStage1Array[i] = Instantiate(cactusBulletStage1,
                                                 new Vector3(edgeCollider.transform.position.x, edgeCollider.transform.position.y, 0),
                                                 Quaternion.Euler(0, 0, bulletRotation));
        spriteRenderers[i] = cactusBulletStage1Array[i].GetComponent<SpriteRenderer>();
        cactusBulletStage1RotationArray[i] = bulletRotation;
        cactusBulletStage1RigidBodyArray[i] = cactusBulletStage1Array[i].GetComponent<Rigidbody2D>();
      }
      testingShoot();
      //yield return new WaitForSeconds(5);
    }
    

   /*
    IEnumerator spawnNewBulletsTimer(){
      testingShoot();
      yield return new WaitForSeconds(1);
      bulletsDestroyed = false;
      StartCoroutine(spawnNewBullets());
    }
    */

    //Changes the bullet sprites to show that they have been destroyed
    void changeBulletSprites()
    {
      for(int j = 0; j < spriteRenderers.Length; j++)
      {
        spriteRenderers[j].sprite = deletedBullets;
      }
    }

    //Destroys all bullets in the cactus bullet array
    void destroyBullets()
    {
      for(int i = 0; i < numberOfBullets; i++)
      {
        Destroy(cactusBulletStage1Array[i]);
      }      
    }



    void testingShoot()
    { 
      int bulletSpeed = 10; //Speed of bullets

      //Shoots bullets(will automate later)
        for(int i = 0; i < cactusBulletStage1Array.Length; i++)
        {
          cactusBulletStage1RigidBodyArray[i].constraints = RigidbodyConstraints2D.None;
          cactusBulletStage1RigidBodyArray[i].velocity = new Vector2(-Mathf.Sin(cactusBulletStage1RotationArray[i] * Mathf.Deg2Rad) * bulletSpeed,
                                                                      Mathf.Cos(cactusBulletStage1RotationArray[i] * Mathf.Deg2Rad) * bulletSpeed);
        }

        bulletsDestroyed = false;
        StartCoroutine(spawnNewBullets());
      

    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "PlayerBullet"){
            bossDamaged.Play();
            DamageBoss(10);
        }
        if(currentHealth <= 0){
          Destroy(finalBoss);
          victoryCheer.Play();
        }
     }

     
     private void DamageBoss(int damage){
        currentHealth -= damage;
     }
}
