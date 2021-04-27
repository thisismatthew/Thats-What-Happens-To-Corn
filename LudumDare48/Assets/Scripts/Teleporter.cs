using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public CornController Controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Controller.CornKernels.Remove(collision.GetComponent<Kernel>());
        Destroy(collision.gameObject, 3f);
    }
}
