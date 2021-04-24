using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public CornController CornController;
    private PlayerInputs inputs;
    // Update is called once per frame
    void Update()
    {
        inputs.HorizonalMovement = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            inputs.JumpDown = true;
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
            inputs.JumpUp = true;


        CornController.SetInputs(ref inputs);
        ResetInput();
    }

    private void ResetInput()
    {
        inputs.HorizonalMovement = 0f;
        inputs.JumpDown = false;
        inputs.JumpUp = false;
    }
}
