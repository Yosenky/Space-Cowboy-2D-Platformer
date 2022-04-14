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


    void Start()
    {
        stageOneShoot();
    }

    void Update()
    {
        shootBullets();
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

    //Spawns in the bullets around the cactus
    IEnumerator spawnNewBullets(){
      Debug.Log("SpawnNewbullets");
      for(int i = 0; i < numberOfBullets; i++)
      {
        float bulletRotation = 85 - (170/(numberOfBullets-1)) * i; //Rotation of the bullets as they are spawned in, creates a semicircle around cactus
        if(!bulletsDestroyed)
        {
          changeBulletSprites();
          yield return new WaitForSeconds(1);
          destroyBullets();
          bulletsDestroyed = true;
        }
        cactusBulletStage1Array[i] = Instantiate(cactusBulletStage1,
                                                 new Vector3(edgeCollider.transform.position.x, edgeCollider.transform.position.y, 0),
                                                 Quaternion.Euler(0, 0, bulletRotation));
        spriteRenderers[i] = cactusBulletStage1Array[i].GetComponent<SpriteRenderer>();
        cactusBulletStage1RotationArray[i] = bulletRotation;
        cactusBulletStage1RigidBodyArray[i] = cactusBulletStage1Array[i].GetComponent<Rigidbody2D>();
      }
      spawnNewBulletsTimer();
    }
    
<<<<<<< Updated upstream

    IEnumerator spawnNewBulletsTimer(){
      Debug.Log("WORKING");
      testingShoot();
      yield return new WaitForSeconds(1);
      bulletsDestroyed = false;
      StartCoroutine(spawnNewBullets());
    }

    //Changes the bullet sprites to show that they have been destroyed
=======
    //Changes the bullet sprites to the partially destroyed ones
>>>>>>> Stashed changes
    void changeBulletSprites()
    {
      for(int j = 0; j < spriteRenderers.Length; j++)
      {
        spriteRenderers[j].sprite = deletedBullets;
      }
    }

<<<<<<< Updated upstream
    //Destroys all bullets in the cactus bullet array
=======
    //Destroys Bullets
>>>>>>> Stashed changes
    void destroyBullets()
    {
      for(int i = 0; i < numberOfBullets; i++)
      {
        Destroy(cactusBulletStage1Array[i]);
      }      
    }

<<<<<<< Updated upstream


    void testingShoot()
=======
    //Actually shoots the bullets
    void shootBullets()
>>>>>>> Stashed changes
    { 
      Debug.Log("testingShoot");
      int bulletSpeed = 10; //Speed of bullets

      //Shoots bullets(will automate later)
        for(int i = 0; i < cactusBulletStage1Array.Length; i++)
        {
          cactusBulletStage1RigidBodyArray[i].constraints = RigidbodyConstraints2D.None;
          cactusBulletStage1RigidBodyArray[i].velocity = new Vector2(-Mathf.Sin(cactusBulletStage1RotationArray[i] * Mathf.Deg2Rad) * bulletSpeed,
<<<<<<< Updated upstream
                                                                      Mathf.Cos(cactusBulletStage1RotationArray[i] * Mathf.Deg2Rad) * bulletSpeed);
        }

        
=======
                                                                      Mathf.Cos(cactusBulletStage1RotationArray[i] * Mathf.Deg2Rad) * bulletSpeed); //Setting the velocity to be radial around the cactus
          Debug.Log("Z rotation: " + cactusBulletStage1RotationArray[i]);
          Debug.Log("Sin(Z) = " + Mathf.Sin(cactusBulletStage1RotationArray[i]) * Mathf.Deg2Rad);
          Debug.Log("Cos(Z) = " + Mathf.Cos(cactusBulletStage1RotationArray[i]) * Mathf.Deg2Rad);
          Debug.Log("X Velocity: " + cactusBulletStage1RigidBodyArray[i].velocity.x);
          Debug.Log("Y Velocity: " + cactusBulletStage1RigidBodyArray[i].velocity.y);
        }

      }
      
      //Deletes bullets
      if(Input.GetKeyDown(KeyCode.F)){
        bulletsDestroyed = false;
>>>>>>> Stashed changes
        StartCoroutine(spawnNewBullets());
      

    }
}