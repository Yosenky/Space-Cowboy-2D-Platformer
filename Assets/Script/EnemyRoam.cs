using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoam : MonoBehaviour
{
    private float currentTarget;
    private Rigidbody2D rb;
    //X values for enemy stopping points
    public float pointA, pointB;
    private float timer = 0f;
    public float velocity = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTarget = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget == pointA) {
            rb.velocity = new Vector2(-velocity, 0);

            if (transform.position.x <= pointA) {
                //Debug.Log("Enemy at A");
                rb.velocity = Vector2.zero;
                currentTarget = pointB;
            }
        }
        else {
            rb.velocity = new Vector2(velocity, 0);
        
            if (transform.position.x >= pointB) {
                //Debug.Log("Enemy at B");
                rb.velocity = Vector2.zero;
                currentTarget = pointA;
            }
        }

        //Debug.Log("Position.X = " + transform.position.x);
        
    }
}
