using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform followTransform;

    private float xMin, xMax, yMin, yMax;
    public BoxCollider2D mapBounds;
    private float camY,camX;
    private float camOrthsize;
    private float cameraRatio;
    private Camera mainCam;
    private Vector3 smoothPos;
    public float smoothSpeed = 0.5f;

    private void Start()
    {
        xMin = -14.00411f; 
        xMax = 40.01427f;
        yMin = -7.98649f; 
        yMax = 10.56783f;
        Debug.Log("xMin = " + xMin + ", xMax = " + xMax + ", yMin = " + yMin + ", yMax = " + yMax);
        mainCam = GetComponent<Camera>();
        camOrthsize = mainCam.orthographicSize;
        cameraRatio = (xMax + camOrthsize) / 2.0f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        camY = Mathf.Clamp(followTransform.position.y, yMin - camOrthsize, yMax + camOrthsize);
        camX = Mathf.Clamp(followTransform.position.x, xMin - cameraRatio, xMax + cameraRatio);
        Debug.Log("camX = " + camX + ", camY = " + camY);
        smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, this.transform.position.z), smoothSpeed);
        this.transform.position = smoothPos;
        
        
    }
}
