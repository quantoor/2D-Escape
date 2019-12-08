﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 30f;

    Rigidbody rb;
    SphereCollider collider;
    bool isGrounded;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
    }

    private void FixedUpdate() {
        PlayerMovement();
    }

    private void PlayerMovement() {
        Move();
        Jump();
    }

    private void Move() {
        float controlThrowHorizontal = Input.GetAxis("Horizontal"); // value is between -1 to +1
        rb.velocity = new Vector3(controlThrowHorizontal * moveSpeed, rb.velocity.y, rb.velocity.z);

        if (GameController.is3d) {
            float controlThrowVertical = Input.GetAxis("Vertical"); // value is between -1 to +1
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, controlThrowVertical * moveSpeed);
        }
    }

    private void Jump() {
        if (!GameController.canJump) {
            return;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            Vector3 jumpForceVector = -Physics.gravity * jumpForce;

            if (GameController.isGravityInverted)
                jumpForceVector *= -1;

            rb.AddForce(jumpForceVector);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision) {
        isGrounded = false;
    }
}
