using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorControl : MonoBehaviour
{
	//Get Player
	SkinnedMeshRenderer _playerSkin;
	//Get the Colour
	public Color _color;

    // Start is called before the first frame update
    void Start()
    {
		SetColour();

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void SetColour()
	{
		_playerSkin = this.GetComponentInChildren<SkinnedMeshRenderer>();
		_playerSkin.material.color = _color;
	}
}
