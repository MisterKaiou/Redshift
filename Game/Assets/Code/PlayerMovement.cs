using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D colliderFront, colliderSide;

    private Vector2 movement;

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
        rb.MovePosition(new Vector2(
                            Mathf.Clamp(rb.position.x, -2.75f, 2.75f),
                            Mathf.Clamp(rb.position.y, -1.30f, 1.95f)) 
            + movement 
            * moveSpeed * Time.fixedDeltaTime);

        InvertAnimation(Input.GetAxisRaw("Horizontal"));
        ToggleColliderBox();
    }

    private void ToggleColliderBox()
    {
        bool ColliderFrontIsActive = colliderFront != null;
        bool ColliderSideIsActive = colliderSide != null;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (ColliderFrontIsActive) colliderFront.enabled = false;
            if (ColliderSideIsActive) colliderSide.enabled = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if (ColliderFrontIsActive) colliderFront.enabled = true;
            if (ColliderSideIsActive) colliderSide.enabled = false;
        }
    }

    private void InvertAnimation(float inputValue)
    {
        if (inputValue < 0) GetComponent<SpriteRenderer>().flipX = true;
        else if (inputValue > 0) GetComponent<SpriteRenderer>().flipX = false;
    }
}
