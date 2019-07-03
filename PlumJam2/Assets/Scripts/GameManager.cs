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
    public RaccoonAI raccoonAI;
    public AudioSource sneakyBGM;
    public AudioSource playfulBGM;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
            sneakyBGM.Stop();
            playfulBGM.Stop();
            CompleteLevel();
        }
        else if(raccoonAI.getHoldingItem() == true && !playfulBGM.isPlaying)
        {
            sneakyBGM.Stop();
            playfulBGM.Play();
        }
        else if(!raccoonAI.getHoldingItem() && !sneakyBGM.isPlaying)
        {
            sneakyBGM.Play();
            playfulBGM.Stop();
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
