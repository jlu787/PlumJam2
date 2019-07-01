using UnityEngine;

public class RaccoonAI : MonoBehaviour {

    public float RaccoonSpeed = 10.0f;
    public Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {

        // if the object it collides with is an obstacle reverse it's direction
        if (collisionInfo.collider.tag == "Obstacle")
        {
            RaccoonSpeed = RaccoonSpeed * -1;
            rb.velocity = new Vector2(RaccoonSpeed * Time.deltaTime, 0.0f);
        } 
    }

    // Use this for initialization
    void Start () {
        rb.velocity = new Vector2(RaccoonSpeed * Time.deltaTime, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {

    }

    // FixedUpdate for physics related stuff
    void FixedUpdate()
    {

    }
}
