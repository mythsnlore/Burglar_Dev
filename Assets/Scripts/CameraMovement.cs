using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	public float smooth = 1.0f;
	public float offsetCam;
	public float factor = 1.25f;
	//public float camMaxDist = 10f;
	private GameObject player;
	//private float relCameraPos;
	//private float relCameraMag;
	private Vector3 newPos;
	
	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag(Tags.player);
		//relCameraPos = Mathf.Abs(transform.position.x - player.position.x);
		//relCameraMag = relCameraPos.magnitude;
	}
	
	
	void FixedUpdate () 
	{
		offsetCam = player.GetComponent<Rigidbody2D>().velocity.x * factor;
		
		//if(player.GetComponent<PlayerController>().facingLeft == true)
			newPos = new Vector3(player.transform.position.x + offsetCam, player.transform.position.y + 5, transform.position.z);
		//else if(player.GetComponent<PlayerController>().facingLeft == false)
			//newPos = new Vector3(player.transform.position.x + offsetCam, player.transform.position.y + 7.0f, transform.position.z);
		
		transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);

	}
}
