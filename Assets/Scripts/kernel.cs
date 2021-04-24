using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Kernel : MonoBehaviour
{
    public LayerMask CollisionLayer;
    public bool _grounded;
    private Rigidbody2D _rigidbody;
    public float MaxSpeed;
    public bool debugging = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 newVelocity)
    {
        //make sure we only add to the y axis when we are grounded
        if(!IsGrounded())
            newVelocity.y = 0;

        //make sure that we are adding negative force to keep us under the speed limit
        if (_rigidbody.velocity.x > MaxSpeed)
        {
            Debug.Log("MAX SPEED");
            //find the amount we are over the speed limit and offset it. 
            Vector2 diff = new Vector2(-(_rigidbody.velocity.x - MaxSpeed), 0);
            newVelocity += diff;
        }

        if (_rigidbody.velocity.x < -MaxSpeed)
        {
            Debug.Log("MAX SPEED NEGATIVE");
            //find the amount we are under the speed limit and offset it. 
            Vector2 diff = new Vector2(-(_rigidbody.velocity.x + MaxSpeed), 0);
            newVelocity += diff;
        }



        _rigidbody.velocity += newVelocity;
        
       
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, CollisionLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    public void Shake(float speed, float amount)
    {
        transform.position = new Vector2(transform.position.x + Mathf.Sin(Time.time * speed) * amount, transform.position.y + Mathf.Sin(Time.time * speed) * amount);
    }

}
