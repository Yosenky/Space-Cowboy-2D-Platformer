using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.tag == "Boss" || other.gameObject.tag == "Boundary"
        || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Floor"){
          Destroy(gameObject);
        }

     }

     private void OnBecameInvisible(){
          Destroy(gameObject);

     }
}
