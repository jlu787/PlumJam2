using UnityEngine;

public class Climb : MonoBehaviour {

    bool shouldBeClimbing = false;
    public bool getShouldBeClimbing() { return shouldBeClimbing; }
    public void setShouldBeClimbing(bool _b) { shouldBeClimbing = _b; }
    //public GameObject raccoon;
    public RaccoonAI raccoonAI;

    int lastObstacle;
    int climbingThis;

    //bool stillClimbing = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "Climbable" || collisionInfo.collider.tag == "Obstacle")
        {
            climbingThis = collisionInfo.collider.gameObject.GetInstanceID(); // get the instance id of whatever it is currently touching
        }    
    }
    

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {

        //Debug.Log("We hit something");
        if (collisionInfo.collider.tag == "Climbable" || collisionInfo.collider.tag == "Obstacle")
        {
            
            // Check if it is the same obstacle as before
            // if it isn't then start climbing
            //if (lastObstacle != collisionInfo.collider.gameObject.GetInstanceID())
            //{
                Debug.Log("Should be climbing");
                shouldBeClimbing = true;
                raccoonAI.SetClimbTimer(0.0f); // Reset the climb timer so that it starts climbing
                lastObstacle = collisionInfo.collider.gameObject.GetInstanceID();
            //}
        }

        // if the object it collides with is wall that is not climbable reverse it's direction
        if (collisionInfo.collider.tag == "Wall")
        {
            raccoonAI.Flip();
        }
    }

    private void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "Climbable" || collisionInfo.collider.tag == "Obstacle")
        {
            shouldBeClimbing = false;

            // set the climber to one to give the raccoon a little bit more time to climb even when it's hands aren't touching the object
            raccoonAI.SetClimbTimer(raccoonAI.getClimbTime() - 0.25f);
        }
    }

    public bool CheckStillClimbingSameWall()
    {
        if (lastObstacle == climbingThis)
        {
            Debug.Log("Returning True");
            return true;

        }
        else
        {
            Debug.Log("Returning False");
            return false;
        }
    }
}
