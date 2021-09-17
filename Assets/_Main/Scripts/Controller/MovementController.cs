using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float currentSpeed;

    //PRIVATE VARIABLES
    private ActorStats _actorStats;
    private Rigidbody rbody;
    private bool isSprinting;
    private float distance = 1.1f;
    private TrailRenderer trail;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
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

    public void Jump()
    {
        if (_actorStats.CanJump && CheckIfGrounded())
        {
            var jumpForce = _actorStats.JumpForce * transform.up;
            rbody.AddForce(jumpForce, ForceMode.Impulse);
        }
    }

    public bool CheckIfGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(new Ray(transform.position, Vector3.down), out hit, distance);
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
}
