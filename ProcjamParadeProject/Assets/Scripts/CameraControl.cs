using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    // How many units should we keep from the players

    // public bool focusOnSoloBuilding = true;

    public float mouseX = 1f;
    public float mouseY = 1f;
    public float mouseZoom = 5f;
    public float mouseZoomLarge = 5f;
    public float zoomFactor = 1.5f;
    public float zoomForSolo = 60f;
    public float zoomDampener = 1f;
    public float followTimeDelta = 0.8f;
    public float nearBumpStop = 10f;


    public bool bumped;
   public  float startingRotX;
    public float rotateForTransparentCells = 10;
    public float extraSpace = 5f;
    public float rotSpeed = 1f;


    public GameObject activeBuilding;

    Vector3 localPosStart;


    public float shadowMod = 1.5f;

    public bool focusOnClicked = false;

    

    // Use this for initialization
    public void Start () {
    
        startingRotX = transform.localRotation.eulerAngles.x;

        localPosStart = transform.localPosition;

        
       
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {


        

        RotateCam();
        

        //clamp
     //  if (transform.position.y < 0.3f)
       //     transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);

        //shadows
        //QualitySettings.shadowDistance = Vector3.Distance(transform.position, Vector3.zero) * shadowMod;
    }

    void RotateCam()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            

            float x = (Input.GetAxis("Horizontal")/Screen.width )* mouseX ;
            Debug.Log(x);
            transform.parent.rotation *= Quaternion.Euler(0, x, 0);

            

        }

    }


    public void FixedCameraFollowSmooth(Camera cam, List<GameObject> players)
    {
        
        float distance = 0f;

        Vector3 avg = Vector3.zero;
        int playersUsed = 0;
        for (int i = 0; i < players.Count; i++)
        {
         
         
         
                avg += players[i].transform.position;
                playersUsed++;

                distance += players[i].transform.position.magnitude;
         
        }
        //anchor to center by adding a player at vector.zero - obz dont need to add zero
        avg /= playersUsed;// + 1;
        // Midpoint we're after
        Vector3 midpoint = avg;// (t1.position + t2.position) / 2f;

        // Distance between objects
        //float distance = (t1.position - t2.position).magnitude;
        float mod = (distance/zoomDampener) * zoomFactor;
        Vector3 cameraDestination = midpoint - cam.transform.forward * mod;// (distance/ zoomDampener) * zoomFactor;
        if (distance < nearBumpStop)///zoomDampener) * zoomFactor)
        {
            bumped = true;
            mod = (nearBumpStop / zoomDampener) * zoomFactor;
            cameraDestination = midpoint - cam.transform.forward * mod;// (distance/ zoomDampener) * zoomFactor;
            cam.transform.parent.position = Vector3.Slerp(cam.transform.parent.position, cameraDestination, followTimeDelta);

            return;
        }
        else bumped = false;
        // Move camera a certain distance
        

        // Adjust ortho size if we're using one of those
        if (cam.orthographic)
        {
            // The camera's forward vector is irrelevant, only this size will matter
            cam.orthographicSize = distance;
        }
        // You specified to use MoveTowards instead of Slerp/if no too close
        
            cam.transform.parent.position = Vector3.Slerp(cam.transform.parent.position, cameraDestination, followTimeDelta);

        // Snap when close enough to prevent annoying slerp behavior
        if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
            cam.transform.parent.position = cameraDestination;
    }
}
