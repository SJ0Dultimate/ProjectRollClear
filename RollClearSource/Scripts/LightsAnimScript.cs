using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightsAnimScript : MonoBehaviour {

	private Animator lightAnim;

	// Use this for initialization
	void Start () {
		lightAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ChangeTag(){
		yield return new WaitForSeconds (0.15f);
		gameObject.tag = "hitLight";
	}

	public void OnCollisionEnter2D (Collision2D other){
		if (other.gameObject.tag == "Player"){
			lightAnim.SetBool ("trigSig",true);
			StartCoroutine (ChangeTag());
		}
	}
}
