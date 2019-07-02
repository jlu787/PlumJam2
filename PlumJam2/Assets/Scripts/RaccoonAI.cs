using UnityEngine;

public class RaccoonAI : MonoBehaviour {

    public GameObject hands;
    public Climb climbScript;
    public ParticleSystem dustParticles;
    

    public float RaccoonSpeed = 10.0f; // how quickly the raccoon moves
    public float ClimbSpeed = 1000.0f;   // how quickly the raccoon climbs
    public float ClimbTime = 3.0f;     // how long the raccoon will climb for 
    public float climbForceX = 10.0f;
    public float climbForceY = 10.0f;

    public Rigidbody2D rb;

    float normalGravity;

    private float climbTimer = 0.0f; // used to see how long the raccoon has been climbing for

    private bool holdingAnItem = false;
    private GameObject itemBeingHeld;

    //bool shouldBeClimbing = false;

    // Use this for initialization
    void Start()
    {
        climbTimer = 1000.0f;
        rb.velocity = new Vector2(RaccoonSpeed * Time.deltaTime, 0.0f);
        normalGravity = rb.gravityScale; // get the gravity at the start of the game
        //dustParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        //dustParticles.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        // For flipping the sprite of the raccoon based on speed
        if (RaccoonSpeed > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (RaccoonSpeed < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        // For updating the position of the collected object
        if (holdingAnItem)
        {
            itemBeingHeld.GetComponent<Rigidbody2D>().position = transform.position;

            //// check if it is near a goal
            //RaycastHit2D hit = Physics2D.Raycast(GetComponent<Rigidbody2D>().position, Vector2.zero);
            //if(hit != null)
            //{
                
            //    if (hit.collider.tag == "Collectable")
            //    {
            //        Debug.Log("Found Collectable");
            //        //holdingAnItem = false;
            //        //Debug.Log("Stored!!!");
            //        //Destroy(itemBeingHeld);
            //    }
            //}
            
        }
    }

    // FixedUpdate for physics related stuff
    void FixedUpdate()
    {
        if (climbTimer <= ClimbTime)
        {
            rb.gravityScale = 0; // disable gravity
           // GetComponent<BoxCollider2D>().enabled = false; // disable box collider

            DoClimb();
            if (dustParticles.isStopped)
            {
                Debug.Log("Start Emitting");
                dustParticles.Play();
            }

        }
        else
        {
            rb.gravityScale = normalGravity; // enable gravity
            //GetComponent<BoxCollider2D>().enabled = true; // enable box collider
            if(dustParticles.isPlaying)
            {
                dustParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);        
            }

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

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
      
        // when it finds a collectable
        if (collisionInfo.collider.tag == "Collectable" && !holdingAnItem)
        {
            holdingAnItem = true;
            Debug.Log("Found collectable");
            itemBeingHeld = collisionInfo.gameObject;
            itemBeingHeld.GetComponent<BoxCollider2D>().enabled = false;
            itemBeingHeld.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        //// when it finds a goal and is holding a collectable
        //if (collisionInfo.collider.tag == "Goal" && holdingAnItem)
        //{
        //    holdingAnItem = false;
        //    Debug.Log("Stored!!!");
        //    Destroy(itemBeingHeld);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        // when it finds a goal and is holding a collectable
        if (collisionInfo.tag == "Goal" && holdingAnItem)
        {
            collisionInfo.GetComponentsInChildren<ParticleSystem>()[0].Play();
            holdingAnItem = false;
            Debug.Log("Stored!!!");
            Destroy(itemBeingHeld);
        }
    }
}
