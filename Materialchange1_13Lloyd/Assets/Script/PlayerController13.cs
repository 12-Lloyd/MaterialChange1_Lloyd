using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerController13 : MonoBehaviour
{
    float speed = 10.0f;
    float xLimit = 19.8f;
    float zLimit = 19.8f;
    float yLimit = 0.6f;
    bool isOnGround;
    float jumpForce = 10.0f;
    float gravityModifier = 2.0f;
    Renderer playerRdr;
    Rigidbody playerRb;
    public Material[] playerMtrs;
    // Start is called before the first frame update
    void Start()
    {
        isOnGround = true;
        Physics.gravity *= gravityModifier;
        playerRdr = GetComponent<Renderer>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        //move player (GameObject) According to user interaction
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);
        //Block object from going out of bound
        if (transform.position.z < -zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);
            playerRdr.material.color = playerMtrs[2].color;
        }
        else if (transform.position.z > zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
            playerRdr.material.color = playerMtrs[3].color;
        }
        if (transform.position.x < -xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[4].color;
        }
        else if (transform.position.x > xLimit)
        {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[5].color;
        }

        if (transform.position.y < yLimit)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            }
        }
        PlayerJump();
        //color change
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gameplane"))
        {
            isOnGround = true;
            playerRdr.material.color = playerMtrs[0].color;
        }
    }
    private void PlayerJump()
    {
        if (isOnGround == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                isOnGround = false;

                playerRdr.material.color = playerMtrs[1].color; 
            }
        }
    }
}
