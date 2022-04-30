using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserNavAI : MonoBehaviour {

	//Find Player
	Transform PlayerPos;

	//NavMesh Variables
	public NavMeshAgent Chaser;

	// Use this for initialization
	void Start () {

		//Assign Target
		PlayerPos = GameObject.Find ("Player").transform;

	}

	// Update is called once per frame
	void Update () {

		//MoveTowards Targert
		Chaser.SetDestination(PlayerPos.transform.position);

	}
}
