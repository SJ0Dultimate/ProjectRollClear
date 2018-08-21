using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollieSur : RollieSurSuper {

	public float upForce = 200f;

	private bool isDead = false;
	private Rigidbody2D rb2d;

	[SerializeField]
	private float rollinSpeed;

	public AudioSource lvlMusic1;
	public AudioSource lvlMusic2;
	public AudioSource lvlMusic3;
	public AudioSource aBlast;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		rb2d = GetComponent<Rigidbody2D> ();
		StartCoroutine (lvlMusicPlay ());
	}

	IEnumerator lvlMusicPlay (){
		lvlMusic1.Play ();
		yield return new WaitForSeconds (lvlMusic1.clip.length);
		lvlMusic2.Play ();
		yield return new WaitForSeconds (lvlMusic2.clip.length);
		lvlMusic3.Play ();
		yield return new WaitForSeconds (lvlMusic3.clip.length);
	}

	// Update is called once per frame
	void Update () {
		if (isDead == false) {
			if (healthy.CurrentVal > 0){
				if (Input.GetMouseButtonDown (0)) {
					//JumpUp ();
					//healthy.CurrentVal -= 10;
				}	
			}
		}
	}

	public void JumpUp (){
		if (healthy.CurrentVal > 0) {
			healthy.CurrentVal -= 10;
			rb2d.velocity = Vector2.zero;
			rb2d.AddForce (new Vector2 (0, upForce));
		}
	}

	void FixedUpdate (){
		rb2d.velocity = new Vector2 (1 * rollinSpeed, rb2d.velocity.y);
		/*if (Time.time % 7 == 0) {
			healthy.CurrentVal += 25;
		}*/
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Pup"){
			healthy.CurrentVal += 50;
			//other.gameObject.SetActive (false);
		}
		if (other.gameObject.name == "Eicon(Clone)"){
			Destroy (other.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Finish"){
			isDead = true;
			aBlast.Play ();
			Destroy (gameObject);
			GameController.instance.RollieDied ();
			healthy.CurrentVal = healthy.MaxVal;
			PauseMusic ();
		}
		if (other.gameObject.tag == "bTNM"){
			isDead = true;
			aBlast.Play ();
			Destroy (gameObject);
			GameController.instance.RollieDied ();
		}
		rb2d.velocity = Vector2.zero;
		if (other.gameObject.tag == "Ground"){
			healthy.CurrentVal += 20;
		}
		if (other.gameObject.tag == "TMN"){
			healthy.CurrentVal -= 40;
		}

	}

	public void PlayMusic(){
		//lvlMusic.UnPause ();
		lvlMusic1.UnPause();
		lvlMusic2.UnPause();
		lvlMusic3.UnPause();
	}

	public void PauseMusic(){
		//lvlMusic.Pause ();
		lvlMusic1.Pause();
		lvlMusic2.Pause();
		lvlMusic3.Pause();
	}
}
