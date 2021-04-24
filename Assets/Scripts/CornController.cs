using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct PlayerInputs
{
    public float HorizonalMovement;
    public bool JumpDown;
    public bool JumpUp;
}

[RequireComponent(typeof(PlayerInput))]
public class CornController : MonoBehaviour
{
    public List<Kernel> CornKernels;
    public CameraController Camera;

    [Header("Kernal Movement")]
    public float Acceleration = 0.01f;
    public float MaxSpeed = 6f;
    private Vector2 _velocity = Vector2.zero;
    private float _horizontalInput = 0f;

    [Header("Kernal Jumping")]
    public float ChargeUpTime = 0.5f;
    public float RegularJumpHeight = 10f;
    public float ChargedJumpHeight = 15f;
    public float TimeToJumpApex = 0.5f;
    private bool _jumpCharging = false;
    private bool _jumpRequested = false;
    private bool _jumpCompleted = false;
    public float _jumpChargeTime = 0f;

    [Header("Misc")]
    public LayerMask CollisionLayer;
    public float ShakeSpeed = 0.2f;
    public float ShakeAmount = 0.2f;
    public bool Debugging = true;

    private void Start()
    {
        foreach (Kernel k in CornKernels)
        {
            k.CollisionLayer = CollisionLayer;
            k.MaxSpeed = MaxSpeed;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_jumpCharging)
        {
            _jumpChargeTime += Time.deltaTime;
        }

        HandleJumping();
        HandleCamera();

        HandleHorizontalMovement();
        foreach (Kernel k in CornKernels)
        {
            
/*            if (_jumpCharging)
                k.Shake(ShakeSpeed, ShakeAmount);*/
            k.Move(_velocity);
        }

        if (_jumpCompleted)
        {
            _velocity.y = 0;
            _jumpCompleted = false;
            _jumpChargeTime = 0;
            _jumpRequested = false;
        }
    }

    private void Update()
    {
        //if all the corn bits are gone in this scene call the next one!
        //might need a special case for the final one. 

        if(CornKernels.Count == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void SetInputs(ref PlayerInputs inputs)
    {
        if (inputs.JumpDown)
        {
            _jumpCharging = true;
        }
        if (inputs.JumpUp)
        {
            _jumpCharging = false;
            _jumpRequested = true;
        }
        _horizontalInput = inputs.HorizonalMovement;
    }

    private void HandleHorizontalMovement()
    {
        if (_horizontalInput > 0.01f ||_horizontalInput < -0.01f)
        {
            _velocity.x += _horizontalInput * Acceleration;
        }
        else
        {
            _velocity.x = 0;
        }
    }

    private void HandleJumping()
    {
        float gravity, jumpHeight;
        //check if jump has been requested
        if (_jumpRequested)
        {
            Debug.Log("Jumping");
            //check if the player charged enough for a super jump. 
            if (_jumpChargeTime > ChargeUpTime)
                jumpHeight = ChargedJumpHeight;
            else
                jumpHeight = RegularJumpHeight;
            _jumpCompleted = true;
            //calculate jump velocity
            gravity = CalculateJumpGravity(jumpHeight);
            _velocity.y = gravity * TimeToJumpApex;
        }
    }

    private void HandleCamera()
    {
        //find the center point of all of the kernels.
        float totalX = 0f;
        float totalY = 0f;
        foreach (Kernel k in CornKernels)
        {
            totalX += k.gameObject.transform.position.x;
            totalY += k.gameObject.transform.position.y;

        }
        //set the camera to follow that
        Vector2 newTarget = new Vector2( totalX / CornKernels.Count,  totalY / CornKernels.Count);
        Camera.SetTarget(newTarget);
        
        if (Debugging)
        {
            foreach (Kernel k in CornKernels)
            {
                Debug.DrawLine(k.transform.position, newTarget);
            }
        }
        
    }

    private float CalculateJumpGravity(float jumpHeight)
    {
        return (2 * jumpHeight) / Mathf.Pow(2, TimeToJumpApex);
    }

    
}
