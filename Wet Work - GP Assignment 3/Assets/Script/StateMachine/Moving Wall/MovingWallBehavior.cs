using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallBehavior : StateMachine
{
    PlayerHealth player;
    Rigidbody2D movingWallRigidbody2D;

    [Header("Movement")]
    [SerializeField] float movementSpeed = 100f;
    [SerializeField] bool isHorizontalMovement = false;
    [SerializeField] public Vector3 idleTarget = new Vector3(0, 1, 0);
    [SerializeField] public int layerMaskIndex = 8;
    [HideInInspector]
    public GateBehavior gate;
    [HideInInspector]
    public bool isHitWall = false;

    private void Start()
    {
        gate = FindObjectOfType<GateBehavior>();
        movingWallRigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerHealth>();
        SetNewState(new Stationary(this));
        SetState(GetNewState());
    }

    private void FixedUpdate()
    {
        SetState(GetNewState());
    }

    public void MoveWall()
    {
        movingWallRigidbody2D.velocity = transform.TransformDirection(idleTarget).normalized * movementSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>())
            player.KillPlayer();
        isHitWall = true;
        idleTarget *= -1;
    }

    public bool IsHitWall()

    {
        return isHitWall;
    }
}
