using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstboostSpawning : MonoBehaviour {

	//Variables
	GameObject PillarGeneration, AmmoDrops, HealthPacks, InvinciboostSpawning, ShieldboostSpawning;
	public GameObject PrefabBurstboost;
	GameObject Burstboost;
	public Vector3 BurstboostSpawnpoint;
	Vector3 CheckingForSpawn;
	public bool AllowSpawn, DropB;
	public int CheckForx, CheckForz, BurstboostsWanted, BurstboostsSpawned;

	// Use this for initialization
	void Start () {

		//Assign
		PillarGeneration = GameObject.Find ("Pillar Generation");
		AmmoDrops = GameObject.Find ("Ammo Drops");
		HealthPacks = GameObject.Find ("Health Packs");
		InvinciboostSpawning = GameObject.Find ("Invinciboost Spawning");
		ShieldboostSpawning = GameObject.Find ("Shieldboost Spawning");

		//Check Position Variables
		CheckForx = -40;
		CheckForz = 40;
		CheckingForSpawn = new Vector3 (CheckForx, 0, CheckForz);

		//Burstboosts
		BurstboostsWanted = 1;
		BurstboostsSpawned = 0;
		AllowSpawn = true;
		DropB = false;

	}
	
	// Update is called once per frame
	void Update () {

		//Called For a chance at spawning
		if (DropB) {

			//Buffer
			Dropped ();

			//Random Spawining (Because there is a buffer, there will need to be a while loop)
			while (BurstboostsSpawned < BurstboostsWanted) {

				int Chance = Random.Range (0, 21);

				if (Chance == 0) {

					//Checks if Position has been used
					for (int i = 0; i < PillarGeneration.GetComponent<PillarGeneration> ().PillarSpawnpoints.Length; i++) {
						if (CheckingForSpawn == PillarGeneration.GetComponent<PillarGeneration> ().PillarSpawnpoints [i]) {
							AllowSpawn = false;
						}
					}
					for (int i = 0; i < AmmoDrops.GetComponent<AmmoDrops> ().AmmoBoxSpawnpoints.Length; i++) {
						if (CheckingForSpawn == AmmoDrops.GetComponent<AmmoDrops> ().AmmoBoxSpawnpoints [i]) {
							AllowSpawn = false;
						}
					}
					for (int i = 0; i < HealthPacks.GetComponent<HeathPacks> ().HealthPacksWanted; i++) {
						if (CheckingForSpawn == HealthPacks.GetComponent<HeathPacks> ().HealthPackSpawnpoint) {
							AllowSpawn = false;
						}
					}
					for (int i = 0; i < InvinciboostSpawning.GetComponent<InvinciboostSpawning>().InvinciboostsWanted; i++) {
						if (CheckingForSpawn == InvinciboostSpawning.GetComponent<InvinciboostSpawning>().InvinciboostSpawnpoint) {
							AllowSpawn = false;
						}
					}
					for (int i = 0; i < ShieldboostSpawning.GetComponent<ShieldboostSpawning> ().ShieldboostsWanted; i++) {
						if (CheckingForSpawn == ShieldboostSpawning.GetComponent<ShieldboostSpawning> ().ShieldboostSpawnpoint) {
							AllowSpawn = false;
						}
					}

					if (AllowSpawn) { 

						int YRotation = Random.Range (0, 361);
						BurstboostSpawnpoint = new Vector3 (CheckingForSpawn.x, 0, CheckingForSpawn.z);

						Burstboost = Instantiate (PrefabBurstboost, transform) as GameObject;
						Burstboost.transform.position = BurstboostSpawnpoint;
						Burstboost.transform.rotation = Quaternion.Euler (0, YRotation, 0);
						BurstboostsSpawned += 1;

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
		}
	}

	void Dropped () {
		DropB = false;
	}

}
