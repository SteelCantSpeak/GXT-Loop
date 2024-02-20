using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FallBlock : NetworkBehaviour
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
	public NetworkVariable<Blockstate> state = new NetworkVariable<Blockstate>(Blockstate.Standard, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			state.Value = Blockstate.SteppedOn;

		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			stayTime -= 1 * Time.deltaTime;
			if (stayTime <= 0f)
			{
				state.Value = Blockstate.SteppedOn;
			}
		}
	}

	private void FixedUpdate()
	{
		if (state.Value == Blockstate.SteppedOn)
		{

			transform.position -= new Vector3(0, fallspeed, 0) * Time.fixedDeltaTime;
			fallspeed += fallspeed * Time.fixedDeltaTime;
			if (fallspeed >= 10f)
			{
				this.gameObject.GetComponent<NetworkObject>().Despawn(true);
			}
		}
		else if (state.Value == Blockstate.Shot)
		{
			Destroy(this.gameObject);
		}
	}

	public void GetShot()
	{
		state.Value = Blockstate.Shot;
	}
}
