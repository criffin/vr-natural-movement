using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkManager))]
public class NetworkManagerActivator : MonoBehaviour 
{
	[SerializeField] bool isClient;

	NetworkManager networkManager;

	void Start () 
	{
		networkManager = GetComponent<NetworkManager>();
		StartManager();
	}

	void StartManager()
	{
		if (isClient) networkManager.StartClient();
		else networkManager.StartServer();
	}

	void StopManager()
	{
		if (isClient) networkManager.StopClient();
		else networkManager.StopServer();
	}

	public void SetAddress(string networkAddress)
	{
		StopManager();
		networkManager.networkAddress = networkAddress;
		StartManager();
	}
}
