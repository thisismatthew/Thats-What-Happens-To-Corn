using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CornController))]
public class PlayerInput : MonoBehaviour
{

    private CornController _cornController;
    private PlayerInputs inputs;

    private void Start()
    {
        _cornController = GetComponent<CornController>();
    }
    // Update is called once per frame
    void Update()
    {
        inputs.HorizonalMovement = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            inputs.JumpDown = true;
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
            inputs.JumpUp = true;


        _cornController.SetInputs(ref inputs);
        ResetInput();
    }

    private void ResetInput()
    {
        inputs.HorizonalMovement = 0f;
        inputs.JumpDown = false;
        inputs.JumpUp = false;
    }
}
