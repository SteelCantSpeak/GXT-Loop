using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;
using Unity.Netcode;

public class PlayerAttack : NetworkBehaviour
{
	public GameObject spectator;
	public GameObject player;

	public GameObject model;
	[Space]
	private StarterAssetsInputs _input;
	public CinemachineVirtualCamera _cam;

	int mapSize = 4;
	public float AimFOV = 20;
	public float BaseFOV = 40;
	public GameObject Particles;

	// Start is called before the first frame update
	void Start()
    {
		player.transform.position = new Vector3(Random.Range(-mapSize, mapSize), 1f, Random.Range(-mapSize, mapSize));
		_input = GetComponentInParent<StarterAssetsInputs>();
		_cam = player.GetComponentInChildren<CinemachineVirtualCamera>();
		spectator.SetActive(false);

		if (!IsOwner)
		{
			Destroy(_input);
			Destroy(_cam);
			this.enabled = false;
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (_input.aim)
		{
			//Aim reticle
			//Animation changea
			if (_cam.m_Lens.FieldOfView != AimFOV)
			{
				_cam.m_Lens.FieldOfView = AimFOV;
			}
			
		}
		else
		{
			if (_cam.m_Lens.FieldOfView != BaseFOV)
			{
				_cam.m_Lens.FieldOfView = BaseFOV;
			}
		}


		if (_input.shoot)
		{
			_input.shoot = false;
			Fire();
		}

		if (model != null)
		{
			//track if Player falls
			if (model.transform.position.y <= -10f)
			{
				Debug.Log("lost");
				player.GetComponent<NetworkObject>().Despawn(true);
				spectator.SetActive(true);
				_cam = spectator.GetComponentInChildren<CinemachineVirtualCamera>();
			}
		}
		
	}

	void Fire()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

		if (Physics.Raycast(ray, out hit, 1000f))
		{
			if (hit.collider.GetComponent<FallBlock>())
			{
				hit.collider.GetComponent<FallBlock>().GetShot();
				GameObject fx = Instantiate(Particles, hit.point, Quaternion.Euler(hit.normal));
				fx.GetComponent<NetworkObject>().Spawn(true);
			}
			else
			{
				Debug.Log(hit.collider.gameObject.name);
			}
		}
	}
}
