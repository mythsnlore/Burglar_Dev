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
	
	void Update()
	{
		//hingeAngle = myRigidbody.GetComponent<HingeJoint2D>().jointAngle;
		
		//if(hingeAngle != 0)
			//hingeAngle = Mathf.Lerp(hingeAngle, 0, 1);
			
		//if(myRigidbody.rotation >= 10)
			
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) 
	{
		speed = other.rigidbody2D.velocity.x * factor;
		myRigidbody.AddTorque(speed);
	}
}
