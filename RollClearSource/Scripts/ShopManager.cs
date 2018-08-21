using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	public GameObject shopImg;
	public GameObject noScoins;
	public GameObject convertMsg;

	public Text scoinsTxtMain;
	public Text lifyTxtMain;
	public Text scoinsTxt;
	public Text lifyTxt;
	public Text noPurchaseTxt;
	public int scoin;
	int life;

	// Use this for initialization
	void Start () {
		scoin = PlayerPrefs.GetInt ("Sparks");
		life = PlayerPrefs.GetInt ("PrefLife");

	}
	
	// Update is called once per frame // Fixed
	void FixedUpdate () {
		scoinsTxt.text = scoin.ToString();
		lifyTxt.text = life.ToString();
		scoinsTxtMain.text = scoin.ToString();
		lifyTxtMain.text = life.ToString();
		if (PlayerPrefs.GetInt ("Sparks") >= 10) {
			StartCoroutine (ConvertRoller());
		}else if (PlayerPrefs.GetInt ("Sparks") < 10){
			convertMsg.gameObject.SetActive (false);
		}
	}

	IEnumerator ConvertRoller(){ 
			convertMsg.gameObject.SetActive (true);
			yield return new WaitForSeconds (0.5f);
			convertMsg.gameObject.SetActive (false);
			yield return new WaitForSeconds (0.5f);
			convertMsg.gameObject.SetActive (true);
			yield return new WaitForSeconds (0.5f);
	}

	public void Shop(){
		shopImg.gameObject.SetActive (true);
	}

	public void ConvertSparks(){
		noPurchaseTxt.text = "Collect More SPARKS!";
		if (PlayerPrefs.GetInt ("Sparks") >= 10) {
			PlayerPrefs.SetInt ("PrefLife", life + 1);
			PlayerPrefs.SetInt ("Sparks", scoin - 10);
		} else {
			noScoins.gameObject.SetActive (true);

		}
		life = PlayerPrefs.GetInt ("PrefLife");
		scoin = PlayerPrefs.GetInt ("Sparks");
		lifyTxt.text = life.ToString ();
		scoinsTxt.text = scoin.ToString ();	
		scoinsTxtMain.text = scoin.ToString();
		lifyTxtMain.text = life.ToString();
	}

	public void PurchaseBtn (){
		//noScoins.gameObject.SetActive (true);
		noPurchaseTxt.text = "PURCHASE SPARKS!";
		PlayerPrefs.SetInt ("Sparks", scoin + 50);
		scoin = PlayerPrefs.GetInt ("Sparks");
		scoinsTxt.text = scoin.ToString ();	
		scoinsTxtMain.text = scoin.ToString();
	}

	public void RewardBtn (){
		StartCoroutine (RewardSparks ());
	}

	IEnumerator RewardSparks (){
		yield return new WaitForSeconds (1f);
		PlayerPrefs.SetInt ("Sparks", scoin + 5);
		scoin = PlayerPrefs.GetInt ("Sparks");
		scoinsTxt.text = scoin.ToString ();	
		scoinsTxtMain.text = scoin.ToString();
	}

	public void GoBack(){
		shopImg.gameObject.SetActive (false);
	}
}
