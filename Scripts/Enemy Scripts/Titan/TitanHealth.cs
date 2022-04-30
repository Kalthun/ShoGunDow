using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanHealth : MonoBehaviour {

	//Variables
	GameObject EnemySpawning, HealthPacks, InvinciboostSpawning, BurstboostSpawning, ShieldboostSpawning, Player;
	public GameObject[] Bullets;
	public float TitanHealthTotal = 10f;
	bool ILock, BLock, SLock, Shoot;

	//Start
	void Start () {

		//Assign
		Player = GameObject.Find ("Player");
		EnemySpawning = GameObject.Find ("Enemy Spawning");
		HealthPacks = GameObject.Find ("Health Packs");
		InvinciboostSpawning = GameObject.Find ("Invinciboost Spawning");
		BurstboostSpawning = GameObject.Find ("Burstboost Spawning");
		ShieldboostSpawning = GameObject.Find ("Shieldboost Spawning");
		Shoot = true;

	}

	// Update is called once per frame
	void Update () {

		if (TitanHealthTotal <= 0f) {
			Die ();
		}

		//Only 1 of each
		if (InvinciboostSpawning.GetComponent <InvinciboostSpawning> ().InvinciboostsSpawned < 1) {
			ILock = false;
		}
		if (BurstboostSpawning.GetComponent <BurstboostSpawning> ().BurstboostsSpawned < 1) {
			ILock = false;
		}
		if (ShieldboostSpawning.GetComponent <ShieldboostSpawning> ().ShieldboostsSpawned < 1) {
			ILock = false;
		}

		//Player Y spawns different bulletes
		if (Player.transform.position.y < 3) {
			if (Shoot) {
				Shoot = false;
				Invoke ("WaitToFire", 2);
				GameObject GroundBulletGO = Instantiate (Bullets [0], transform) as GameObject;
				GroundBulletGO.transform.position = new Vector3 (GroundBulletGO.transform.position.x, 0.5f, GroundBulletGO.transform.position.z);
			}
		}
	
		if (Player.transform.position.y > 3 && Player.transform.position.y < 6) {
			if (Shoot) {
				Shoot = false;
				Invoke ("WaitToFire", 3);
				GameObject GroundBulletGO = Instantiate (Bullets [1], transform) as GameObject;
				GroundBulletGO.transform.position = new Vector3 (GroundBulletGO.transform.position.x, 3.5f, GroundBulletGO.transform.position.z);
			}
		}

		if (Player.transform.position.y > 6) {
			if (Shoot) {
				Shoot = false;
				Invoke ("WaitToFire", 5);
				GameObject GroundBulletGO = Instantiate (Bullets [2], transform) as GameObject;
				GroundBulletGO.transform.position = new Vector3 (GroundBulletGO.transform.position.x, 6f, GroundBulletGO.transform.position.z);
			}
		}

	}

	//On Death
	void Die () {

		Destroy (gameObject);
		Player.GetComponent<Player> ().Score += 100;
		EnemySpawning.GetComponent<EnemySpawning> ().EnemiesSpawned -= 1;
		EnemySpawning.GetComponent<EnemySpawning> ().Titans -= 1;
		HealthPacks.GetComponent<HeathPacks> ().Rollforspawn = true;

		int Check = 0;

		//Randomly Spawn 1 boost on dealth
		while (true) {
			
			int Chance = Random.Range (1, 11);

			if (Chance < 3) {

				if (!ILock) {
					if (InvinciboostSpawning.GetComponent<InvinciboostSpawning> ().InvinciboostsSpawned < 1) {
						InvinciboostSpawning.GetComponent<InvinciboostSpawning> ().DropI = true;
						break;
					} else if (InvinciboostSpawning.GetComponent<InvinciboostSpawning> ().InvinciboostsSpawned >= 1) {
						Check += 1;
						ILock = true;
					}
				}

			} else if (Chance > 2 && Chance < 5) {

				if (!BLock) {
					if (BurstboostSpawning.GetComponent<BurstboostSpawning> ().BurstboostsSpawned < 1) {
						BurstboostSpawning.GetComponent<BurstboostSpawning> ().DropB = true;
						break;
					} else if (BurstboostSpawning.GetComponent<BurstboostSpawning> ().BurstboostsSpawned >= 1) {
						Check += 1;
						BLock = true;
					}
				}

			} else if (Chance > 4) {

				if (!SLock) {
					if (ShieldboostSpawning.GetComponent<ShieldboostSpawning> ().ShieldboostsSpawned < 1) {
						ShieldboostSpawning.GetComponent<ShieldboostSpawning> ().DropS = true;
						break;
					} else if (ShieldboostSpawning.GetComponent<ShieldboostSpawning> ().ShieldboostsSpawned >= 1) {
						Check += 1;
						SLock = true;
					}
				}

			}

			if (Check >= 3) {
				break; 
			}
				
		}

		Check = 0;

	}

	public void WaitToFire () {
		Shoot = true;
	}

}
