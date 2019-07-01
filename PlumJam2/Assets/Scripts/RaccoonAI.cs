using UnityEngine;

public class RaccoonAI : MonoBehaviour {

    public GameObject hands;
    public Climb climbScript;

    public float RaccoonSpeed = 10.0f; // how quickly the raccoon moves
    public float ClimbSpeed = 1000.0f;   // how quickly the raccoon climbs
    public float ClimbTime = 3.0f;     // how long the raccoon will climb for 
    public float climbForceX = 10.0f;
    public float climbForceY = 10.0f;

    public Rigidbody2D rb;

    float normalGravity;

    private float climbTimer = 0.0f; // used to see how long the raccoon has been climbing for

    //bool shouldBeClimbing = false;

    // Use this for initialization
    void Start()
    {
        climbTimer = 1000.0f;
        rb.velocity = new Vector2(RaccoonSpeed * Time.deltaTime, 0.0f);
        normalGravity = rb.gravityScale; // get the gravity at the start of the game
    }

    // Update is called once per frame
    void Update()
    {
        if (RaccoonSpeed > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (RaccoonSpeed < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // FixedUpdate for physics related stuff
    void FixedUpdate()
    {
        if (climbTimer <= ClimbTime)
        {
            rb.gravityScale = 0; // disable gravity
            GetComponent<BoxCollider2D>().enabled = false; // disable box collider

            DoClimb();
        }
        else
        {
            rb.gravityScale = normalGravity; // enable gravity
            GetComponent<BoxCollider2D>().enabled = true; // enable box collider

        }
    }

    public float getClimbTime()
    {
        return ClimbTime;
    }

    public void SetClimbTimer(float _f)
    {
        climbTimer = _f;
    }
    public void Flip()
    {
        // Flipping the raccoon
        RaccoonSpeed = RaccoonSpeed * -1;
        if (RaccoonSpeed > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (RaccoonSpeed < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        rb.velocity = new Vector2(RaccoonSpeed * Time.deltaTime, 0.0f);
    }

    public void DoClimb()
    {
        climbTimer += Time.deltaTime;
        if (RaccoonSpeed > 0)
        {
            //rb.AddForce(new Vector2(climbForceX, climbForceY));
            rb.velocity = new Vector2(RaccoonSpeed * Time.deltaTime, ClimbSpeed * Time.deltaTime);

        }
        else if (RaccoonSpeed < 0)
        {
            //rb.AddForce(new Vector2(-climbForceX, climbForceY));
            rb.velocity = new Vector2(RaccoonSpeed * Time.deltaTime, ClimbSpeed * Time.deltaTime);

        }
        if (climbTimer >= ClimbTime) // we have reached the end of the climb time
        {
            // check if it is still touching the obstacle / wall and is still trying to climb
            if (climbScript.CheckStillClimbingSameWall() && climbScript.getShouldBeClimbing())
            {
                // if it is then flip it
                Flip();
                
                climbScript.setShouldBeClimbing(false);
            }
            climbTimer = 1000.0f;
        }
    }
}
