using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject enemy;
    public int maxHealth = 40;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "PlayerBullet") {
            Debug.Log(enemy.name + " DAMAGED");
            DamageEnemy(10);
        }
        if(currentHealth <= 0) {
            Destroy(enemy);
        }
     }
     
     private void DamageEnemy(int damage){
        currentHealth -= damage;
     }
}
