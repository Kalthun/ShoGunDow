using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathPacks : MonoBehaviour {

	//Variables
	GameObject PillarGeneration, AmmoDrops, InvinciboostSpawning, BurstboostSpawning, ShieldboostSpawning;
	public GameObject PrefabHealthPack;
	GameObject HealthPack;
	public Vector3 HealthPackSpawnpoint;
	Vector3 CheckingForSpawn;
	public bool AllowSpawn, Rollforspawn;
	public int CheckForx, CheckForz, HealthPacksWanted, HealthPacksSpawned;

	// Use this for initialization
	void Start () {

		//Assign
		PillarGeneration = GameObject.Find ("Pillar Generation");
		AmmoDrops = GameObject.Find ("Ammo Drops");
		InvinciboostSpawning = GameObject.Find ("Invinciboost Spawning");
		BurstboostSpawning = GameObject.Find ("Burstboost Spawning");
		ShieldboostSpawning = GameObject.Find ("Shieldboost Spawning");

		//Check Position Variables
		CheckForx = -40;
		CheckForz = 40;
		CheckingForSpawn = new Vector3 (CheckForx, 0, CheckForz);

		//HealthPacks
		HealthPacksWanted = 1;
		HealthPacksSpawned = 0;
		AllowSpawn = true;
		Rollforspawn = false;

	}

	// Update is called once per frame
	void Update () {

		//Called For a chance at spawning
		if (Rollforspawn) {

			//Buffer
			StopRolling ();

			// 1 in 20 chance of spawning
			int Chance = Random.Range (0, 51);

			if (Chance == 0) {

				//Random Spawining (Because there is a buffer, there will need to be a while loop)
				while (HealthPacksSpawned < HealthPacksWanted) {

					int Chance2 = Random.Range (0, 21);

					if (Chance2 == 0) {
						
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

						if (AllowSpawn) {

							int YRotation = Random.Range (0, 361);
							HealthPackSpawnpoint = new Vector3 (CheckingForSpawn.x, 0, CheckingForSpawn.z);

							HealthPack = Instantiate (PrefabHealthPack, transform) as GameObject;
							HealthPack.transform.position = HealthPackSpawnpoint;
							HealthPack.transform.rotation = Quaternion.Euler (0, YRotation, 0);
							HealthPacksSpawned += 1;

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
	}

	void StopRolling () {
		Rollforspawn = false;
	}

}

