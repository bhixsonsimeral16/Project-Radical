using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float topSpeed = 5f;
    [SerializeField] float acceleration = 0.5f;
    [SerializeField] float deceleration = 0.5f;
    [SerializeField] float jumpForce = 5f;

    Rigidbody2D rb;
    bool isGrounded = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    void HandlePlayerMovement()
    {
        if (isGrounded && (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Jump") > 0))
        {
            HandlePlayerJump();
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            HandlePlayerRun();
        }
    }

    void HandlePlayerJump()
    {
        if (rb.velocity.y == 0) 
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void HandlePlayerRun()
    {
        float direction = Input.GetAxis("Horizontal");

        if (direction > 0 && rb.velocity.x < topSpeed)
        {
            rb.AddForce(Vector2.right * acceleration, ForceMode2D.Impulse);
        }
        else if (direction < 0 && rb.velocity.x > -topSpeed)
        {
            rb.AddForce(Vector2.left * acceleration, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (isPlatform(collision.gameObject))
        {
            // Only set grounded if the player is colliding with the ground from above
            if (collision.GetContact(0).normal.y > 0)
            {
                isGrounded = true;
            }

            Platform platform = collision.gameObject.GetComponent<Platform>();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (isPlatform(collision.gameObject))
        {
            isGrounded = false;
        }
    }

    // TODO: What is OnCollisionStay2D?
    void OnCollisionStay2D(Collision2D collision)
    {
        if (isPlatform(collision.gameObject))
        {
            isGrounded = true;
        }
    }

    bool isPlatform(GameObject gameObject)
    {
        return (gameObject.GetComponent<Platform>() != null);
    }
}
