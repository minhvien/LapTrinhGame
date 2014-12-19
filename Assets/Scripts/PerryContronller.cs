using UnityEngine;
using System.Collections;

public class PerryContronller : MonoBehaviour {
	public float jetpackForce = 75.0f;
	public float forwardMovementSpeed;

	public Transform groundCheckTransform;
	
	private bool grounded;
	
	public LayerMask groundCheckLayerMask;
	
	Animator animator;

	public ParticleSystem jetpack;

	// dead
	private bool dead = false;
	public ScrollCamera parallax;
	//COUNT COINS
	private uint perry_coin=0;
	public Texture2D coinIconTexture;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	int i=0;
	int speed=3;
	// Update is called once per frame
	void Update () {
		/*i += 10;
		if (i <= 5000)
		{
			forwardMovementSpeed =4;
		}
		else if (i <= 8000)
		{
			forwardMovementSpeed =5;
		}
		else if (i <= 9500)
		{
			forwardMovementSpeed =6;
		}else if (i <= 10000)
		{
			forwardMovementSpeed =7;
		}
		else if (i <= 15000)
		{
			forwardMovementSpeed =8;
		}
		else if (i <= 20000)
		{
			forwardMovementSpeed =9;
		}else if (i <= 30000)
		{
			forwardMovementSpeed =12;
		}*/

		i += 1;
		if (i % 1500 == 0 && forwardMovementSpeed < 20 )
				forwardMovementSpeed += 1;



	}

	void FixedUpdate () 
	{

		bool jetpackActive = Input.GetButton ("Fire1");
		jetpackActive = jetpackActive && !dead;
		if (jetpackActive) {
				rigidbody2D.AddForce (new Vector2 (0, jetpackForce));
		}
		if (!dead) {
				Vector2 newVelocity = rigidbody2D.velocity;
				newVelocity.x = forwardMovementSpeed;
				rigidbody2D.velocity = newVelocity;
		}


		UpdateGroundedStatus();
		AdjustJetpack(jetpackActive);
		// dead bacgroud stop
		parallax.offset = transform.position.x;
	}
	void UpdateGroundedStatus()
	{
		//1
		grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
		
		//2
		animator.SetBool("grounded", grounded);
	}
	void AdjustJetpack (bool jetpackActive)
	{
		jetpack.enableEmission = !grounded;
		jetpack.emissionRate = jetpackActive ? 300.0f : 75.0f; 
	}

	// dead
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("coin"))
			CollectCoin(collider);
		else
			HitByLaser(collider);
	}
	
	void HitByLaser(Collider2D laserCollider)
	{
		dead = true;
		animator.SetBool("isDead", true);
	}
	void DisplayRestartButton()
	{
		if (dead && grounded)
		{
			Rect buttonRect = new Rect(Screen.width * 0.35f, Screen.height * 0.45f, Screen.width * 0.30f, Screen.height * 0.1f);
			if (GUI.Button(buttonRect, "Tap to restart!"))
			{
				Application.LoadLevel (Application.loadedLevelName);
			};
		}
	}
	void DisplayCoinsCount()
	{
		Rect coinIconRect = new Rect(10, 10, 60, 60);
		GUI.DrawTexture(coinIconRect, coinIconTexture);                         
		
		GUIStyle style = new GUIStyle();
		style.fontSize = 48;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.black;
		
		Rect labelRect = new Rect(coinIconRect.xMax, coinIconRect.y, 60, 60);
		GUI.Label(labelRect, perry_coin.ToString(), style);
	}
	void OnGUI()
	{
		DisplayCoinsCount();
	}

	void CollectCoin(Collider2D coinCollider)
	{
		perry_coin++;
		
		Destroy(coinCollider.gameObject);
	}

}
