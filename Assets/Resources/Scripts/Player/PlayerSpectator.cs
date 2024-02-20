using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;

public class PlayerSpectator : MonoBehaviour
{
	private StarterAssetsInputs _input;
	public CinemachineVirtualCamera _cam;
	Transform centerBoard;
	Transform centerRotate;

	//Camera Angles
	public Vector2 rotation = Vector2.zero;
	public float lookSpeed = 20f;
	public float moveSpeed = 20f;

	public Vector3 pos = Vector3.zero;
	Vector3 originalPos;

	// Start is called before the first frame update
	void Start()
    {
		_input = GetComponentInParent<StarterAssetsInputs>();
		centerBoard = GameObject.FindGameObjectWithTag("Origin").transform;
		centerRotate = centerBoard.GetChild(0);

		_cam.Follow = centerRotate;
		_cam.LookAt = centerBoard;

		originalPos = centerBoard.position;
    }

	// Update is called once per frame
	void FixedUpdate()
	{
		if (_input.look != Vector2.zero)
		{
			float delta = Mathf.Clamp(lookSpeed * Time.fixedDeltaTime, -10f, 10f);
			rotation.x += _input.look.x * delta;
			rotation.y -= _input.look.y * delta;

			rotation.y = Mathf.Clamp(rotation.y, 0, 89);
			centerRotate.localRotation = Quaternion.Euler(rotation.y, 0f, 0f);
			centerBoard.localRotation = Quaternion.Euler(0f, rotation.x, 0f);
		}

		if (_input.move != Vector2.zero)
		{
			pos.x = _input.move.x;
			pos.z = _input.move.y;

			Vector3 dir = (centerBoard.forward * pos.z) + (centerBoard.right * pos.x);
			centerBoard.position += dir * moveSpeed *Time.fixedDeltaTime;
		}
	}
}
