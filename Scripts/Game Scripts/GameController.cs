using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	//Variables
	public bool SpawnPillars, StartGame, SpawnEnemies, SpawnAmmo;
	GameObject PillarGeneration;
	public bool Switch1, Switch2, Switch3;

	// Use this for initialization
	void Start () {

		//Assign
		PillarGeneration = GameObject.Find ("Pillar Generation");

		//Assign Bools
		SpawnPillars = true;
		StartGame = false;
		SpawnEnemies = false;
		SpawnAmmo = false;
		Switch1 = true;
		Switch2 = true;
		Switch3 = true;

	}
	
	// Update is called once per frame
	void Update () {

		//Spawn Enemies
		if (PillarGeneration.GetComponent<PillarGeneration> ().DonePillarSpawning) {
			if (Switch1) {
				SpawnEnemies = true;
				Switch1 = false;
			}
		}

		//Spawn Ammo Packs
		if (PillarGeneration.GetComponent<PillarGeneration> ().DonePillarSpawning) {
			if (Switch2) {
				SpawnAmmo = true;
				StartGame = true;
				Switch2 = false;
			}
		}
			
	}
}
