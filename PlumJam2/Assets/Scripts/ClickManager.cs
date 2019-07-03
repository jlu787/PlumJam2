using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {

    bool objectFound = false;
    Collider2D attachedObject;
    Quaternion objectOrientation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
        {

            // Convert click position to world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null) // clicked an object
            {
                // if the object is an obstacle
                if (hit.collider.gameObject.tag == "Obstacle")
                {
                    objectFound = true;
                    attachedObject = hit.collider;
                    objectOrientation = hit.collider.transform.rotation;
                }
            }
        }
        
        if (objectFound == true)
        {
            attachedObject.attachedRigidbody.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            attachedObject.transform.rotation = objectOrientation;

            // disable the box collider
            //attachedObject.gameObject.GetComponent<BoxCollider2D>().enabled = false; 
            attachedObject.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        if (Input.GetMouseButtonUp(0)&&objectFound == true)
        {
            objectFound = false;

            // enable the box collider
            //attachedObject.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            attachedObject.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;

        }
    }
}
