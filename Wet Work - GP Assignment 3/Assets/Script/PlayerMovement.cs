using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float movementSpeed = 10;
    [SerializeField] GameObject playerSprite;

    Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        myRigidbody2D.velocity = new UnityEngine.Vector2(deltaX, deltaY);

        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if(direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            playerSprite.transform.rotation = Quaternion.RotateTowards(playerSprite.transform.rotation, toRotation, 1000 * Time.fixedDeltaTime);
        }
    }

    public Vector3 GetPlayerVelocity()
    {
        return myRigidbody2D.velocity;
    }

    public GameObject GetPlayerSprite()
    {
        return playerSprite;
    }
}
