using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
	public float theTime;
	public int minute;
	public static int second;
	
	// Use this for initialization
	void Start () 
	{
		theTime = 0.0f;
		minute = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		theTime = Time.time;
		second = (int) theTime;
		minute = second / 60;
	}
}
