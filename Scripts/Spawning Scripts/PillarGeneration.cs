using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarGeneration : MonoBehaviour {

	//Variables
	GameObject GameController;
	public GameObject[] PrefabPillars = new GameObject[2];
	GameObject[] Pillars;
	public Vector3[] PillarSpawnpoints;
	Vector3 CheckingForSpawn;
	public bool AllowSpawn, ShatteredPillar, DonePillarSpawning;
	int CheckForx, CheckForz, PillarsWanted, PillarsSpawned;

	// Use this for initialization
	void Start () {

		//Assign
		GameController = GameObject.Find ("Game Controller");

		//Check Position Variables
		CheckForx = -40;
		CheckForz = 40;
		CheckingForSpawn = new Vector3 (CheckForx, 0, CheckForz);

		//Pillars
		PillarsWanted = 15;
		PillarsSpawned = 0;
		AllowSpawn = true;
		ShatteredPillar = false;
		DonePillarSpawning = false;

		//Arrays
		Pillars = new GameObject[PillarsWanted];
		PillarSpawnpoints = new Vector3[PillarsWanted];

	}

	// Update is called once per frame
	void Update () {
		
		if (GameController.GetComponent<GameController> ().SpawnPillars) {

			//Buffer
			GameController.GetComponent<GameController> ().SpawnPillars = false;

			//Random Spawining (Because there is a buffer, there will need to be a while loop)
			while (PillarsSpawned < PillarsWanted) {

				// 1 in 20 chance of spawning
				int Chance = Random.Range (0, 21);

				if (Chance == 0) {

					//Checks if Position has been used
					for (int i = 0; i < PillarsWanted; i++) {
						if (CheckingForSpawn == PillarSpawnpoints [i]) {
							AllowSpawn = false;
						}
					}

					//Spawns Pillar
					if (AllowSpawn) {

						int YRotation = Random.Range (0, 361);
						PillarSpawnpoints [PillarsSpawned] = CheckingForSpawn;

						//Always Have One Shattered Pillar
						if (!ShatteredPillar) {
							Pillars [0] = Instantiate (PrefabPillars [1], transform) as GameObject;
							ShatteredPillar = true;
							Pillars [0].transform.position = PillarSpawnpoints [0];
							Pillars [0].transform.rotation = Quaternion.Euler (0, YRotation, 0);
							PillarsSpawned += 1;
						} else {
							Pillars [PillarsSpawned] = Instantiate (PrefabPillars [Random.Range (0, PrefabPillars.Length)], transform) as GameObject;
							Pillars [PillarsSpawned].transform.position = PillarSpawnpoints [PillarsSpawned];
							Pillars [PillarsSpawned].transform.rotation = Quaternion.Euler (0, YRotation, 0);
							PillarsSpawned += 1;
						}
					}
				}

				//Cycle Through Positions
				if (CheckForx == 40) {
					CheckForx = -40;
					CheckForz -= 10;
				} else {
					CheckForx += 10;
				}
				if (CheckForx == -40 && CheckForz == -40) {
					CheckForx = -40;
					CheckForz = 40;
				}

				//Reset Checked Position
				CheckingForSpawn = new Vector3 (CheckForx, 0, CheckForz);
				AllowSpawn = true;
			}

			//Start Enemy/Ammo Spawning/Health Pack
			DonePillarSpawning = true;
		}
	}
}