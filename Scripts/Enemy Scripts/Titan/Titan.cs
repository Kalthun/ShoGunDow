using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan : MonoBehaviour {

	//Get Titan
	public GameObject TitanGO;

	//Damage Titan
	public void TakeDamage (float Damage) {

		//GetComponent of current Titan
		TitanGO.GetComponent<TitanHealth> ().TitanHealthTotal -= Damage;

	}
}
