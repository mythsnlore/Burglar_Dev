using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	
	public bool facingLeft = true;
	bool grounded = false;
	bool canJump = false;
	bool cantStand = false;
	float groundRadius = 0.6f;
	float headRadius = 0.4f;
	
	public static PlayerController Instance;
	
	Animator anim;
	BoxCollider2D playerCollider;
	
	public float hInputRaw;
	
	public float accelSpeed = 1f;	//how fast the character changes direction horizontally
	public float maxSpeed = 8f;		//the current speed limit, rewritten by walking, running, crawling, etc
	public float walkSpeed = 8f;	//walk speed limit
	public float runSpeed = 15f;	//run speed limit
	public float crawlSpeed = 6f;	//crawl speed limit
	
	public Transform groundCheck;
	public Transform headCheck;
	public Transform blockedCheck;
	public LayerMask whatIsGround;
	public LayerMask whatBlocksSidle;
	
	public float jumpForce = 300f;
	public float jumpMax = 16f;
	
	public bool crawling = false;
	public bool tripping = false;
	public bool sidle = false;
	public bool canSidle = false;
	public float sidlePosition = 2;
	public float walkPosition = 0;
	public Vector3 playerDepth;
	
	void Start () 
	{
		anim = GetComponent<Animator>();
		playerCollider = GetComponent<BoxCollider2D>();
		playerDepth = gameObject.transform.position;
		Instance = (PlayerController)GameObject.FindObjectOfType(typeof(PlayerController)); 
	}
	
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		cantStand = Physics2D.OverlapCircle(headCheck.position, headRadius, whatIsGround);
		canSidle = !Physics2D.OverlapCircle(blockedCheck.position, groundRadius, whatBlocksSidle);
		anim.SetBool("Ground", grounded);
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
		anim.SetFloat("hSpeed", Mathf.Abs(rigidbody2D.velocity.x));
		
		float move = Input.GetAxis ("Horizontal");
		hInputRaw = move;
		
		if(tripping == false)
		{
			if(grounded) //&& rigidbody2D.velocity.y <= 0)
			{
				canJump = true;
			}
			else
			{
				canJump = false;
			}
			
			// Checking for opposite movement keys and eliminating motion if both or none are pressed
			if((Input.GetKey ("a")) ^ (Input.GetKey ("d")))
			{
				if(move > 0)
					rigidbody2D.AddForce(new Vector2(accelSpeed, 0));
				else if (move < 0)
					rigidbody2D.AddForce(new Vector2(accelSpeed * -1, 0));
			}
			else //Slow to a stop if neither or both direction keys are held
			{
				rigidbody2D.AddForce(new Vector2((rigidbody2D.velocity.x * -0.25f), 0f));
				anim.SetFloat("hSpeed", 0);
			}
			
			// Flip the character when facing left or right
			if(move < 0 && !facingLeft)
			{
				if(Mathf.Abs(rigidbody2D.velocity.x) >= runSpeed * 0.6f)
					Skid();
				else
				{
					Flip();
					anim.ResetTrigger("Skid");
				}
			}
			else if(move > 0 && facingLeft)
			{
				if(Mathf.Abs(rigidbody2D.velocity.x) >= runSpeed * 0.6f)
					Skid();
				else
				{
					Flip();
					anim.ResetTrigger("Skid");
				}
			}
			else
				anim.ResetTrigger("Skid");
			
			if(!grounded)
				anim.ResetTrigger("Skid");
				
		}
		
		//Keep the jump under control
		if(rigidbody2D.velocity.y > jumpMax)
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpMax);
			
		//Caps the movement speed both left and right
		if(rigidbody2D.velocity.x > maxSpeed)
			rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
		else if (rigidbody2D.velocity.x < (maxSpeed * -1))
			rigidbody2D.velocity = new Vector2((maxSpeed * -1), rigidbody2D.velocity.y);
	}
	
	void Update()
	{
		maxSpeed = walkSpeed;
		
		if(tripping == false)
		{
			if(canJump && !cantStand && Input.GetButton("Jump"))
			{
				anim.SetBool("Ground", false);
				rigidbody2D.AddForce(new Vector2(0, jumpForce));
			}
			
			if(Input.GetButton("Run") && !crawling && !sidle)
			{
				maxSpeed = runSpeed;
				anim.SetBool("Running", true);
			}
			else
			{
				//hSpeed = walkSpeed;
				anim.SetBool("Running", false);
			}
			
			if(grounded && Input.GetButton ("Crawl"))
			{
				anim.SetBool ("Crawl", true);
				playerCollider.center = (new Vector2(-0f,0.63f));
				playerCollider.size = (new Vector2(2.75f,1.25f));
				maxSpeed = crawlSpeed;
				crawling = true;
			}
			else if(!cantStand)
			{
				anim.SetBool ("Crawl", false);
				playerCollider.center = (new Vector2(-0.25f, 1.63f));
				playerCollider.size = (new Vector2(1.25f,3.25f));
				crawling = false;
			}
			
			if(Input.GetButton ("Sidle") && canSidle)
			{
				maxSpeed = crawlSpeed;
				sidle = true;
			}
			else if(!Input.GetButton ("Sidle") && canSidle)
			{
				sidle = false;
			}
			
			if(sidle)
			{
				playerDepth = new Vector3 (rigidbody2D.position.x, rigidbody2D.position.y ,sidlePosition);
				anim.SetBool("Sidle", true);
			}
			else if(!sidle)
			{
				playerDepth = new Vector3 (rigidbody2D.position.x, rigidbody2D.position.y ,walkPosition);
				anim.SetBool("Sidle", false);
			}
			
		gameObject.transform.position = playerDepth;
		}
	}
	
	void Flip()
	{
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	void Skid()
	{
		anim.SetTrigger ("Skid");
		Flip();
	}
	
	public static void tripMe() //sets anim state for trip right before being disabled
	{
		Instance.anim.SetTrigger ("Trip");
		Instance.tripping = true;
		TripRecover.tripToggle();
		Instance.anim.ResetTrigger("GetUp");
	}
	
	public static void tripSave()
	{
		Instance.anim.ResetTrigger ("Trip");
		Instance.anim.SetTrigger ("TripSave");
		Instance.tripping = false;
		TripRecover.tripToggle();
		DisableControls.tripCancel();
	}
	
	public static void getUp()
	{
		Instance.anim.SetTrigger ("GetUp");
		Instance.tripping = false;
		TripRecover.tripToggle();
		DisableControls.tripCancel();
	}
}
