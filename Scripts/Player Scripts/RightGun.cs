using UnityEngine;
using UnityEngine.UI;

public class RightGun : MonoBehaviour {

	//Left Gun Variables
	int range = 25;
	public float fireRate = 15f;
	private float MaxAmmo = 4f;
	public float CurrentAmmo;
	private float nextTimeToFire = 0f;
	public bool ShotBuffer, PauseBuffer;
	public bool Reload, BurstBoost;
	public Camera PlayerPOV;
	public ParticleSystem RightGunMuzzleFlash;
	public GameObject RightGunImpact, RightGunHammer;
	public Text AmmoText;
	public Material GoldC, RedC, BurstC;

	// Use this for initialization
	void Start () {

		//Assign
		PauseBuffer = false;
		RightGunHammer = GameObject.Find ("Right Gun Hammer");

		//Set Ammo
		CurrentAmmo = MaxAmmo;

	}

	// Update is called once per frame
	void Update () {

		//UI Ammo Left
		AmmoText.text = CurrentAmmo + " / " + MaxAmmo;

		//Reload
		if (Reload) {
			CurrentAmmo = MaxAmmo;
			Reloading ();
		}

		//BOOST!
		if (!PauseBuffer) {
			if (BurstBoost) {

				//BURST BOOST
				if (Input.GetButton ("Fire2") && Time.time >= nextTimeToFire) {
					nextTimeToFire = Time.time + 1f / fireRate;
					CurrentAmmo = Mathf.Infinity;
					Shoot ();
				}

			} else {

				//Normal Mode
				if (Input.GetButton ("Fire2")) {
					if (!ShotBuffer) {
						ShotBuffer = true;
						Invoke ("ShotBufferDisable", 0.5f);
						Shoot ();
					}

				}
			}

			//Change Colour of Hammer
			if (BurstBoost) {
				RightGunHammer.GetComponent<Renderer> ().material = BurstC;
			} else {
				if (ShotBuffer) {
					RightGunHammer.GetComponent<Renderer> ().material = RedC;
				} else {
					RightGunHammer.GetComponent<Renderer> ().material = GoldC;
				}

			}
		}

	}

	void Shoot ()
	{

		//Checks For Ammo
		if (CurrentAmmo <= 0) {
			return;
		}

		//Ammo
		CurrentAmmo--;

		//ShootBeam
		RaycastHit hit;
		if (Physics.Raycast (PlayerPOV.transform.position, PlayerPOV.transform.forward, out hit, range)) {

			//MuzzleFlash
			RightGunMuzzleFlash.Play ();

			//ShotImpact
			GameObject ImpactGO = Instantiate (RightGunImpact, hit.point, Quaternion.LookRotation (hit.normal)) as GameObject;
			Destroy (ImpactGO, 0.2f);

			//Checks If Hit Object is Chaser
			Chaser ChaserTarget = hit.transform.GetComponent<Chaser> ();
			string ChaserTargetName = hit.transform.name;

			//Damage Chaser
			if (ChaserTargetName == "Chaser Head") {
				ChaserTarget.TakeDamage (2);
			}
			if (ChaserTargetName == "Chaser Eye") {
				ChaserTarget.TakeDamage (2);
			}

			//Checks If Hit Object is Soldier
			Soldier SoldierTarget = hit.transform.GetComponent<Soldier> ();
			string SoldierTargetName = hit.transform.name;
			string SoldierTargetTag = hit.transform.tag;

			//Damage Soldier
			if (SoldierTargetName == "Soldier Body") {
				SoldierTarget.TakeDamage (3);
			}
			if (SoldierTargetName == "Soldier Head") {
				SoldierTarget.TakeDamage (9);
			}
			if (SoldierTargetTag == "Soldier Face") {
				SoldierTarget.TakeDamage (9);
			}

			//Checks If Hit Object is Titan
			Titan TitanTarget = hit.transform.GetComponent<Titan> ();
			string TitanTargetName = hit.transform.name;

			//Damage Titan
			if (TitanTargetName == "Titan Head") {
				TitanTarget.TakeDamage (5);
			}
			if (TitanTargetName == "Titan Eye") {
				TitanTarget.TakeDamage (10);
			}	
		}
	}
		
	//Reload Buffer
	void Reloading () {
		Reload = false;
	}

	//ShotBuffer
	void ShotBufferDisable () {
		ShotBuffer = false;
	}

}
	