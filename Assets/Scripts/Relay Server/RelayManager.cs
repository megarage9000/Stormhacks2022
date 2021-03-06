using System.Threading.Tasks;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.Authentication;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;

public class RelayManager : Singleton<RelayManager>
{
    [SerializeField]
    private string environment = "production";

    [SerializeField]
    private int maxConnections = 6;

    public bool IsRelayEnabled => Transport != null &&
        Transport.Protocol == UnityTransport.ProtocolType.RelayUnityTransport;

    public UnityTransport Transport =>
        NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();


    private string _joinCode = "";
    public string JoinCode
    {
        get => _joinCode;
        set
        {
            if(!string.IsNullOrEmpty(value))
            {
                _joinCode = value;
            }
        }
        
    }


    public async Task<RelayHostData> SetupRelay() {
        Debug.Log($"Starting Environment, with join number = {maxConnections}");
        InitializationOptions options = new InitializationOptions().SetEnvironmentName(environment);

        await UnityServices.InitializeAsync(options);

        Debug.Log("Checking authentication");
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log("Signing in Anonymously");
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        Debug.Log("Allocating");
        Allocation allocation = await Relay.Instance.CreateAllocationAsync(maxConnections);
        Debug.Log("Finished allocating");
        RelayHostData relayHostData = new RelayHostData
        {
            Key = allocation.Key,
            Port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            IPv4Address = allocation.RelayServer.IpV4,
            ConnectionData = allocation.ConnectionData
        };
        Debug.Log("Getting Join Code");
        relayHostData.JoinCode = await Relay.Instance.GetJoinCodeAsync(relayHostData.AllocationID);
        Debug.Log($"Successfully got join code {relayHostData.JoinCode}");
        JoinCode = relayHostData.JoinCode;
        Debug.Log("Setting Transport");
        Transport.SetRelayServerData(
            relayHostData.IPv4Address, 
            relayHostData.Port, 
            relayHostData.AllocationIDBytes, 
            relayHostData.Key, 
            relayHostData.ConnectionData);
        Debug.Log($"Host started relay server on code ${relayHostData.JoinCode}");
        return relayHostData;
    }

    public async Task<RelayJoinData>JoinRelay (string joinCode)
    {
        Debug.Log($"Joining in a relay server of ${maxConnections} with code {joinCode}");
        InitializationOptions options = new InitializationOptions().SetEnvironmentName(environment);

        await UnityServices.InitializeAsync(options);

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        JoinAllocation allocation = await Relay.Instance.JoinAllocationAsync(joinCode);

        RelayJoinData relayJoinData = new RelayJoinData
        {
            Key = allocation.Key,
            Port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            ConnectionData = allocation.ConnectionData,
            HostConnectionData = allocation.HostConnectionData,
            IPv4Address = allocation.RelayServer.IpV4,
            JoinCode = joinCode
        };

        Transport.SetRelayServerData(
            relayJoinData.IPv4Address, 
            relayJoinData.Port, 
            relayJoinData.AllocationIDBytes, 
            relayJoinData.Key, 
            relayJoinData.ConnectionData, 
            relayJoinData.HostConnectionData);

        Debug.Log($"Client Joined game with code {joinCode}");
        return relayJoinData;
    }

    
}
