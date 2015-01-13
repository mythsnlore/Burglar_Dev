using UnityEngine;
using System.Collections;

public class Platform1way : MonoBehaviour 
{

	public bool solid = true;
	EdgeCollider2D topSurface;
	//public BoxCollider2D playerCollider;
	
	void Awake()
	{
		topSurface = GetComponent<EdgeCollider2D>();
		//playerCollider = 
	}
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.rigidbody2D.position.y < transform.position.y)
		{
			topSurface.enabled = false;
			solid = false;
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		topSurface.enabled = true;
		solid = true;
	}
}
