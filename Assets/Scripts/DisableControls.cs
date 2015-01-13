using UnityEngine;
using System.Collections;

public class DisableControls : MonoBehaviour 
{
	public static GameObject player;
	public static DisableControls Instance;
	PlayerController playerController;
	public bool tripped = false;
	
	float tripDelay = 1.5f;
	public float tripTimer;
	
	void Start()
	{
		player = GameObject.Find("Player");
		Instance = (DisableControls)GameObject.FindObjectOfType(typeof(DisableControls)); 
	}
	
	void Update()
	{
		if(tripped)
		{
		tripTimer -= Time.deltaTime;
		}
		else
		{
		tripTimer = tripDelay;
		}
		
		if(tripTimer < 0)
		{
			TripRecover.setFalse();
			
			if(Input.GetButton("Sidle") && tripped)
			{
				player.GetComponent<PlayerController>().enabled = true;
				PlayerController.getUp();
				tripped = false;
			}
		}
	}

	public static void toggleControl() 
	{
		Instance.tripTimer = Instance.tripDelay;
		player.GetComponent<PlayerController>().tripping = true;
		//player.GetComponent<Rigidbody2D>().drag = 0.01f;
		//player.GetComponent<Rigidbody2D>().mass = 0.01f;
		Instance.tripped = true;
		Debug.Log ("Disabled player controller");
	}
	
	public static void tripCancel()
	{
		Instance.tripped = false;
		Instance.tripTimer = Instance.tripDelay;
	}
}
