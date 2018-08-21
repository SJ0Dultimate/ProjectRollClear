using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollieSurSuper : MonoBehaviour {

	[SerializeField]
	protected StatSur healthy;

	// Use this for initialization
	public virtual void Start () {
		healthy.Initialize ();
	}

	// Update is called once per frame
	void Update () {

	}
}
