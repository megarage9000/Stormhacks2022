using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FPSMoveNetwork : NetworkBehaviour
{
    public NetworkVariable<Vector3> rotationChange = new NetworkVariable<Vector3>();
    public NetworkVariable<Vector3> rotationHeadChange = new NetworkVariable<Vector3>();
    public NetworkVariable<Vector3> playerPosition = new NetworkVariable<Vector3>();
    public scr_CharControl controls;
    public Camera playerCamera;

    public override void OnNetworkSpawn()
    {
        controls = GetComponent<scr_CharControl>();
        if(!IsOwner && playerCamera) {
            Destroy(playerCamera);
        }
    }
    
    // Random position displacement
    // From: https://github.com/dilmerv/UnityMultiplayerPlayground/blob/master/Assets/Scripts/PlayerControl.cs
    [SerializeField]
    private Vector2 randomPosition = new Vector2(-4, 4);
    void Start() {
        if(IsServer && IsOwner){
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void Update()
    {   
        Move();
    }

    public void Move() {
        if(IsClient && IsOwner) {
            UpdateClient();
        } 
        UpdateServer();
    }

    // Get value changes
    private void UpdateServer() {
        controls.MoveCharacter(playerPosition.Value);
        controls.RotateCharacter(rotationChange.Value);
        controls.RotateCharacterHead(rotationHeadChange.Value);
    }

    // Send via RPC
    private void UpdateClient() {
        controls.CalculateCharacterMovements();
        UpdateClientMovementServerRpc(controls.GetBodyRotation(), controls.GetCameraRotation(), controls.GetCalculatedPosition());
    }

    [ServerRpc]
    public void UpdateClientMovementServerRpc(Vector3 rotation, Vector3 rotationHead, Vector3 position) {
        playerPosition.Value = position;
        rotationChange.Value = rotation;
        rotationHeadChange.Value = rotationHead;
    }

    public Vector3 GetRandomPosition() {
        return new Vector3(
            Random.Range(randomPosition.x, randomPosition.y),
            0,
            Random.Range(randomPosition.x, randomPosition.y)
        );
    }
    [ServerRpc]
    public void RandomizePositionServerRpc() {
        playerPosition.Value = GetRandomPosition();
    }
}
