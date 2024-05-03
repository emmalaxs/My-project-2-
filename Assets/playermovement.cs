using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermomevement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animate;

    [SerializeField] private float moveSpeed;
    private float jumpForce = 30f;
    private float moveHorizontal;
    private float moveVertical;
    private bool isGrounded;
    private bool facingRight = true;

    //[SerializeField] private float slideSpeed = 5f;
    //[SerializeField] private float slideDuration = 0.5f;
    //private bool isSliding = false;
    //private float slideTimer = 0f;



    //Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();

        isGrounded = false;
    }

    //Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)) // Check for jump key press
        {
            Debug.Log("Jump key pressed!");
        }

        animate.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }

        // Get input for horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate movement amount based on input and speed
        float movementAmount = horizontalInput * moveSpeed * Time.deltaTime;
        Debug.Log(movementAmount);

        // Calculate new position
        Vector3 newPosition = transform.position + new Vector3(movementAmount, 0f, 0f);

        transform.position = newPosition;

        // Check for input to start sliding 
        //if (Input.GetKey(KeyCode.LeftShift) && !isSliding)
        //{
        //Debug.Log("Left Shift pressed!");
        //StartSlide();
        //}
        // Handle sliding logic
        // if (isSliding)
        //{
        //slideTimer += Time.deltaTime;

        // Apply sliding force
        //rb2D.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * slideSpeed, rb2D.velocity.y);

        // Stop slide after duration
        //if (slideTimer >= slideDuration)
        //{
        //StopSlide();
        //}
        //}

        // Set Animator parameter
        //animate.SetBool("isSliding", isSliding);
    }

    void FixedUpdate()
    {
        //rb2D.velocity = new Vector2(moveHorizontal * moveSpeed * Time.deltaTime, rb2D.velocity.y);

        if (moveVertical > 0.1f && isGrounded)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) // Detect collision for grounding
    {
        Debug.Log("Collision detected!"); // Added line to verify collision
        if (collision.gameObject.tag == "Ground") // Check if collided with ground object
        {
            isGrounded = true;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    //void StartSlide()
    //{
        //isSliding = true;
        //slideTimer = 0f;
    //}

    //void StopSlide()
    //{
        //isSliding = false;
    //}
}