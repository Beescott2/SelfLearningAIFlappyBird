using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
