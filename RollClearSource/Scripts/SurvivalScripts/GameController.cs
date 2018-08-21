using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private RollieSur rollieInst;
	public static GameController instance;
	public GameObject gameOverText;
	public Text scoreText;
	public bool gameOver = false;
	public float scrollSpeed;

	bool pauseB;
	public Button[] buttons;


	public Text bestScore;

	private int score = 0;

	// Use this for initialization
	void Awake () {
		pauseB = false;
		Time.timeScale = 1;
		rollieInst = GameObject.FindGameObjectWithTag ("Player").GetComponent<RollieSur> ();
		scoreText.text = score.ToString ();
		InvokeRepeating ("timeUpdate", 1.0f, 1.0f);
		if (instance == null) {
			instance = this;
		}else if(instance != this){
			Destroy (gameObject);
		}
	}

	//void start (){
	//Time.timeScale = 1;
	//InvokeRepeating ("timeUpdate", 1.0f, 1.0f);
	//}

	void timeUpdate(){
		if (gameOver == false) {
			RollieScored ();		
		} 
	}

	public void pause () {
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			pauseB = true;
			rollieInst.PauseMusic ();
		} else if (Time.timeScale == 0) {
			Time.timeScale = 1;
			pauseB = false;
			rollieInst.PlayMusic ();
		}
		ButtonOn ();
	}

	// Update is called once per frame
	void Update () {
		//if (gameOver == true && Input.GetMouseButtonDown (0)) {
		//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		//}
	}

	public void RollieScored(){
		if (gameOver) {
			return;
		}
		score++;
		if (score > PlayerPrefs.GetInt("BestSur")){
			PlayerPrefs.SetInt ("BestSur", score);
		}
		scoreText.text = score.ToString ();
		bestScore.text = "Best Time : " + PlayerPrefs.GetInt ("BestSur") + " Seconds";
	}

	public void RollieDied(){
		bestScore.gameObject.SetActive (true);
		gameOverText.SetActive (true);
		gameOver = true;
		pauseB = true;
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

}
