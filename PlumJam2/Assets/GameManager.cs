using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    int currentScene;
    GameObject[] arrCollectable1;
    GameObject[] arrCollectable2;
    GameObject[] arrCollectable3;

    public Text DeliverText;

    public GameObject completeLevelUI;

    int numOfCollectables;

	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        completeLevelUI.SetActive(false);


    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(currentScene);
        }

        // find how many collectable objects there are in the level
        arrCollectable1 = GameObject.FindGameObjectsWithTag("Collectable1");
        arrCollectable2 = GameObject.FindGameObjectsWithTag("Collectable2");
        arrCollectable3 = GameObject.FindGameObjectsWithTag("Collectable3");

        numOfCollectables = arrCollectable1.Length + arrCollectable2.Length + arrCollectable3.Length;

        //Debug.Log("Need to collect: " + numOfCollectables);

        DeliverText.text = "Deliver: " + numOfCollectables.ToString();

        if (numOfCollectables == 0)
        {
            CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    public void DecrementNumOfCollectables()
    {
        numOfCollectables--;
    }
}
