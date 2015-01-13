using UnityEngine;
using System.Collections;

public class TripPlayer : MonoBehaviour 
{

	public GameObject player;
	public PlayerController playerController;
	public DisableControls disableControls;
	public float randNum;
	
	public Texture2D cursorGrab;
	CursorMode cursorMode = CursorMode.Auto;
	
	
	void OnTriggerEnter2D (Collider2D player)
	{
		randNum = Random.value;
		
		if(randNum >= 0.5f && playerController.maxSpeed == playerController.runSpeed)
		{
			PlayerController.tripMe();
			DisableControls.toggleControl();
		}
		else
		{
		
		}
	}
	
	void OnMouseEnter()
	{
		Cursor.SetCursor(cursorGrab, new Vector2(7f,7f),cursorMode);
	}
	
	void OnMouseExit()
	{
		Cursor.SetCursor(null, new Vector2(3f,3f), cursorMode);
	}
}
