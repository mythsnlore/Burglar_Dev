using UnityEngine;
using System.Collections;

public class FurnitureTip : MonoBehaviour 
{
	public float speed;
	public float factor = 10.0f;
	public float hingeAngle;
	Rigidbody2D myRigidbody;
	//HingeJoint2D myHinge;
	// Use this for initialization
	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		//myHinge = GetComponent<HingeJoint2D>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) 
	{
		speed = other.rigidbody2D.velocity.x * factor;
		myRigidbody.AddTorque(speed);
		
		if(Mathf.Abs(speed) >= 7 * factor)
		MakeSound();
	}
	
	void MakeSound()
	{
		Debug.Log("Sound Produced");
	}
}
