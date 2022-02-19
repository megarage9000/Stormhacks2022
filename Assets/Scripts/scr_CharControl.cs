using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static scr_Models;

public class scr_CharControl : MonoBehaviour
{
    private CharacterController characterController;
    private DefaultInput defaultInput;
    public Vector2 input_Movement;
    public Vector2 input_View;

    private Vector3 newCameraRotation;
    private Vector3 newCharacterRotation;
    private Vector3 currMovementInput;
    private Vector3 currCameraRotation;
    private Vector3 currBodyRotation;

    [Header("References")]
    public Transform cameraHolder;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    public float viewClampMin = -70;
    public float viewClampMax = 80;

    // FOR DEBUGGING:
    private bool canMove = true;

    private void Awake()
    {
        defaultInput = new DefaultInput();

        defaultInput.Character.Movement.performed += e => input_Movement = e.ReadValue<Vector2>();
        defaultInput.Character.View.performed += e => input_View = e.ReadValue<Vector2>();

        defaultInput.Enable();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;

        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // CalculateView();
        // CalculateMovement();
        if(Input.GetKeyDown(KeyCode.KeypadEnter)) {
            canMove = !canMove;
        }
    }

    //Calculates Camera rotation
    public void CalculateView() 
    {
        if(canMove){
            newCameraRotation.x += playerSettings.ViewYSensitivity * (playerSettings.ViewYInverted ? input_View.y: -input_View.y) * Time.deltaTime;
            newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampMin, viewClampMax);             //limits
            currCameraRotation = newCameraRotation;
            // cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
        }

    }

    public void CalculateBodyRotation() {
        if(canMove){
            newCharacterRotation.y += playerSettings.ViewXSensitivity * (playerSettings.ViewXInverted ? -input_View.x : input_View.x) * Time.deltaTime;
            currBodyRotation = newCharacterRotation;
            // transform.rotation = Quaternion.Euler(newCharacterRotation);
        }
    }

    //Calculates Player movement
    public void CalculateMovement()
    {
        if(canMove){
            var verticalSpeed = playerSettings.WalkingForwardSpeed * input_Movement.y * Time.deltaTime;
            var horizontalSpeed = playerSettings.WalkingStrafeSpeed * input_Movement.x * Time.deltaTime;

            var newMovementSpeed = new Vector3(horizontalSpeed, 0, verticalSpeed);
            newMovementSpeed = transform.TransformDirection(newMovementSpeed);
            currMovementInput = newMovementSpeed;
            // characterController.Move(newMovementSpeed);
        }
    }

    public void CalculateCharacterMovements() {
        if(canMove) {
            CalculateMovement();
            CalculateBodyRotation();
            CalculateView();
        }
    }

    //Getter Methods
    public Vector3 GetCalculatedPosition()
    {
        return transform.position + currMovementInput;
    }

    public Vector3 GetCameraRotation()
    {
        return currCameraRotation;
    }

    public Vector3 GetBodyRotation() {
        return currBodyRotation;
    }

    //Setter Methods
    public void MoveCharacter(Vector3 newPosition) {
        Vector3 movement = (newPosition / Time.deltaTime) - transform.position;
        characterController.Move(movement * Time.deltaTime);
    }

    public void RotateCharacterHead(Vector3 rotation){
        cameraHolder.localRotation = Quaternion.Euler(rotation);
    }

    public void RotateCharacter(Vector3 rotation){
        transform.localRotation = Quaternion.Euler(rotation);
    }
}
