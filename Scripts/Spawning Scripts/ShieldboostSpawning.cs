using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldboostSpawning : MonoBehaviour {

	//Variables
	GameObject PillarGeneration, AmmoDrops, HealthPacks, InvinciboostSpawning, BurstboostSpawning;
	public GameObject PrefabShieldboost;
	GameObject Shieldboost;
	public Vector3 ShieldboostSpawnpoint;
	Vector3 CheckingForSpawn;
	public bool AllowSpawn, DropS;
	public int CheckForx, CheckForz, ShieldboostsWanted, ShieldboostsSpawned;

	// Use this for initialization
	void Start () {
	
		//Assign
		PillarGeneration = GameObject.Find ("Pillar Generation");
		AmmoDrops = GameObject.Find ("Ammo Drops");
		HealthPacks = GameObject.Find ("Health Packs");
		InvinciboostSpawning = GameObject.Find ("Invinciboost Spawning");
		BurstboostSpawning = GameObject.Find ("Burstboost Spawning");

		//Check Position Variables
		CheckForx = -40;
		CheckForz = 40;
		CheckingForSpawn = new Vector3 (CheckForx, 0, CheckForz);

		//ShieldBoosts
		ShieldboostsWanted = 1;
		ShieldboostsSpawned = 0;
		AllowSpawn = true;
		DropS = false;

	}
	
	// Update is called once per frame
	void Update () {

		//Called For a chance at spawning
		if (DropS) {

			//Buffer
			Dropped ();

			//Random Spawining (Because there is a buffer, there will need to be a while loop)
			while (ShieldboostsSpawned < ShieldboostsWanted) {

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
					for (int i = 0; i < BurstboostSpawning.GetComponent<BurstboostSpawning> ().BurstboostsWanted; i++) {
						if (CheckingForSpawn == BurstboostSpawning.GetComponent<BurstboostSpawning> ().BurstboostSpawnpoint) {
							AllowSpawn = false;
						}
					}

					if (AllowSpawn) { 

						int YRotation = Random.Range (0, 361);
						ShieldboostSpawnpoint = new Vector3 (CheckingForSpawn.x, 0, CheckingForSpawn.z);

						Shieldboost = Instantiate (PrefabShieldboost, transform) as GameObject;
						Shieldboost.transform.position = ShieldboostSpawnpoint;
						Shieldboost.transform.rotation = Quaternion.Euler (0, YRotation, 0);
						ShieldboostsSpawned += 1;

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
		DropS = false;
	}

}
