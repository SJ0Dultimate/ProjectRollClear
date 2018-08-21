using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerCoins : MonoBehaviour {

	private static GameManagerCoins instance;

	[SerializeField]
	private GameObject coinPrefb;

	[SerializeField]
	private Text coinCollectTxt;

	[SerializeField]
	private Text coinsTotal;

	private int collectedCoins;

	float minCoinTime = 5;
	float maxcoinTime = 10;
	private float cTime;
	private float coinSpawnTime;


	public int CollectedCoins {
		get { 
			return collectedCoins;
		}
		set { 
			coinCollectTxt.text = value.ToString ();
			collectedCoins = value;
			if (collectedCoins < PlayerPrefs.GetInt ("Sparks") || collectedCoins > PlayerPrefs.GetInt ("Sparks")) {
				int Scoins = PlayerPrefs.GetInt ("Sparks");
				PlayerPrefs.SetInt ("Sparks", Scoins + 1);
			}
			coinsTotal.text = ""+PlayerPrefs.GetInt ("Sparks");
		}
	}

	public GameObject CoinPrefb {
		get { 
			return coinPrefb;
		}
	}

	public static GameManagerCoins Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType <GameManagerCoins> ();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		coinsTotal.text = ""+PlayerPrefs.GetInt ("Sparks");
		coinCollectTxt.text = "0";
		SetRandomT ();
		cTime = minCoinTime;
	}
	
	// Update is called once per frame

	void FixedUpdate () {
		cTime += Time.deltaTime;

		if (cTime >= coinSpawnTime){
			SpawnCoim ();
			SetRandomT ();
		}
	}

	void SetRandomT(){
		coinSpawnTime = Random.Range (minCoinTime, maxcoinTime);
	}

	void SpawnCoim(){
		cTime = minCoinTime;
		Instantiate (CoinPrefb, transform.position, CoinPrefb.transform.rotation);
	}

	public void Destroy(){
		Destroy (instance);
	}
}
