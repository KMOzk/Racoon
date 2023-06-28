using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;         // Movement speed
    [SerializeField] private float jumpForce = 5f;         // Jump force
    [SerializeField] private Transform groundCheck;        // Ground check object
    [SerializeField] private LayerMask groundLayer;        // Layer mask for the ground

    private Rigidbody rigidBody;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;
    private Vector3 moveDirection;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Get input axes
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Apply movement
        MovePlayer();

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        // Calculate the desired movement vector
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;

        // Set the vertical velocity to preserve gravity
        movement.y = rigidBody.velocity.y;

        // Move the player
        rigidBody.velocity = movement;
    }

    private void Jump()
    {
        // Apply jump force
        rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}