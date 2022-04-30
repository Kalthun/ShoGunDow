using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour {

	//Get Chaser
	public GameObject ChaserGO;

	//Damage
	public void TakeDamage (float Damage) {

		//GetComponent of current Chaser
		ChaserGO.GetComponent<ChaserHealth> ().ChaserHealthTotal -= Damage;

	}

}
