using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
	public void serverButton()
	{
		NetworkManager.Singleton.StartServer();
	}

	public void clientButton()
	{
		NetworkManager.Singleton.StartClient();
	}

	public void hostButton()
	{
		NetworkManager.Singleton.StartHost();
	}
}
