using UnityEngine;
using System.Collections;
//using System.Windows.Forms;

public class RoomGen : MonoBehaviour 
{
	//public GameObject go;
	public string one = "one";
	public string two = "two";
	public string three = "three";
	public RoomFill scriptRef;
	
	public HouseGen housegen;
	public int housenum;
	public string[,] arrayName;
	public string walls = "Walls";
	
	private int ranNumb;
	private int nextPos;
	private string testName;
	private float BackranNumb;
	private float chance;
	
	IEnumerator Start () 
	{
		//Make FRONT rooms and assign to array
		ranNumb = Random.Range(0,4);
		MakeRoom(ranNumb,3, three);
		arrayName[ranNumb,0] = "LThree";
		arrayName[ranNumb + 1,0] = "MThree";
		arrayName[ranNumb + 2,0] = "RThree";
		
		switch(ranNumb)
		{
			case 0 : 
					int ran = Random.Range(3,5);
					MakeRoom(ran, 2, two);
					arrayName[ran,0] = "LTwo";
					arrayName[ran + 1,0] = "RTwo";
					if(ran == 3)
						{
						MakeRoom (5, 1, one);
						arrayName[5,0] = "One";
						}
					else
						{
						MakeRoom (3, 1, one);
						arrayName[3,0] = "One";
						}
					break;
			case 1 :
					MakeRoom(4, 2, two);
					arrayName[4,0] = "LTwo";
					arrayName[5,0] = "RTwo";
					MakeRoom(0, 1, one);
					arrayName[0,0] = "One";
					break;
			case 2 :
					MakeRoom(0, 2, two);
					arrayName[0,0] = "LTwo";
					arrayName[1,0] = "RTwo";
					MakeRoom(5, 1, one);
					arrayName[5,0] = "One";
					break;
			case 3 :
					int r = Random.Range(0,2);
					MakeRoom(r, 2, two);
					arrayName[r,0] = "LTwo";
					arrayName[r + 1,0] = "RTwo";
					if(r == 0)
						{
						MakeRoom(2, 1, one);
						arrayName[2,0] = "One";
						}
					else
						{
						MakeRoom (0,1, one);
						arrayName[0,0] = "One";
						}
					break;
			default:
					break;
		}
		
		//Make BACK rooms and assign to array
		chance = 1.0f;
		nextPos = 0;
		
		while(nextPos < 5)
		{
			BackranNumb = Random.value * chance;
			
			if(BackranNumb >= 0.5f)
			{
				StartCoroutine(MakeBackRoom(nextPos,1,one));
				arrayName[nextPos,1] = "BackOne";
				nextPos += 1;
				chance -= 0.20f;
			}
			else if(BackranNumb < 0.5f)
			{
				StartCoroutine(MakeBackRoom(nextPos,2,two));
				arrayName[nextPos,1] = "BackLTwo";
				arrayName[nextPos + 1,1] = "BackRTwo";
				nextPos += 2;
			}
		}
		
		if(nextPos == 5)
		{
			StartCoroutine(MakeBackRoom(nextPos, 1, one));
			arrayName[5,1] = "BackOne";
		}
		yield return new WaitForSeconds(0.5f);
		
		//Place a wall piece at the RIGHT END of the house
		//192 in x should hit the right spot just beyond the room tile... maybe subtract to bring it on the tile?
		//GameObject go = Instantiate (Resources.Load(walls, typeof(GameObject)), new Vector3(192,0,0), Quaternion.identity) as GameObject;
		//go.transform.parent = transform;
	}
	
	void Update () 
	{
	
	}
	
	void MakeRoom(int num, int size, string type)
	{
		int pos;
		pos = num * 32;
		
		GameObject room = new GameObject();
		room.transform.parent = transform;
		room.transform.position = new Vector3(pos, room.transform.position.y, room.transform.position.z);
		testName = "room" + type;
		room.name = testName;
		
		room.AddComponent("RoomFill");		//Calls RoomFill to build each tile within the room
		room.GetComponent<RoomFill>().PlaceTile(num, size, type);
		
	}
	
	IEnumerator MakeBackRoom(int num, int size, string type)
	{
		int pos;
		pos = num * 32;
		
		GameObject room = new GameObject();
		room.transform.parent = transform;
		room.transform.position = new Vector3(pos, room.transform.position.y, room.transform.position.z);
		testName = "Backroom" + type;
		room.name = testName;
		
		room.AddComponent("RoomFill");		//Calls RoomFill to build each tile within the room
		room.GetComponent<RoomFill>().PlaceTile(num, size, type);
		
		yield return null;					//So the house will move as a unit
		
		room.transform.position = new Vector3(room.transform.position.x, 32, room.transform.position.z);
		
	}
	
	public void SetUp(int h)
	{
		switch(h)
		{
			case 1 :
				arrayName = HouseGen.rooms1;
				break;
			case 2 :
				arrayName = HouseGen.rooms2;
				break;
			case 3 :
				arrayName = HouseGen.rooms3;
				break;
			case 4 :
				arrayName = HouseGen.rooms4;
				break;
			default :
				break;
		}
		
		GameObject go = Instantiate (Resources.Load(walls, typeof(GameObject)), new Vector3(192,0,0), Quaternion.identity) as GameObject;
		go.transform.parent = transform;
	}
}
