using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private LayerMask groundDetectionList;

    //PRIVATE VARIABLES
    private ActorStats _actorStats;
    private Rigidbody2D rbody;
    private bool isSprinting;
    private readonly float distance = 1.1f;
    private TrailRenderer trail;
    private bool facingRight = true;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
    }

    public void SetStats(ActorStats stats)
    {
        _actorStats = stats;
        currentSpeed = _actorStats.OriginalSpeed;
    }

    public void Move(Vector3 direction)
    {
        transform.position += (direction * (currentSpeed * Time.deltaTime));
    }

    public void OnMove2D(float horizontal)
    {
        var movement = horizontal * (currentSpeed * Time.deltaTime); //El valor va entre -1 (izquierda) y 1 (derecha). 
        transform.Translate(Mathf.Abs(movement), 0, 0); //El Mathf.Abs -> Math Absolute le saca los signos. Esto sirve porque al flippear el personaje siempre se mueve hacia adelante y el Flip me lo rota. 

        if (movement < 0 && facingRight) //Si el movimiento es positivo y esta mirando a la derecha...
        {
            Flip();
        }
        else if (movement > 0 && !facingRight) //Si el movimiento es negativo y no esta mirando a la derecha...
        {
            Flip();
        }
    }

    public void Jump()
    {
        if (_actorStats.CanJump && CheckIfGrounded())
        {
            var jumpForce = _actorStats.JumpForce * transform.up;
            rbody.AddForce(jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);
        if(hit.collider != null)
            return true;
        else
            return false;
    }

    public void Sprint()
    {
        if (isSprinting)
        {
            currentSpeed = _actorStats.OriginalSpeed;
            isSprinting = false;
        }
        else
        {
            currentSpeed = _actorStats.BuffedSpeed;
            isSprinting = true;
        }
    }

    private void Flip() //Solo flippea al personaje
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }
}
