using UnityEngine;
using System.Collections;

public class TripRecover : MonoBehaviour 
{
	public Texture2D cursorGrab;
	CursorMode cursorMode = CursorMode.Auto;
	public static GameObject player;
	public PlayerController playerController;
	public DisableControls disableControls;
	public bool trip = false;
	
	public static TripRecover Instance;

	void Start () 
	{
		player = GameObject.Find("Player");
		Instance = (TripRecover)GameObject.FindObjectOfType(typeof(TripRecover)); 
	}
	
	void Update()
	{
		//trip = playerController.tripping;
	}

	void OnMouseDown() 
	{
		if(trip == true)
		{
			PlayerController.tripSave();
			player.GetComponent<PlayerController>().tripping = false;
			DisableControls.tripCancel();
			Debug.Log("Saved Trip");
		}
		else
			Debug.Log("Not Tripping");
	}
	
	void OnMouseEnter()
	{
		if(trip == true)
		{
		Cursor.SetCursor(cursorGrab, new Vector2(7f,7f),cursorMode);
		}
	}
	
	void OnMouseExit()
	{
		Cursor.SetCursor(null, new Vector2(3f,3f), cursorMode);
	}
	
	public static void tripToggle()
	{
		if(Instance.trip == true)
		{
		Instance.trip = false;
		}
		else if(Instance.trip == false)
		{
		Instance.trip = true;
		}
	}
	
	public static void setFalse()
	{
		Instance.trip = false;
	}
}
