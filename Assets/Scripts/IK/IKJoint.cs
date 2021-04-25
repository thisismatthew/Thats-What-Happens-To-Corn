using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKJoint : MonoBehaviour
{
    public IKJoint m_child;
    // Start is called before the first frame update
    
    public void Rotate(float _angle)
    {
        transform.Rotate(Vector2.up * _angle);
    }
}
