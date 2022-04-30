using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	//Controller Variables
	public float speed = 10.0f;
	public float jumpSpeed = 8.0F;
	float gravity = 15.0F;
	public float sensitivity = 3.0f;
	float Csensitivity = 3.0f;
	private GameObject eyes;
	private Vector3 moveDirection;
	private CharacterController controller;
	private float turner, looker;

	//Player Variables
	float PlayerHealth, PlayerMaxHealth, PlayerShield, PlayerMaxShield, Seconds, Minutes, BoostSeconds;
	bool HealthBuffer, Invincible, Bursting, Scoring, Timer, TimerOn, BoostTime, GameisOver, GameisPaused;
	public float Score;

	//Other Variables
	GameObject [] Guns = new GameObject[2];
	GameObject[] ArmBands = new GameObject[2];
	GameObject GameController, AmmoDrops, HealthPacks, InvinciboostSpawning, BurstboostSpawning, ShieldboostSpawning;
	public GameObject PauseMenuUI;
	public Image HealthBar, ShieldBar, GameOverScreen;
	public Text ScoreText, PlayerHealthText, TimerText, BoostTimeText, GameOverText, RestartText;

	//Colors
	public Material BurstC, InvincibleC, ShieldC, White;

	//Start
	void Start () {

		//Assign
		GameController = GameObject.Find ("Game Controller");
		AmmoDrops = GameObject.Find ("Ammo Drops");
		HealthPacks = GameObject.Find ("Health Packs");
		InvinciboostSpawning = GameObject.Find ("Invinciboost Spawning");
		BurstboostSpawning = GameObject.Find ("Burstboost Spawning");
		ShieldboostSpawning = GameObject.Find ("Shieldboost Spawning");
		Guns = GameObject.FindGameObjectsWithTag ("Gun");
		ArmBands = GameObject.FindGameObjectsWithTag ("Arm Band");

		//Setup Controller
		eyes = GameObject.Find ("Player POV");
		controller= GetComponent<CharacterController> ();
		moveDirection = new Vector3 (0, 0, 0);

		//Pause / End
		Time.timeScale = 1f;
		GameisPaused = false;
		PauseMenuUI.SetActive (false);
		GameisOver = false;

		//Set Health / Shield
		PlayerMaxHealth = 100;
		PlayerHealth = PlayerMaxHealth;
		PlayerMaxShield = 100;
		PlayerShield = 0;

		//Start scoring
		Scoring = true;

		//Start timer
		Timer = true;
		TimerOn = true;
		Tic ();

		//Set Timer
		BoostSeconds = 10;

	}

	//Update
	void Update () {

		//If Game Controller Has Started Game
		if (GameController.GetComponent<GameController> ().StartGame) {
			
			//Movement
			if (controller.isGrounded) {
				moveDirection.x = Input.GetAxis ("Horizontal") * speed;
				moveDirection.z = Input.GetAxis ("Vertical") * speed;
				moveDirection = transform.TransformDirection (moveDirection);
				if (Input.GetButton ("Jump")) {
					moveDirection.y = jumpSpeed;
				}
			}
			turner = Input.GetAxis ("Mouse X") * sensitivity; // L/R
			if (turner != 0) {
				transform.eulerAngles += new Vector3 (0, turner, 0);
			}
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move (moveDirection * Time.deltaTime);
					
			//Health / Shield
			PlayerHealthText.text = PlayerHealth + " / " + PlayerMaxHealth;
			HealthBar.fillAmount = PlayerHealth / PlayerMaxHealth;
			ShieldBar.fillAmount = PlayerShield / PlayerMaxShield;

			//Score
			if (Scoring) {
				ScoreText.text = Score.ToString ();
			} else {
				ScoreText.text = "";
			}

			//IG Time
			if (Timer) {
				if (Seconds == 60) {
					Seconds = 0;
					Minutes = Minutes + 1;
				}
				if (Minutes < 10 && Seconds < 10) {
					TimerText.text = "0" + Minutes.ToString () + " : 0" + Seconds.ToString ();
				} else if (Minutes < 10 && Seconds >= 10) {
					TimerText.text = "0" + Minutes.ToString () + " : " + Seconds.ToString ();
				} else if (Minutes >= 10 && Seconds < 10) {
					TimerText.text = Minutes.ToString () + " : 0" + Seconds.ToString ();
				} else if (Minutes >= 10 && Seconds >= 10) { 
					TimerText.text = Minutes.ToString () + " : " + Seconds.ToString ();
				}
			} else {
				TimerText.text = "";
			}

			//BoostTime
			if (BoostTime) {
				if (BoostSeconds <= 0) {
					StopBoostTimer ();
				}
				BoostTimeText.text = BoostSeconds.ToString ();
			} else { 
				BoostTimeText.text = "";
			}

			//GameOver
			if (PlayerHealth <= 0 || Score == 10000 || (Minutes == 59 && Seconds == 59)) {
				GameOver ();
			}

			//GameOver / Pause Menu
			if (!GameisOver) {
				GameOverText.text = "";
				GameOverScreen.enabled = false;
				RestartText.text = "";
				PauseMenu ();
			} else {
				if (Input.GetKeyDown (KeyCode.R)) {
					ChangeToScene (1);
				}
				if (Input.GetKeyDown (KeyCode.M)) {
					ChangeToScene (0);
				}
			}
		}

	}

	//Collisions
	void OnTriggerStay (Collider hit) {

		//Hit Chaser
		if (hit.gameObject.tag == "Chaser") {
			if (!HealthBuffer && !Invincible) {
				HealthBuffer = true;
				Invoke ("HealthBufferDisable", 0.05f);
				if (PlayerShield > 0) {
					PlayerShield -= 1;
				} else {
					PlayerHealth -= 1;
				}
			}
		}

		//Hit Soldier
		if (hit.gameObject.tag == "Soldier") {
			if (!HealthBuffer && !Invincible) {
				HealthBuffer = true;
				Invoke ("HealthBufferDisable", 0.075f);
				if (PlayerShield > 0) {
					PlayerShield -= 3;
				} else {
					PlayerHealth -= 3;
				}
			}
		}

		//Hit Titan
		if (hit.gameObject.tag == "Titan") {
			if (!HealthBuffer && !Invincible) {
				HealthBuffer = true;
				Invoke ("HealthBufferDisable", 0.5f);
				if (PlayerShield > 0) {
					PlayerShield -= 40;
				} else {
					PlayerHealth -= 40;
				}
			}
		}

		//Hit Bullet
		if (hit.gameObject.tag == "Bullet") {
			
			if (hit.gameObject.name == "Ground(Clone)") {
				if (!HealthBuffer && !Invincible) {
					HealthBuffer = true;
					Invoke ("HealthBufferDisable", 0.075f);
					Destroy (hit.gameObject);
					if (PlayerShield > 0) {
						PlayerShield -= 10;
					} else {
						PlayerHealth -= 10;
					}
				}
			}

			if (hit.gameObject.name == "Body(Clone)") {
				if (!HealthBuffer && !Invincible) {
					HealthBuffer = true;
					Invoke ("HealthBufferDisable", 0.075f);
					Destroy (hit.gameObject);
					if (PlayerShield > 0) {
						PlayerShield -= 20;
					} else {
						PlayerHealth -= 20;
					}
				}
			}

			if (hit.gameObject.name == "Sky(Clone)") {
				if (!HealthBuffer && !Invincible) {
					HealthBuffer = true;
					Invoke ("HealthBufferDisable", 0.075f);
					Destroy (hit.gameObject);
					if (PlayerShield > 0) {
						PlayerShield -= 30;
					} else {
						PlayerHealth -= 30;
					}
				}
			}

		}

		//Reload Gun
		if (hit.gameObject.tag == "Ammo Box") {
			Guns [0].GetComponent<LeftGun> ().Reload = true;
			Guns [1].GetComponent<RightGun> ().Reload = true;
			Destroy (hit.gameObject);
			AmmoDrops.GetComponent<AmmoDrops> ().AmmoBoxesSpawned -= 1;
		}

		//Heal
		if (hit.gameObject.tag == "Health Pack") {
			PlayerHealth = PlayerMaxHealth;
			Destroy (hit.gameObject);
			HealthPacks.GetComponent<HeathPacks> ().HealthPacksSpawned -= 1;
		}

		//Invinciboost
		if (hit.gameObject.tag == "Invinciboost") {
			if (!Bursting) {
				MakeInvincible ();
				Invoke ("MakeVincible", 9);
				Destroy (hit.gameObject);
				InvinciboostSpawning.GetComponent<InvinciboostSpawning> ().InvinciboostsSpawned -= 1;
			}
		}

		//Burstboost
		if (hit.gameObject.tag == "Burstboost") {
			if (!Invincible) {
				BurstOn ();
				Invoke ("BurstOff", 9);
				Destroy (hit.gameObject);
				BurstboostSpawning.GetComponent<BurstboostSpawning> ().BurstboostsSpawned -= 1;
			}
		}

		//Shieldboost
		if (hit.gameObject.tag == "Shieldboost") {
			PlayerShield = PlayerMaxShield;
			Destroy (hit.gameObject);
			ShieldboostSpawning.GetComponent<ShieldboostSpawning> ().ShieldboostsSpawned -= 1;
		}
			
	}

	//Health Buffer
	void HealthBufferDisable () {
		HealthBuffer = false;
	}

	//Invincible
	void MakeInvincible () {
		StartBoostTimer ();
		Invincible = true;
		ArmBands [0].GetComponent<Renderer> ().material = InvincibleC;
		ArmBands [1].GetComponent<Renderer> ().material = InvincibleC;
	}

	//Vincible
	void MakeVincible () {
		Invincible = false;
		ArmBands [0].GetComponent<Renderer> ().material = White;
		ArmBands [1].GetComponent<Renderer> ().material = White;
	}

	//Burst
	void BurstOn () {
		StartBoostTimer ();
		Bursting = true;
		Guns [0].GetComponent<LeftGun> ().BurstBoost = true;
		Guns [1].GetComponent<RightGun> ().BurstBoost = true;
		ArmBands [0].GetComponent<Renderer> ().material = BurstC;
		ArmBands [1].GetComponent<Renderer> ().material = BurstC;
	}

	//NoBurst
	void BurstOff () {
		Bursting = false;
		Guns [0].GetComponent<LeftGun> ().BurstBoost = false;
		Guns [1].GetComponent<RightGun> ().BurstBoost = false;
		Guns [0].GetComponent<LeftGun> ().CurrentAmmo = 0;
		Guns [1].GetComponent<RightGun> ().CurrentAmmo = 0;
		ArmBands [0].GetComponent<Renderer> ().material = White;
		ArmBands [1].GetComponent<Renderer> ().material = White;
	}

	//Timer
	void Tic () {
		if (TimerOn) {
			Seconds += 1;
			Invoke ("Toc", 1);
		}
	}
	void Toc () {
		if (TimerOn) {
			Seconds += 1;
			Invoke ("Tic", 1);
		}
	}
	void StopTimer () {
		TimerOn = false;
	}

	//BoostTimer
	void StartBoostTimer () {
		BoostTime = true;
		BTic ();
	}
	void StopBoostTimer () {
		BoostTime = false;
		BoostSeconds = 10;
	}
	void BTic () {
		if (BoostTime) {
			BoostSeconds -= 1;
			Invoke ("BToc", 1);
		}
	}
	void BToc () {
		if (BoostTime) {
			BoostSeconds -= 1;
			Invoke ("BTic", 1);
		}
	}

	//GameOver
	void GameOver () {

		//Stop All Motions
		PlayerHealth = Mathf.Infinity;
		Invincible = true;
		Cursor.visible = true;
		GameisOver = true;
		GameOverScreen.enabled = true;
		speed = 0;
		jumpSpeed = 0;
		sensitivity = 0;
		Guns [0].GetComponent<LeftGun> ().ShotBuffer = true;
		Guns [1].GetComponent<RightGun> ().ShotBuffer = true;
		StopTimer ();

		//Text
		RestartText.text = "Press [R] for Restart\nPress [M] for Menu";
		if (Minutes < 10 && Seconds < 10) {
			GameOverText.text = "GAME OVER\nYour Score : " + Score + "\nYour Time [ 0" + Minutes.ToString () + " : 0" + Seconds.ToString() + " ]";
		} else if (Minutes < 10 && Seconds >= 10) {
			GameOverText.text = "GAME OVER\nYour Score : " + Score + "\nYour Time [ 0" + Minutes.ToString () + " : " + Seconds.ToString () + " ]";
		} else if (Minutes >= 10 && Seconds < 10) {
			GameOverText.text = "GAME OVER\nYour Score : " + Score + "\nYour Time [ " + Minutes.ToString () + " : 0" + Seconds.ToString () + " ]";
		} else if (Minutes >= 10 && Seconds >= 10) { 
			GameOverText.text = "GAME OVER\nYour Score : " + Score + "\nYour Time [ " + Minutes.ToString () + " : " + Seconds.ToString () + " ]";
		}

	}

	//ChangeToScene (IndexOfScene)
	public void ChangeToScene (int SceneNumber) {
		Application.LoadLevel (SceneNumber);
	}

	public void PauseMenu () {
		if (!GameisPaused) {
			Cursor.visible = false;
		} else {
			Cursor.visible = true;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GameisPaused) { 
				Resume ();
			} else {
				Pause ();
			}
		}
	}

	void Resume () {
		sensitivity = Csensitivity;
		Guns [0].GetComponent<LeftGun> ().PauseBuffer = false;
		Guns [1].GetComponent<RightGun> ().PauseBuffer = false;

		PauseMenuUI.SetActive (false);
		Time.timeScale = 1f;
		GameisPaused = false;
	}

	void Pause () {
		sensitivity = 0;
		Guns [0].GetComponent<LeftGun> ().PauseBuffer = true;
		Guns [1].GetComponent<RightGun> ().PauseBuffer = true;

		PauseMenuUI.SetActive (true);
		Time.timeScale = 0f;
		GameisPaused = true;
	}
		
	public void AdjustSensitivity (float newSensitivity) {
		sensitivity = newSensitivity;
		Csensitivity = newSensitivity;
	}
}
