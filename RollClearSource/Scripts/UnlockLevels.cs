using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevels : MonoBehaviour {

	public GameObject[] goLock;

	public GameObject helpPage;

	public static bool isLvl2Unlocked = false;
	public static bool isLvl3Unlocked = false;
	public static bool isLvl4Unlocked = false;
	public static bool isLvl5Unlocked = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckUnlocks ();
	}

	private void CheckUnlocks (){
		for (int i = 0; i <= goLock.Length; i++){
			if (isLvl2Unlocked == true || PlayerPrefs.GetInt ("lvlLock1") == 1) {
				goLock [0].SetActive (false);
			}
			if (isLvl3Unlocked == true || PlayerPrefs.GetInt ("lvlLock2") == 1) {
				goLock [1].SetActive (false);
			}
			if (isLvl4Unlocked == true || PlayerPrefs.GetInt ("lvlLock3") == 1) {
				goLock [2].SetActive (false);
			}
			if (isLvl5Unlocked == true || PlayerPrefs.GetInt ("lvlLock4") == 1) {
				goLock [3].SetActive (false);
			}	
		}
	}

	public void help(){
		helpPage.gameObject.SetActive (true);
	}

	public void hBack(){
		helpPage.gameObject.SetActive (false);
	}
}
