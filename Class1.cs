using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;  // The force applied when jumping
    public float moveSpeed = 5f;   // The speed of vertical movement

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Handle vertical movement
        float verticalInput = Input.GetAxisRaw("Vertical");
        MoveVertical(verticalInput);
    }

    void Jump()
    {
        // Apply vertical force to jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void MoveVertical(float input)
    {
        // Move vertically based on input
        rb.velocity = new Vector2(rb.velocity.x, input * moveSpeed);
    }
}






using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;   // The speed of horizontal movement
    public float jumpForce = 10f;  // The force applied when jumping
    public LayerMask groundLayer;  // The layer(s) representing the ground

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Handle horizontal movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

        // Update animation parameters
        anim.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        // Check for flipping the character's direction
        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }

    void FixedUpdate()
    {
        // Check if the player is grounded using a raycast
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

        // Update animation parameter for grounded state
        anim.SetBool("IsGrounded", isGrounded);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}
