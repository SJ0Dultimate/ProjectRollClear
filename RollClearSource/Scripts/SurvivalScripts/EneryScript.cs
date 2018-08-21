using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneryScript : MonoBehaviour {

	private static EneryScript instance;

	public GameObject energyPrefb;

	private int collectedCoins;

	private float cTime;
	private float engySpawnTime;

	Vector2 posE;

	public GameObject EnergyPrefb {
		get { 
			return energyPrefb;
		}
	}

	public static EneryScript Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType <EneryScript> ();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		SetRandomT ();
		cTime = 7f;
		posE.x = 30;
		posE.y = 2;
	}

	// Update is called once per frame

	void FixedUpdate () {
		cTime += Time.deltaTime;
		if (cTime >= engySpawnTime){
			SpawnEnergy ();
			SetRandomT ();
		}
	}

	void SetRandomT(){
		engySpawnTime = Random.Range (7f, 18f);
	}

	void SpawnEnergy(){
		cTime = 7f;

		Instantiate (EnergyPrefb, posE, EnergyPrefb.transform.rotation);
	}

	public void Destroy(){
		Destroy (instance);
	}
}
