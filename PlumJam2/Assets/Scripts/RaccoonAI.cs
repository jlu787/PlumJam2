using UnityEngine;

public class RaccoonAI : MonoBehaviour {

    public GameObject hands;
    public Climb climbScript;

    public float RaccoonSpeed = 10.0f; // how quickly the raccoon moves
    public float ClimbSpeed = 1000.0f;   // how quickly the raccoon climbs
    public float ClimbTime = 2.0f;     // how long the raccoon will climb for 
    public float climbForceX = 10.0f;
    public float climbForceY = 10.0f;


    public Rigidbody2D rb;

    private float climbTimer = 0.0f; // used to see how long the raccoon has been climbing for

    //bool shouldBeClimbing = false;

    // Use this for initialization
    void Start()
    {
        rb.velocity = new Vector2(RaccoonSpeed * Time.deltaTime, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    // FixedUpdate for physics related stuff
    void FixedUpdate()
    {
        if (climbScript.getShouldBeClimbing())
        {
            DoClimb();
        }
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
            rb.AddForce(new Vector2(climbForceX, climbForceY));
        }
        else if (RaccoonSpeed < 0)
        {
            rb.AddForce(new Vector2(-climbForceX, climbForceY));
        }
        //rb.velocity = new Vector2(RaccoonSpeed * Time.deltaTime, ClimbSpeed * Time.deltaTime);
        if (climbTimer >= ClimbTime)
        {
            Flip();
            climbTimer = 0.0f;
            climbScript.setShouldBeClimbing(false);
            //shouldBeClimbing = false;
        }
    }

    //private void OnCollisionStay2D(Collision2D collisionInfo)
    //{
    //    //if (collisionInfo.collider.tag == "Climbable" || collisionInfo.collider.tag == "Obstacle")
    //    //{
    //    //    shouldBeClimbing = true;
    //    //}
    //}

    //private void OnCollisionEnter2D(Collision2D collisionInfo)
    //{
    //    if (collisionInfo.collider.tag == "Climbable" || collisionInfo.collider.tag == "Obstacle")
    //    {
    //        if (collisionInfo.collider.GetType() == typeof(CircleCollider2D))
    //        {
    //            Debug.Log("Should be climbing");
    //            shouldBeClimbing = true;
    //        }
    //        //Climb();
    //    }

    //    // if the object it collides with is wall that is not climbable reverse it's direction
    //    if (collisionInfo.collider.tag == "Wall")
    //    {
    //        Flip();
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collisionInfo)
    //{
    //    if (collisionInfo.collider.tag == "Climbable" || collisionInfo.collider.tag == "Obstacle")
    //    {
    //        if (collisionInfo.collider.GetType() == typeof(CircleCollider2D))
    //        {
    //            shouldBeClimbing = false;
    //        }
    //        //Climb();
    //    }
    //}

}
