using UnityEngine;
using System.Collections;

public class SkyAnimator : MonoBehaviour 
{
	Animator anim;
	float skyTime;
	
	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	
	
	void FixedUpdate () 
	{
		skyTime = Timer.second * 10.00f;
		
		anim.SetFloat("SkyState", skyTime);
	}
}
