using UnityEngine;

public class Climb : MonoBehaviour {

    bool shouldBeClimbing = false;
    public bool getShouldBeClimbing() { return shouldBeClimbing; }
    public void setShouldBeClimbing(bool _b) { shouldBeClimbing = _b; }
    public GameObject raccoon;
    public RaccoonAI raccoonAI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "Climbable" || collisionInfo.collider.tag == "Obstacle")
        {
          
            Debug.Log("Should be climbing");
            shouldBeClimbing = true;
            //Climb();
        }

        // if the object it collides with is wall that is not climbable reverse it's direction
        if (collisionInfo.collider.tag == "Wall")
        {
            //raccoon.Flip();
        }
    }

    private void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "Climbable" || collisionInfo.collider.tag == "Obstacle")
        {
            shouldBeClimbing = false;
            //Climb();
        }
    }
}
