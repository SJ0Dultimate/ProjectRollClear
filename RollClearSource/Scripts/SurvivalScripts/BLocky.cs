using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLocky : MonoBehaviour {

	void Start(){
		gameObject.SetActive (true);
	}

	private void OnCollissionEnter2D (Collision2D other){
		if (other.gameObject.tag == "bTNM"){
			Destroy (gameObject);
		}
	}
}
