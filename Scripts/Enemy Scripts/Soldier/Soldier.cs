using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {

	//Get Soldier
	public GameObject SoldierGO;

	//Damage
	public void TakeDamage (float Damage) {

		//GetComponent of current Soldier
		SoldierGO.GetComponent<SoldierHealth> ().SoldierHealthTotal -= Damage;

	}

}
