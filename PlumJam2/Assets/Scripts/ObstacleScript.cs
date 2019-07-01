using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion newQuat = new Quaternion();
        newQuat = transform.rotation;
        newQuat.z = 0;
        transform.rotation = newQuat;
	}
}
