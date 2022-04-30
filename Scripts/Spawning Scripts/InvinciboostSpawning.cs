using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinciboostSpawning : MonoBehaviour {

	//Variables
	GameObject PillarGeneration, AmmoDrops, HealthPacks, BurstboostSpawning, ShieldboostSpawning;
	public GameObject PrefabInvinciboost;
	GameObject Invinciboost;
	public Vector3 InvinciboostSpawnpoint;
	Vector3 CheckingForSpawn;
	public bool AllowSpawn, DropI;
	public int CheckForx, CheckForz, InvinciboostsWanted, InvinciboostsSpawned;

	// Use this for initialization
	void Start () {

		//Assign
		PillarGeneration = GameObject.Find ("Pillar Generation");
		AmmoDrops = GameObject.Find ("Ammo Drops");
		HealthPacks = GameObject.Find ("Health Packs");
		BurstboostSpawning = GameObject.Find ("Burstboost Spawning");
		ShieldboostSpawning = GameObject.Find ("Shieldboost Spawning");

		//Check Position Variables
		CheckForx = -40;
		CheckForz = 40;
		CheckingForSpawn = new Vector3 (CheckForx, 0, CheckForz);

		//Invinciboosts
		InvinciboostsWanted = 1;
		InvinciboostsSpawned = 0;
		AllowSpawn = true;
		DropI = false;

	}
	
	// Update is called once per frame
	void Update () {

		//Called For a chance at spawning
		if (DropI) {

			//Buffer
			Dropped ();

			//Random Spawining (Because there is a buffer, there will need to be a while loop)
			while (InvinciboostsSpawned < InvinciboostsWanted) {

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
					for (int i = 0; i < BurstboostSpawning.GetComponent<BurstboostSpawning> ().BurstboostsWanted; i++) {
						if (CheckingForSpawn == BurstboostSpawning.GetComponent<BurstboostSpawning> ().BurstboostSpawnpoint) {
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
						InvinciboostSpawnpoint = new Vector3 (CheckingForSpawn.x, 0, CheckingForSpawn.z);

						Invinciboost = Instantiate (PrefabInvinciboost, transform) as GameObject;
						Invinciboost.transform.position = InvinciboostSpawnpoint;
						Invinciboost.transform.rotation = Quaternion.Euler (0, YRotation, 0);
						InvinciboostsSpawned += 1;

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
		DropI = false;
	}

}
