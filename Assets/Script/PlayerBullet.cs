using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.tag == "Boss" || other.gameObject.tag == "Boundary"){
          Destroy(gameObject);
        }

     }
}
