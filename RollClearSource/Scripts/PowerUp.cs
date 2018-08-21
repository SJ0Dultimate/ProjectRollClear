using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UseP ()
	{
		if (Player.Instance.PowerUp) {
			UsePower (false, 1);
		} else {
			UsePower (true, 0);
		}
	}

	private void UsePower(bool power, int gravity){
		Player.Instance.PowerUp = power;
		Player.Instance.myRigid.gravityScale = gravity;
	}
}
