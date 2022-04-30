using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrops : MonoBehaviour {

	//Variables
	GameObject GameController, PillarGeneration, HealthPacks, InvinciboostSpawning, BurstboostSpawning, ShieldboostSpawning;
	public GameObject PrefabAmmo;
	GameObject AmmoBox;
	public Vector3[] AmmoBoxSpawnpoints;
	Vector3 CheckingForSpawn;
	bool AllowSpawn, SpawnCycle;
	public int CheckForx, CheckForz, AmmoBoxesWanted, AmmoBoxesSpawned, AmmoBoxesRespawn;

	// Use this for initialization
	void Start () {

		//Assign
		GameController = GameObject.Find ("Game Controller");
		PillarGeneration = GameObject.Find ("Pillar Generation");
		HealthPacks = GameObject.Find ("Health Packs");
		InvinciboostSpawning = GameObject.Find ("Invinciboost Spawning");
		BurstboostSpawning = GameObject.Find ("Burstboost Spawning");
		ShieldboostSpawning = GameObject.Find ("Shieldboost Spawning");

		//Check Position Variables
		CheckForx = -40;
		CheckForz = 40;
		CheckingForSpawn = new Vector3 (CheckForx, 0, CheckForz);

		//AmmoBoxes
		AmmoBoxesWanted = 3;
		AmmoBoxesSpawned = 0;
		AmmoBoxesRespawn = 0;
		AllowSpawn = true;
		SpawnCycle = false;

		//Arrays
		AmmoBoxSpawnpoints = new Vector3[AmmoBoxesWanted];

	}
		
	// Update is called once per frame
	void Update () {

		if (GameController.GetComponent<GameController> ().SpawnAmmo) {

			if (AmmoBoxesSpawned < AmmoBoxesWanted) {

				// 1 in 20 chance of spawning
				int Chance = Random.Range (0, 21);

				if (Chance == 0) {

					//Checks if Position has been used
					for (int i = 0; i < PillarGeneration.GetComponent<PillarGeneration>().PillarSpawnpoints.Length; i++) {
						if (CheckingForSpawn == PillarGeneration.GetComponent<PillarGeneration> ().PillarSpawnpoints [i]) {
							AllowSpawn = false;
						}
					}
					for (int i = 0; i < HealthPacks.GetComponent<HeathPacks>().HealthPacksWanted; i++) {
						if (CheckingForSpawn == HealthPacks.GetComponent<HeathPacks>().HealthPackSpawnpoint) {
							AllowSpawn = false;
						}
					}
					for (int i = 0; i < InvinciboostSpawning.GetComponent<InvinciboostSpawning>().InvinciboostsWanted; i++) {
						if (CheckingForSpawn == InvinciboostSpawning.GetComponent<InvinciboostSpawning>().InvinciboostSpawnpoint) {
							AllowSpawn = false;
						}
					}
					for (int i = 0; i < BurstboostSpawning.GetComponent<BurstboostSpawning>().BurstboostsWanted; i++) {
						if (CheckingForSpawn == BurstboostSpawning.GetComponent<BurstboostSpawning>().BurstboostSpawnpoint) {
							AllowSpawn = false;
						}
					}
					for (int i = 0; i < ShieldboostSpawning.GetComponent<ShieldboostSpawning>().ShieldboostsWanted; i++) {
						if (CheckingForSpawn == ShieldboostSpawning.GetComponent<ShieldboostSpawning>().ShieldboostSpawnpoint) {
							AllowSpawn = false;
						}
					}
					for (int i = 0; i < AmmoBoxSpawnpoints.Length; i++) {
						if (CheckingForSpawn == AmmoBoxSpawnpoints [i]) {
							AllowSpawn = false;
						}
					}
						
					//Spawns Ammo Box
					if (AllowSpawn) {

						if (SpawnCycle) {

							int YRotation = Random.Range (0, 361);
							AmmoBoxSpawnpoints [AmmoBoxesRespawn] = new Vector3 (CheckingForSpawn.x, 0.5f, CheckingForSpawn.z);

							AmmoBox = Instantiate (PrefabAmmo, transform) as GameObject;
							AmmoBox.transform.position = AmmoBoxSpawnpoints [AmmoBoxesRespawn];
							AmmoBox.transform.rotation = Quaternion.Euler (0, YRotation, 0);
							AmmoBoxesSpawned += 1;
							AmmoBoxesRespawn += 1;
							if (AmmoBoxesRespawn > 2) {
								AmmoBoxesRespawn = 0;
							}

						} else {

							int YRotation = Random.Range (0, 361);
							AmmoBoxSpawnpoints [AmmoBoxesSpawned] = new Vector3 (CheckingForSpawn.x, 0.5f, CheckingForSpawn.z);

							AmmoBox = Instantiate (PrefabAmmo, transform) as GameObject;
							AmmoBox.transform.position = AmmoBoxSpawnpoints [AmmoBoxesSpawned];
							AmmoBox.transform.rotation = Quaternion.Euler (0, YRotation, 0);
							AmmoBoxesSpawned += 1;

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

			//Begins Normal Cycle
			if (AmmoBoxesSpawned == AmmoBoxesWanted) {
				SpawnCycle = true;
			}

		}
	}
}
