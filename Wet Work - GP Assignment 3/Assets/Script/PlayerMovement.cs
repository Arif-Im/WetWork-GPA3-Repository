using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float movementSpeed = 10;
    [SerializeField] float minX = 2, minY = 2, maxX = 5, maxY = 5;

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
    }

    public Vector3 GetPlayerVelocity()
    {
        return myRigidbody2D.velocity;
    }
}
