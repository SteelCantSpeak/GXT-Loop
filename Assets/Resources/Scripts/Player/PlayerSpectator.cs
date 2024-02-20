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

	//Camera Angles
	public Vector2 rotation = Vector2.zero;
	public float lookSpeed = 20f;
	public float moveSpeed = 20f;

	//POV position
	float MaxDist = 3;
	public Vector3 pos = Vector3.zero;
	Vector3 originalPos;

	// Start is called before the first frame update
	void Start()
    {
		_input = GetComponent<StarterAssetsInputs>();
		centerBoard = GameObject.FindGameObjectWithTag("Origin").transform;

		_cam = transform.parent.GetComponentInChildren<CinemachineVirtualCamera>();
		_cam.Follow = centerBoard;
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
			centerBoard.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0f);
		}

		if (_input.move != Vector2.zero)
		{
			pos.x = _input.move.x;
			pos.z = _input.move.y;

			Vector3 dir = Vector3.forward * Mathf.Sqrt(pos.sqrMagnitude);
			dir.y = 0f;
			centerBoard.Translate(dir * Time.fixedDeltaTime * moveSpeed);
		}
	}
}
