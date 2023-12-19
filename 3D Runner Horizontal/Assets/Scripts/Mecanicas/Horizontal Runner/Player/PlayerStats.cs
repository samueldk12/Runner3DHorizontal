using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float _forceJump = 4.5f;

    [SerializeField]
    private float velocity = 1;

    [SerializeField]
    private float acceleration = 0.01f;


    public float GetForceJump()
    {
        return _forceJump;
    }

    public void SetForceJump(float forceJump)
    {
        _forceJump = forceJump;
    }

    public void FixedUpdate()
    {
        velocity += acceleration;
    }

    public float GetVelocity() {  
        return velocity; 
    }

    public void SetVelocity(float velocity)
    {
        this.velocity = velocity;
    }

    public void SetAcceleration(float acceleration)
    {
        this.acceleration = acceleration;
    }


}
