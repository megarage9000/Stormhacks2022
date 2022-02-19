using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FPSMoveNetwork : NetworkBehaviour
{

    public NetworkVariable<Vector3> movementChange = new NetworkVariable<Vector3>();
    public NetworkVariable<Vector3> rotationChange = new NetworkVariable<Vector3>();

    public NetworkVariable<Vector3> rotationHeadChange = new NetworkVariable<Vector3>();
    public scr_CharControl controls;

    
    void Awake() {
        controls = GetComponent<scr_CharControl>();
    }

    // Random position displacement
    // From: https://github.com/dilmerv/UnityMultiplayerPlayground/blob/master/Assets/Scripts/PlayerControl.cs
    [SerializeField]
    private Vector2 defaultInitialPositionOnPlane = new Vector2(-4, 4);
    void Start() {
        transform.position = new Vector3(Random.Range(defaultInitialPositionOnPlane.x, defaultInitialPositionOnPlane.y), 5,
                   Random.Range(defaultInitialPositionOnPlane.x, defaultInitialPositionOnPlane.y));

        if(IsServer){
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void Update()
    {   
        if(IsClient && IsOwner) {
            UpdateClient();
        } 
        UpdateServer();
    }

    // Get value changes
    private void UpdateServer() {
        controls.MoveCharacter(movementChange.Value);
        controls.RotateCharacter(rotationChange.Value);
        controls.RotateCharacterHead(rotationHeadChange.Value);
    }

    // Send via RPC
    private void UpdateClient() {
        controls.CalculateMovement();
        controls.CalculateBodyRotation();
        controls.CalculateView();
        UpdateClientMovementServerRpc(controls.GetBodyRotation(), controls.GetCameraRotation(), controls.GetMovementInput());
    }

    [ServerRpc]
    public void UpdateClientMovementServerRpc(Vector3 rotation, Vector3 rotationHead, Vector3 movement) {
        movementChange.Value = movement;
        rotationChange.Value = rotation;
        rotationHeadChange.Value = rotationHead;
    }
}
