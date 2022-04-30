using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

	//Variables
	GameObject GameController;
	public GameObject[] PrefabEnemies = new GameObject[3];
	GameObject Enemies;
	public Transform[] Portals = new Transform[4];
	public int EnemiesWanted, EnemiesSpawned, Chasers, Soldiers, Titans;

	// Use this for initialization
	void Start () {

		//Assign GameObjects
		GameController = GameObject.Find ("Game Controller");

		//Assign Variables
		EnemiesWanted = 10;
		EnemiesSpawned = 0;
		Chasers = 0;
		Soldiers = 0;
		Titans = 0;

	}
	
	// Update is called once per frame
	void Update () {

		if (GameController.GetComponent<GameController> ().SpawnEnemies) {
			
			if (EnemiesSpawned < EnemiesWanted) {

				//Always 2 Titans, 4 Soldiers, 4 Chasers
				if (Titans < 2) {
					Enemies = Instantiate (PrefabEnemies [2], Portals [Random.Range (0, Portals.Length)]) as GameObject;
					EnemiesSpawned += 1;
					Titans += 1;
				} else if (Soldiers < 4) {
					Enemies = Instantiate (PrefabEnemies [1], Portals [Random.Range (0, Portals.Length)]) as GameObject;
					EnemiesSpawned += 1;
					Soldiers += 1;
				} else {
					Enemies = Instantiate (PrefabEnemies [0], Portals [Random.Range (0, Portals.Length)]) as GameObject;
					EnemiesSpawned += 1;
					Chasers += 1;
				}

			}
		}
	}
}
