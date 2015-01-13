using UnityEngine;
using System.Collections;

public class RoomFill : MonoBehaviour {

	public int number;
	private string style;
	public string walls = "Walls";
	private int ranNumb;
	
	void Start () 
	{
		
	}
	

	void Update () 
	{
	
	}
	
	public void PlaceTile(int num, int size, string type)
	{
		int pos;
		ranNumb = Random.Range(0,3);
		
		switch(ranNumb)
		{
			case 0 : 
				style = "Wood";
				break;
			case 1 : 
				style = "Stucco";
				break;
			case 2 : 
				style = "Tile";
				break;
			default :
				break;
		}
		
		string rmType = type + style;
		
		for(int i = 0; i < size; i++)
		{
			pos = num * 32;
			if(i == 0)
			{
				//place wall left of start of room
				GameObject goWall = Instantiate (Resources.Load(walls, typeof(GameObject)), new Vector3(pos,0,0), Quaternion.identity) as GameObject;
				goWall.transform.parent = transform;
				Debug.Log("Placed wall in " + rmType);
			}
			
			//pos = num * 32;
			GameObject go = Instantiate (Resources.Load(rmType, typeof(GameObject)), new Vector3(pos,0,0), Quaternion.identity) as GameObject;
			go.transform.parent = transform;
			num++;
		}
	}
}
