using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoam : MonoBehaviour
{

    private Rigidbody2D rb;
    //X values for enemy stopping points
    public float pointA, pointB;
    //Holds the current point the enemy is moving towards
    private float currentTarget;
    //Stops movement for this amount of seconds (TODO)
    private float timer = 0f;
    //Enemy's move speed
    public float velocity = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //Set the enemy's initial target to point A, get Rigidbody
        rb = GetComponent<Rigidbody2D>();
        currentTarget = pointA;
        //Make the player move towards point A (point A < point B)
        rb.velocity = new Vector2(-velocity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is going towards point A
        if(currentTarget == pointA) {
            //Player has reached point A, reverse velocity and switch target point 
            if (transform.position.x <= pointA) {
                //Debug.Log("Enemy at A");
                rb.velocity = new Vector2(velocity, 0);
                currentTarget = pointB;
            }
        }
        //If the player is going towards point B
        else {
            //Player has reached point B, reverse velocity and switch target point
            if (transform.position.x >= pointB) {
                //Debug.Log("Enemy at B");
                rb.velocity = new Vector2(-velocity, 0);
                currentTarget = pointA;
            }
        }

        //Debug.Log("Position.X = " + transform.position.x);
    }
}
