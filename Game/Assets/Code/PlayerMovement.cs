using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        InvertAnimation(Input.GetAxisRaw("Horizontal"));
        CheckFacingDirection(movement);
    }

    private void CheckFacingDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
    }

    private void InvertAnimation(float inputValue)
    {
        if(inputValue < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(inputValue > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
