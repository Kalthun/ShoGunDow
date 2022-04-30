using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHealth : MonoBehaviour {

	//Variables
	GameObject EnemySpawning, HealthPacks, Player;
	public float SoldierHealthTotal = 9f;

	//Start
	void Start () {

		//Assign
		Player = GameObject.Find ("Player");
		EnemySpawning = GameObject.Find ("Enemy Spawning");
		HealthPacks = GameObject.Find ("Health Packs");

	}

	// Update is called once per frame
	void Update () {

		if (SoldierHealthTotal <= 0f) {
			Die ();
		}
	}

	void Die () {
		Destroy (gameObject);
		Player.GetComponent<Player> ().Score += 50;
		EnemySpawning.GetComponent<EnemySpawning> ().EnemiesSpawned -= 1;
		EnemySpawning.GetComponent<EnemySpawning> ().Soldiers -= 1;
		HealthPacks.GetComponent<HeathPacks> ().Rollforspawn = true;
	}

}
