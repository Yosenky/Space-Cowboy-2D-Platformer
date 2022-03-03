using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossBehavior : MonoBehaviour
{

  public GameObject cactusBulletStage1; //Bullets shot out of cactus during stage 1



    void Start()
    {
        stageOneShoot();
    }

    void Update()
    {
        
    }

    //Shooting script for stage 1
    void stageOneShoot()
    {
      //Instantiate bullets around cactus
      int numberOfBullets = Random.Range(5,8);
      for(int i = 0; i < 1; i++){
        Instantiate(cactusBulletStage1, new Vector3(transform.position.x, transform.position.y, 0),Quaternion.Euler(0, 0, 90 + (180/numberOfBullets)*i));
      }
      //Pause
      //Shoot slowly out from spawn
    }
}
