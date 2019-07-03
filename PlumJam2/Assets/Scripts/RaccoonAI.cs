using UnityEngine;

public class RaccoonAI : MonoBehaviour {

    public GameObject hands;
    public Climb climbScript;
    public ParticleSystem dustParticles;
    public Animation raccoonAnimation;
    public Animator raccoonAnimator;
    public AudioSource deliveredSFX;
    

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
    private int collisionCount = 0;
    private bool movingRight = true;

    // Use this for initialization
    void Start()
    {
        climbTimer = 1000.0f;
        rb.velocity = new Vector2(RaccoonSpeed * Time.deltaTime, 0.0f);
        normalGravity = rb.gravityScale; // get the gravity at the start of the game

        // check which way the raccoon is moving
        if (RaccoonSpeed >= 0)
        {
            movingRight = true;
        }
        else movingRight = false;

    }

    // Update is called once per frame
    void Update()
    {
        // For flipping the sprite of the raccoon based on speed
        if (RaccoonSpeed > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            movingRight = true;
        }
        else if (RaccoonSpeed < 0)
        {
            movingRight = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        // For updating the position of the collected object
        if (holdingAnItem)
        {
            Vector3 collectPos = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
            itemBeingHeld.GetComponent<Rigidbody2D>().position = collectPos;          
        }
    }

    // FixedUpdate for physics related stuff
    void FixedUpdate()
    {
        if (climbTimer <= ClimbTime)
        {

            rb.gravityScale = 0; // disable gravity
            

            // change animation
            raccoonAnimator.SetInteger("state", 1);

            DoClimb();
            if (dustParticles.isStopped)
            {
                //Debug.Log("Start Emitting");
                dustParticles.Play();
            }

        }
        else
        {
            // Debug.Log(rb.velocity);
            if (rb.velocity.x == 0)
            {
                //Debug.Log("HELP");
                //Debug.Log(RaccoonSpeed);

                rb.AddForce(new Vector2(0.0f, 10.0f));
                rb.velocity = new Vector2(RaccoonSpeed * Time.deltaTime, 0.0f);
            }

            // if it is colliding with something play the walking animation
            if (collisionCount >0)
            {
                raccoonAnimator.SetInteger("state", 0);
            }
            // if it isn't play the falling animation
            else
                raccoonAnimator.SetInteger("state", 2);

            rb.gravityScale = normalGravity; // enable gravity
            //GetComponent<BoxCollider2D>().enabled = true; // enable box collider
            if(dustParticles.isPlaying)
            {
                dustParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);        
            }

        }
    }

    public bool getHoldingItem()
    {
        return holdingAnItem;
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
        // used to keep track of how many objects raccoon is colliding with so we know when to play the fall animation
        collisionCount++;
    }

    private void OnCollisionExit2D(Collision2D collisionInfo)
    {
        // used to keep track of how many objects raccoon is colliding with so we know when to play the fall animation
        collisionCount--;
    }

    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        Debug.Log("trigger");
        // when it finds a collectable
        if ((collisionInfo.tag == "Collectable1" ||
            collisionInfo.tag == "Collectable2" ||
            collisionInfo.tag == "Collectable3")
            && !holdingAnItem)
        {
            holdingAnItem = true;
            Debug.Log("Found collectable");
            itemBeingHeld = collisionInfo.gameObject;
            itemBeingHeld.GetComponent<BoxCollider2D>().enabled = false;
            itemBeingHeld.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        // when it finds a goal and is holding a collectable
        if (itemBeingHeld !=null)
        {
            if (((collisionInfo.tag == "Goal1" && itemBeingHeld.tag == "Collectable1") ||
                        (collisionInfo.tag == "Goal2" && itemBeingHeld.tag == "Collectable2") ||
                        (collisionInfo.tag == "Goal3" && itemBeingHeld.tag == "Collectable3"))
                        && holdingAnItem)
            {
                collisionInfo.GetComponentsInChildren<ParticleSystem>()[0].Play();
                holdingAnItem = false;

                deliveredSFX.Play();
                //Debug.Log("Stored!!!");
                Destroy(itemBeingHeld);
            }
        }
        
    }
}
