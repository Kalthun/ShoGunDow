using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBullet : MonoBehaviour {

	//Player
	GameObject PlayerGO;
	Vector3 TargetPos;

	// Use this for initialization
	void Start () {

		//Assign
		PlayerGO = GameObject.Find ("Player");
		TargetPos = PlayerGO.transform.position;

		//KillTime
		Invoke ("DefaultKill", 6);

	}

	// Update is called once per frame
	void Update () {

		//MoveTowards Players Location (Old)
		transform.position = Vector3.MoveTowards (transform.position, new Vector3 (TargetPos.x, 3.5f, TargetPos.z), 10 * Time.deltaTime);

		//Destroy if arrived
		if (transform.position ==  new Vector3 (TargetPos.x, 3.5f, TargetPos.z)) {
			Destroy (gameObject);
		}

	}

	void DefaultKill () {
		Destroy (gameObject);
	}

}