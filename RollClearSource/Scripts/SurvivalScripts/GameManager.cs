using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject load;

	public void Play(){
		load.gameObject.SetActive (true);
		SceneManager.LoadScene ("Survival");
	}

	public void Menu(){
		load.gameObject.SetActive (true);
		SceneManager.LoadScene ("Select");
	}

	public void Exit(){
		Application.Quit();
	}
}
