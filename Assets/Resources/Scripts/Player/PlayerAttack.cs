using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;

public class PlayerAttack : MonoBehaviour
{

	private StarterAssetsInputs _input;
	public CinemachineVirtualCamera _cam;

	public float AimFOV = 20;
	public float BaseFOV = 40;
	public GameObject Particles;

	// Start is called before the first frame update
	void Start()
    {
		_input = GetComponent<StarterAssetsInputs>();
		_cam = transform.parent.GetComponentInChildren<CinemachineVirtualCamera>();
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

		//track if Player falls
		if (transform.position.y <= -10f)
		{
			Debug.Log("lost");
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
				Instantiate(Particles, hit.point, Quaternion.Euler(hit.normal));
			}
			else
			{
				Debug.Log(hit.collider.gameObject.name);
			}
		}
	}
}
