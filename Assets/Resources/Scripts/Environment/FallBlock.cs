using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : MonoBehaviour
{
	public int destroyTime = 1;
	float fallspeed = 5f;
	float stayTime =10f;

	public enum Blockstate
	{
		SteppedOn,
		Shot,
		Standard
	}
	public Blockstate state = Blockstate.Standard;

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			state = Blockstate.SteppedOn;
			Destroy(this.gameObject, destroyTime);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			stayTime -= 1 * Time.deltaTime;
			if (stayTime <= 0f)
			{
				state = Blockstate.SteppedOn;
			}
		}
	}

	private void FixedUpdate()
	{
		if (state == Blockstate.SteppedOn)
		{

			transform.position -= new Vector3(0, fallspeed, 0) * Time.fixedDeltaTime;
			fallspeed += fallspeed * Time.fixedDeltaTime;
		}
		else if (state == Blockstate.Shot)
		{
			Destroy(this.gameObject);
		}
	}

	public void GetShot()
	{
		state = Blockstate.Shot;
	}
}
