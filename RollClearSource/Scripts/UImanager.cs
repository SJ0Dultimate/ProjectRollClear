using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {


	Life lives;
	public Button[] buttons;
	public Text timeText;
	bool pauseB;
	public int portalTime = 0;
	bool gameOver;

	public GameObject loadLvl;
	public GameObject loadSur;
	public GameObject pImage;

	private Player playa;

	public Text lifeLeft;

	string sceneLoaded;

	// Use this for initialization
	void Start () {
		LoadScene ();
	}

	void LoadScene() {
		Scene currentScene = SceneManager.GetActiveScene ();
		sceneLoaded = currentScene.name;
		Time.timeScale = 1;
		gameOver = false;
		pauseB = false;
		if (sceneLoaded != "Select"){
			SetTime ();
			playa = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
			lives = GameObject.FindGameObjectWithTag ("Player").GetComponent<Life> ();
		}
		InvokeRepeating ("TimeOfPortal", 1.0f, 1.0f);
	}

	// Update is called once per frame
	void Update () {
		if (sceneLoaded != "Select"){
			timeText.text = " " + portalTime; 
		}
	}

	void TimeOfPortal(){
		if (gameOver == false) {
			portalTime -= 1;			
			if (portalTime == 0) {
				lives.LifeLost (1);
				SetTime ();
				if (lives.LifeSph == 0){
					pause ();
					gameOverActive ();						
				}
				playa.PlayerDead ();
			}
		} 
	}

	public void SetTime(){
		this.portalTime = 135;
		//InvokeRepeating ("TimeOfPortal", 1.0f, 1.0f);
	}

	private void SceneSurLoad (){
		loadSur.gameObject.SetActive (true);
	}

	private void SceneLoad (){
		loadLvl.gameObject.SetActive (true);
	}

	public void play () {
		SceneManager.LoadScene("Select");
	}

	public void Restart(){
		if (PlayerPrefs.GetInt ("PrefLife") == 0) {
			playa.RestartLevel ();
		} else {
			lives.LifeLost (1);
			playa.RestartLevel ();
		}
	}

	public void SurVive(){
		SceneSurLoad ();
		SceneManager.LoadScene ("Survival");
	}

	public void Select() {
		SceneLoad ();
		SceneManager.LoadScene ("D4");
	}



	public void Select2() {
		SceneLoad ();
		SceneManager.LoadScene ("D2");
	}

	public void Select3() {
		SceneLoad ();
		SceneManager.LoadScene ("ProjectD");
	}

	public void Select4() {
		SceneLoad ();
		SceneManager.LoadScene ("D3");
	}

	public void pause () {
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			pauseB = true;
			playa.PauseMusic ();
			pImage.gameObject.SetActive (true);
		} else if (Time.timeScale == 0) {
			Time.timeScale = 1;
			pauseB = false;
			playa.PlayMusic ();
			pImage.gameObject.SetActive (false);
		}
		ButtonOn ();
	}

	private void ButtonOn(){
		if (pauseB == true) {
			foreach (Button button in buttons) {
				button.gameObject.SetActive (true);
			}
		} else if (pauseB == false){
			foreach (Button button in buttons) {
				button.gameObject.SetActive (false);

			}
		}
	}

	public void menu(){
		SceneManager.LoadScene ("Select");
	}



	public void exit(){
		Application.Quit();
	}

	public void gameOverActive(){
		gameOver = true;
		foreach (Button button in buttons) {
			button.gameObject.SetActive(true);
		}
	}
}