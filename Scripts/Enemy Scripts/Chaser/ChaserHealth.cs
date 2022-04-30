using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserHealth : MonoBehaviour {

	//Variables
	GameObject EnemySpawning, HealthPacks, Player;
	public float ChaserHealthTotal = 2f;

	//Start
	void Start () {

		//Assign
		Player = GameObject.Find ("Player");
		EnemySpawning = GameObject.Find ("Enemy Spawning");
		HealthPacks = GameObject.Find ("Health Packs");

	}

	// Update is called once per frame
	void Update () {

		if (ChaserHealthTotal <= 0f) {
			Die ();
		}
	}

	void Die () {
		Destroy (gameObject);
		Player.GetComponent<Player> ().Score += 10;
		EnemySpawning.GetComponent<EnemySpawning> ().EnemiesSpawned -= 1;
		EnemySpawning.GetComponent<EnemySpawning> ().Chasers -= 1;
		HealthPacks.GetComponent<HeathPacks> ().Rollforspawn = true;
	}

}
