using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // so i want a camera controller where the player can move around a bit without the camera moving,
    // like get to the edges and then the camera lerps over a bit. 

    public float MaxDistanceBeforeFollow = 3f;
    public float FollowSpeed = 1f;

    // the camera will move if projctedPos and currentPos are not equal, projectedPos gets moved relative
    // to the distance of the player from the currentPos. 
    private Vector2 _target;
    private Vector2 _currentPos;
    private Vector2 _projectedPos;

    void Start()
    {
        _currentPos = transform.position;
        _projectedPos = _target;
    }

    void LateUpdate()
    {
        if (_currentPos != _projectedPos)
        {
            Vector3 newPos = Vector2.Lerp(_currentPos, _projectedPos, Time.deltaTime * FollowSpeed);
            newPos.z = -10;
            transform.position = newPos;
            _currentPos = transform.position;
        }

        if (Vector2.Distance(_currentPos, _target) > MaxDistanceBeforeFollow)
        {
            _projectedPos = _target;

        }
    }

    public void SetTarget(Vector2 newTarget)
    {
        _target = newTarget;
    }
}
