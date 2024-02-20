using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : MonoBehaviour
{
	public int destroyTime = 1;
	float fallspeed = 5f;
	public bool steppedOff = false;
	public bool shot = false;


	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			steppedOff = true;
			Destroy(this.gameObject, destroyTime);
		}
	}



	private void FixedUpdate()
	{
		if (steppedOff)
		{

			transform.position -= new Vector3(0, fallspeed, 0) * Time.fixedDeltaTime;
			fallspeed += fallspeed * Time.fixedDeltaTime;
		}
		if (shot)
		{
			Destroy(this.gameObject);
		}
	}
}
