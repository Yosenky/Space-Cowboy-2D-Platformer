using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform followTransform;
    public BoxCollider2D leftMapBound, rightMapBound, topMapBound, bottomMapBound;

    private float xMin, xMax, yMin, yMax;
    private float camY,camX;
    private float width;
    private float height;
    private Camera mainCam;
    private Vector3 smoothPos;
    public float smoothSpeed = 0.5f;

    private void Start()
    {
        xMin = leftMapBound.bounds.max.x;
        xMax = rightMapBound.bounds.min.x;
        yMin = bottomMapBound.bounds.max.y;
        yMax = topMapBound.bounds.min.y;
        mainCam = GetComponent<Camera>();
        //Camera view height is 2 * the orthographic size
        height = mainCam.orthographicSize * 2;
        //Camera width is the aspect of the camera view * the height
        width = height * mainCam.aspect;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        camY = Mathf.Clamp(followTransform.position.y, yMin, yMax);
        camX = Mathf.Clamp(followTransform.position.x, xMin, xMax);
        smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, this.transform.position.z), smoothSpeed);
        this.transform.position = smoothPos;
    }
}
