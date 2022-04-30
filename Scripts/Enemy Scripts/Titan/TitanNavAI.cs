using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitanNavAI : MonoBehaviour {

	//Find Player
	Transform PlayerPos;

	//NavMesh Variables
	public NavMeshAgent Titan;

	// Use this for initialization
	void Start () {

		//Assign Target
		PlayerPos = GameObject.Find ("Player").transform;

	}

	// Update is called once per frame
	void Update () {

		//MoveTowards Targert
		Titan.SetDestination(PlayerPos.transform.position);

	}
}
