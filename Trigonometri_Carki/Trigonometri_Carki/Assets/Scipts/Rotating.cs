using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotating : MonoBehaviour {

	const int CemberCap = 500;
	private Transform trans;
	private float Lx, Ly;
	private int rot;
	private bool Downed;

	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Downed){
			rot = Rotation (Lx,Ly,Input.mousePosition.x, Input.mousePosition.y);
			trans.Rotate (0, 0, rot);
			Lx = Input.mousePosition.x;
			Ly = Input.mousePosition.y;
			Debug.Log (rot);
		}
	}


	void OnMouseDown(){
		Lx = Input.mousePosition.x;
		Ly = Input.mousePosition.y;
		Downed = true;
	}
	void OnMouseUp(){
		Downed = false;
	}

	private int Rotation(float lx,float ly,float x,float y){
		float h = y - ly;
		float hr = h / CemberCap * 180 * trans.localScale.y;
		float w = x - lx;
		float wr = w / CemberCap * 180 * trans.localScale.x;
		float f = hr - wr; 
		return (int)f;
	}
}
