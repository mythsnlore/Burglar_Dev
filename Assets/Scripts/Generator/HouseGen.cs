using UnityEngine;
using System.Collections;

public class HouseGen : MonoBehaviour 
{
	static int houseNumber = 0;		//individual house number
	public int houseCount = 4;		//number of houses to make
	private string houseName;
	private int move;				//remove after testing
	
	public RoomGen tempRoomGen;
	
	public static string[,] rooms1 = new string[6,2];
	public static string[,] rooms2 = new string[6,2];
	public static string[,] rooms3 = new string[6,2];
	public static string[,] rooms4 = new string[6,2];
	
	IEnumerator Start () 
	{
		
		for(int i = houseNumber; i < houseCount; i++)
		{
			StartCoroutine(CreateHouse());
			yield return new WaitForSeconds(0.5f);
		}
		
		foreach (string i in rooms2)
		{
			Debug.Log(i);
		}
	}

	void Update () 
	{
		
	}
	
	IEnumerator CreateHouse()
	{
		GameObject house = new GameObject();
		houseNumber++;
		houseName = "house" + houseNumber;
		house.name = houseName;
		
		house.AddComponent("RoomGen");		//Calls RoomGen to build each room within the house
		tempRoomGen = house.GetComponent<RoomGen>();
		tempRoomGen.SetUp(houseNumber);
		yield return null;					//So the house will move as a unit

		move = (houseNumber -1) * 256;
		house.transform.position = new Vector3(move, house.transform.position.y, 10);
	}
	
}
