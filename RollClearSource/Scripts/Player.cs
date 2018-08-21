using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public GameObject portal;
	public GameObject portalMsg;

	public GameObject cpLights;
	public GameObject beacUIimg;

	Life lives;
	float currentTime;

	public Text meterLable;
	public Text bestyDisty;
	public Text bDistMeter;
	public Text bBeacons;
	int beaconCount;

	private static Player instance;

	public static Player Instance {
		get{
			if (instance == null){
				instance = GameObject.FindObjectOfType<Player> ();
			}
			return instance;
		}
	}

	public Rigidbody2D myRigid { get; set;}

	private AudioSource audios;
	private AudioSource audioBlst;
	private AudioSource beacFired;

	private Animator myAnim;

	public bool PowerUp { get; set;}

	[SerializeField]
	private Text winTxt;

	//[SerializeField]
	private float movementSpeed;

	[SerializeField]
	public float jumpForce;

	private Vector2 startPos;

	private int dir;

	public UImanager uim;
	public DirChange dchange;

	public Sprite portOn;

	int disty;

	string sceneLoaded;

	float startTime;

	// Use this for initialization
	void Start () {
		InitializeGame ();
		CheckLvl ();
	}

	void InitializeGame (){
		startTime = Time.time;
		Scene currentScene = SceneManager.GetActiveScene ();
		sceneLoaded = currentScene.name;
		beaconCount = 10;
		bBeacons.text = beaconCount.ToString();
		dir = 1;
		PowerUp = false;
		startPos = transform.position;
		myRigid = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		lives = GetComponent<Life> ();
		uim = GetComponent<UImanager> ();
		audios = GetComponent<AudioSource> ();
		audioBlst = GameObject.FindGameObjectWithTag("Respawn").GetComponent<AudioSource> ();
		beacFired = GameObject.FindGameObjectWithTag ("checkPoint").GetComponent<AudioSource> ();
		PlayerPrefs.SetInt ("Grav", 0);
	}

	void CheckLvl (){
		if (sceneLoaded == "D4"){
			movementSpeed = 5f;
		}
		if (sceneLoaded == "D2"){
			movementSpeed = 5.7f;
		}
		if (sceneLoaded == "ProjectD"){
			movementSpeed = 6.3f;
		}
		if (sceneLoaded == "D3"){
			movementSpeed = 7.1f;
		}
	}

	// Update is called once per frame
	void Update () {
		HandleInput ();
		if (beaconCount == 0 || beaconCount < 0){			
			SetPortalOn ();
		}
		CalcDist ();
	}

	void SetPortalOn(){
		portalMsg.gameObject.SetActive (true);
		portal.gameObject.SetActive (true);
		bBeacons.text = "0";
		beacUIimg.gameObject.GetComponent<Image> ().sprite = portOn;
		//uim.SetTime ();
	}

	private void CalcDist (){		
		//Vector2 posRoll = transform.position;
		if (myRigid.velocity.x != 0) { //posRoll != posRoll
			currentTime = Time.time;	
			float meters = (currentTime - startTime) * 2;
			disty = Mathf.RoundToInt (meters);
			meterLable.text = "Distance: " + disty + " m";
			if (sceneLoaded == "D4"){
				if (disty > PlayerPrefs.GetInt ("Best1")) {
					PlayerPrefs.SetInt ("Best1", disty);
				}
				bDistMeter.text = "Best: " + PlayerPrefs.GetInt ("Best1") + "m";
				bestyDisty.text = "Best Distance : " + PlayerPrefs.GetInt ("Best1") + "m";
			}
			if (sceneLoaded == "D2"){
				if (disty > PlayerPrefs.GetInt ("Best2")) {
					PlayerPrefs.SetInt ("Best2", disty);
				}
				bDistMeter.text = "Best: " + PlayerPrefs.GetInt ("Best2") + "m";
				bestyDisty.text = "Best Distance : " + PlayerPrefs.GetInt ("Best2") + "m";
			}
			if (sceneLoaded == "ProjectD"){
				if (disty > PlayerPrefs.GetInt ("Best3")) {
					PlayerPrefs.SetInt ("Best3", disty);
				}
				bDistMeter.text = "Best: " + PlayerPrefs.GetInt ("Best3") + "m";
				bestyDisty.text = "Best Distance : " + PlayerPrefs.GetInt ("Best3") + "m";
			}
			if (sceneLoaded == "D3"){
				if (disty > PlayerPrefs.GetInt ("Best4")) {
					PlayerPrefs.SetInt ("Best4", disty);
				}
				bDistMeter.text = "Best: " + PlayerPrefs.GetInt ("Best4") + "m";
				bestyDisty.text = "Best Distance : " + PlayerPrefs.GetInt ("Best4") + "m";
			}
		}
	}

	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			HandleMovementUp();
		}else if (Input.GetMouseButtonDown(0)){
			//HandleMovementUp();			
		}else if (Input.GetKeyDown (KeyCode.LeftArrow)){
			DirLeft ();
		}else if (Input.GetKeyDown (KeyCode.RightArrow)){
			DirRight ();
		}
	}

	public void DirLeft(){
		dir = -1;
	}

	public void DirRight(){
		dir = 1;
	}

	void FixedUpdate(){
		myRigid.velocity = new Vector2 (dir * movementSpeed, myRigid.velocity.y);
	}

	private void HandleMovementSide(float horizontal){
		myRigid.velocity = new Vector2 (horizontal * movementSpeed, myRigid .velocity.y);
	}

	public void HandleMovementUp(){
		myRigid.AddForce (new Vector2(0, jumpForce));
	}

	IEnumerator ResetText(){
		yield return new WaitForSeconds (1f);
		winTxt.text = "";
	}

	IEnumerator MoveStart(){
		yield return new WaitForSeconds (0f);
		transform.position = startPos;
		yield return new WaitForSeconds (0.2f);
		winTxt.text = "T R Y   A G A I N!";
		audios.Play ();
		dir = 1;
		startTime = Time.time;
		StartCoroutine (ResetText ());
	}

	public void PlayMusic(){
		audios.UnPause ();
	}

	public void PauseMusic (){
		audios.Pause ();	
	}

	IEnumerator NextLvl(){
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
		winTxt.text = "";
		if (sceneLoaded == "D4"){
			UnlockLevels.isLvl2Unlocked = true;
			PlayerPrefs.SetInt ("lvlLock1", 1);
		}
		if (sceneLoaded == "D2"){
			UnlockLevels.isLvl3Unlocked = true;
			PlayerPrefs.SetInt ("lvlLock2", 1);
		}
		if (sceneLoaded == "ProjectD"){
			UnlockLevels.isLvl4Unlocked = true;
			PlayerPrefs.SetInt ("lvlLock3", 1);
		}
		if (sceneLoaded == "D3"){
			UnlockLevels.isLvl5Unlocked = true;

			PlayerPrefs.SetInt ("lvlLock4", 1);
		}
		SceneManager.LoadScene ("Select");
	}



	public void PlayerDead (){
		myAnim.SetTrigger ("dest");
		StartCoroutine (MoveStart());
		lives.LifeLost (1);
		audioBlst.Play ();
	}

	private void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "TMN"){			
			PlayerDead ();
		}
		if (other.gameObject.tag == "DirTag") {
			dir = -1;
		}
		if (other.gameObject.tag == "DirTagR") {
			dir = 1;
		}
		if (other.gameObject.tag == "Finish"){
			myRigid.gravityScale = 0;
			dir = 0;
			winTxt.text = "ROLL CLEAR";
			myAnim.SetTrigger ("win");
			StartCoroutine (NextLvl());

		}
		if (other.gameObject.tag == "checkPoint"){
			beacFired.Play ();
			CheckLightCount(1);
		}
		if (other.gameObject.tag == "Coin"){
			Destroy (other.gameObject);
			GameManagerCoins.Instance.CollectedCoins++;

		}
		if (other.gameObject.tag == "gravti"){
			if (PlayerPrefs.GetInt ("Grav") == 0){
				myRigid.gravityScale = 0.8f;
				jumpForce = (jumpForce * (-1)) + 50;					
			}
			dir = -1;
			PlayerPrefs.SetInt ("Grav", 1);
		}
	}

	public void CheckLightCount(int minusPoint){
		beaconCount = beaconCount - minusPoint;
		bBeacons.text = beaconCount.ToString ();
	}

	public void Dead(){
		movementSpeed = 0;
		myRigid.gravityScale = 0;
		jumpForce = 0;
	}

	public void RestartLevel(){
		SceneManager.LoadScene (sceneLoaded);
	}
}