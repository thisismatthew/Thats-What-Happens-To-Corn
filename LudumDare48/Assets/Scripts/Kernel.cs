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
    public bool InFrame = true;
    public Transform RightLegAnimationTarget, LeftLegAnimationTarget, RightFoot, LeftFoot, RightUpdateTarget, LeftUpdateTarget;

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
            //find the amount we are over the speed limit and offset it. 
            Vector2 diff = new Vector2(-(_rigidbody.velocity.x - MaxSpeed), 0);
            newVelocity += diff;
        }

        if (_rigidbody.velocity.x < -MaxSpeed)
        {
            //find the amount we are under the speed limit and offset it. 
            Vector2 diff = new Vector2(-(_rigidbody.velocity.x + MaxSpeed), 0);
            newVelocity += diff;
        }

        

        _rigidbody.velocity += newVelocity;
        
       
    }

    public void Shake(float waitTime)
    {
        Mathf.Clamp(waitTime, 0, 1);
        _rigidbody.velocity += new Vector2(Random.Range(waitTime, -waitTime), Random.Range(waitTime, -waitTime));
    }

    bool IsGrounded()
    {
        
        Vector2 direction = Vector2.down;
        float distance = 1f;
        Color debugGroundColor = Color.red;
        Vector2 position = transform.position;
        position.y += distance;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, CollisionLayer);
        if (hit.collider != null)
        {
            debugGroundColor = Color.green;
            Debug.DrawLine(position, (position + (Vector2.down * distance)), debugGroundColor);
            return true;
        }

        Debug.DrawLine(position, (position + (Vector2.down * distance)), debugGroundColor);
        return false;
    }

    public void Update()
    {
        if (IsGrounded())
        {
            SnapFeetToGround(ref RightFoot, ref RightLegAnimationTarget, RightUpdateTarget);
            SnapFeetToGround(ref LeftFoot, ref LeftLegAnimationTarget, LeftUpdateTarget);

        }
    
    }

    public void SnapFeetToGround(ref Transform foot, ref Transform origin, Transform newTarget)
    {
        RaycastHit2D newTargetHit = Physics2D.Raycast(newTarget.position, Vector2.down, 3f, CollisionLayer);
        Debug.DrawLine(newTarget.position, newTargetHit.point, Color.blue);

        RaycastHit2D hit = Physics2D.Raycast(origin.position, Vector2.down, 3f, CollisionLayer);
        Debug.DrawLine(origin.position, hit.point, Color.cyan);

        //if the foot postition gets too far from the target position
        //move the foot and the ray origin over to the new target
        if (Vector2.Distance(foot.transform.position, newTargetHit.point)> 1f)
        {
            //Debug.Log("pointMoved");
            origin.position = newTarget.position;
        }

        if (Vector2.Distance(foot.transform.position, hit.point) > 0.1f)
            foot.position = Vector2.Lerp(foot.position, hit.point, 0.5f);

    }


}
