using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class example : MonoBehaviour {

	public static example instence;

	void Awack()
	{
		if (instence == null) {
			instence = this;
		} else {
			DestroyImmediate (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
