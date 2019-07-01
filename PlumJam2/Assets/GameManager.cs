using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    int currentScene;

	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(currentScene);
        }
	}
}
