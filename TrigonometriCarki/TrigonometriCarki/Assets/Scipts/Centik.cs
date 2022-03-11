using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centik : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag =="Cot"){
			GetComponent<SpriteRenderer> ().enabled = false;
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag =="Cot"){
			GetComponent<SpriteRenderer> ().enabled = true;
		}
	}
}
