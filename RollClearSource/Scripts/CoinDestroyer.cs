using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDestroyer : MonoBehaviour {

	private void Awake () {
	
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Coin"){
			Destroy (other.gameObject);
		}
		Destroy (other.gameObject);
	}
}
