using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour {

	public GameObject pauseBtn;

	public GameObject gameOverScn;

	public GameObject convertLifeMsg;

	Player player;

	public Text lifeTxt;

	private int lifeSph;
	private int lifeCountPref;

	public int LifeSph {
		get { 
			return lifeSph;
		}
		set { 
			
		}
	}

	private UImanager uiM;

	// Use this for initialization
	void Start () {
		SetLife ();
	}

	void SetLife(){
		lifeSph = PlayerPrefs.GetInt ("PrefLife");
		lifeCountPref = lifeSph;
		if (PlayerPrefs.GetInt ("PrefLife") == 0){
			lifeSph = 0;
			InvokeRepeating ("convertSparksLife", 0.25f, 1.5f);
		}else{
			convertLifeMsg.gameObject.SetActive(false);
		}
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		uiM = GameObject.Find("uiManager").GetComponent<UImanager>();
		SetCountText ();	
	}

	// Update is called once per frame
	void Update () {
		
	}

	void convertSparksLife (){
		StartCoroutine (BlinkMsg());
	}

	IEnumerator BlinkMsg(){
		yield return new WaitForSeconds (0.5f);
		convertLifeMsg.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		convertLifeMsg.gameObject.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		convertLifeMsg.gameObject.SetActive (true);
	}

	IEnumerator LevelSelector (){
		yield return new WaitForSeconds (3f);
		uiM.pause();
	}

	public void LifeLost (int lifeminus){
		if (LifeSph == 0){
			player.Dead ();
			pauseBtn.gameObject.SetActive (false);
			gameOverScn.gameObject.SetActive (true);
			StartCoroutine (LevelSelector ());
		}
		lifeSph -= lifeminus;
		PlayerPrefs.SetInt ("PrefLife", lifeCountPref - lifeminus);
		uiM.SetTime ();
		SetCountText ();
	}

	void SetCountText (){
		if (LifeSph < 0) {
			lifeSph = 0;
			PlayerPrefs.SetInt ("PrefLife", lifeSph);
			lifeTxt.text = "0";
		} else {
			lifeTxt.text = LifeSph.ToString ();
		}
	}

}
