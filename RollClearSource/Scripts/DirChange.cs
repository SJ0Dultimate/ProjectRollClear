using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirChange : MonoBehaviour {

	public Animator myAnimPunch{ get; set;}

	// Use this for initialization
	void Start () {
		myAnimPunch = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player"){
			myAnimPunch.SetTrigger("turn");
		}
	}

}