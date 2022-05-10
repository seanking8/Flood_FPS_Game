using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Ladder : MonoBehaviour 

{
	public Transform player;
	public float speedUpDown = 12.0f;
	private FirstPersonController FPSInput;

void Start()
{
	FPSInput = GetComponent<FirstPersonController>();
}

void OnTriggerEnter(Collider col)
{
	if(col.gameObject.tag == "Ladder")
	{
		FPSInput.enabled = false;
	}
}

void OnTriggerExit(Collider col)
{
	if(col.gameObject.tag == "Ladder")
	{
		FPSInput.enabled = true;
	}
}

void OnTriggerStay()
{
	if(Input.GetKey("w"))
	{
			player.transform.position += Vector3.up / speedUpDown;
	}
	
	if(Input.GetKey("s"))
	{
			player.transform.position += Vector3.down / speedUpDown;
	}
}

}