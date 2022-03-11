using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotating2 : MonoBehaviour {

	//Tıklama
	private bool Downed;

	//Açı bulmamız için 3 nokta
	public Vector3 P0, P1, P2;

	//Dödürme için baştaki rotasyonuna ekleme 
	public float Srot;

	//Rotasyon alma
	private Transform trans;
	public Transform sinrot;
	public Transform Mainobject;

	//Rotasyon etmek istediğimiz açı
	public int Rotint;

	private int[] Angles = { 0, 30, 45, 60, 90, 120, 135, 150, 180, 210, 225, 240, 270, 300, 315, 330,358,359};

	public SpriteRenderer[] centikler,sinusler;

	public GameObject circlepos;

	public Text sin, cos, tan, cot, sinn, cosn, tann, cotn;

	public string sins,coss,tans,cots,sinns,cosns,tanns,cotns;

	public float timer;
	void Start () {
		timer = 3f;



		//trans çekme
		trans = GetComponent<Transform> ();

		//her cihaz için optimize
		Mainobject.position = Camera.main.ScreenToWorldPoint (circlepos.transform.position);

		//cirlepos -10 a çekiyor 
		Mainobject.position += new Vector3 (0, 0, 10);

		//dairenin merkezini alma
		P0 = Camera.main.WorldToScreenPoint(Mainobject.position);

		//0 dan başlama
		secondstringcolumn(358);
		firststringcolumn (358);
		settexts ();
	}
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;
		if (timer < 0) {
			timer = 3f;
		}

		Rotate (Downed);

		for (int x = (int)trans.eulerAngles.z, i = 0;i < Angles.Length - 2;i++){
			if (x + 7 > Angles [i] && x - 7 < Angles [i]) {
				centikler [i].enabled = false;
			} else
				centikler [i].enabled = true;
		}
		for (int x = (int)sinrot.eulerAngles.z, i = 0;i < Angles.Length - 2;i++){
			if (x + 7 > Angles [i] && x - 7 < Angles [i]) {
				sinusler [i].enabled = true;
			} else
				sinusler [i].enabled = false;
		}
	}

	void OnMouseDown(){

		//başlangıç rot
		Srot = trans.eulerAngles.z;

		//tıklandığındaki nokta
		P1 = Input.mousePosition;

		//loop için
		Downed = true;
	}

	void OnMouseUp(){
		//unloop için
		Downed = false;

		//cache
		int transrot = (int)trans.eulerAngles.z;

		//int olmayan açıyı düzelme
		fixangle ();

		//oto döndür yada döndürme
		if (inAngles (transrot)) {
			//döndürme direk yaz
			secondstringcolumn(transrot);
			firststringcolumn (transrot);
			settexts ();
		} else {
			//döndür ve yaz
			StartCoroutine (rotatefornextangle(findnextbiggerangle(transrot)));
		}
	}

	//yazdığımız açılardanmı
	bool inAngles(int angle){
		bool b = false;
		for (int i = 0;i < Angles.Length;i++){
			if (angle == Angles [i]) {
				b = true;
			}
		}
		return b;
	}

	//dön
	void Rotate(bool b){
		if (b) {
			//anlık pos
			P2 = Input.mousePosition;

			//hesaplama
			Rotint = Rot (P0, P1, P2);

			//esas ölçü
			if (Rotint < 0){
				Rotint += 360;
			}

			//döndürme
			trans.eulerAngles = new Vector3 (0, 0, Rotint + Srot);
		}
	}

	//dönme açısı hesabı
	 int Rot(Vector3 p0,Vector3 p1,Vector3 p2){
		//cache
		float p2y = p2.y - p0.y;
		float p2x = p2.x - p0.x;
		float p1x = p1.x - p0.x;
		float p1y = p1.y - p0.y;


		//açıları hesapla
		float rot2 = Mathf.Rad2Deg * Mathf.Atan2 (p2y, p2x);
		float rot1 = Mathf.Rad2Deg * Mathf.Atan2 (p1y, p1x);

		//açı çıkarımı
		float rot = rot2 - rot1;

		//döüş değeri
		return (int)rot;
	}

	//kendisinden büyük bir sonraki yazdığımız açı bulma
	public int findnextbiggerangle(int j){
		int x = 358;
		for (int i = 0;i < Angles.Length;i++){
			if (Angles[i] > j){
				x = Angles [i];
				break;
			}
		}
		return x;
	}

	//sonraki açıya döndürme
	IEnumerator rotatefornextangle(int angle){
		while(trans.eulerAngles.z < angle){
			trans.eulerAngles += new Vector3 (0, 0, 1);
			yield return new WaitForSeconds (0.01f);
		}
		fixangle ();
		firststringcolumn (angle);
		secondstringcolumn (angle);
		settexts ();
	}

	void fixangle(){
		trans.eulerAngles = new Vector3 (0, 0, (int)trans.eulerAngles.z);
		if (trans.eulerAngles.z == 358){
			trans.eulerAngles += new Vector3 (0, 0, 2);
		}else if (trans.eulerAngles.z == 359){
			trans.eulerAngles += new Vector3 (0, 0, 1);
		}
	}

	void firststringcolumn(int angle){
		if (angle == 358) {
			sins = "sin(270°):";
			coss = "cos(180°):";
			tans = "tan(90°):";
			cots = "cot(0-360°):";
		} else if (angle == 90){
			sins = "sin(0-360°):";
			coss = "cos(270°):";
			tans = "tan(180°):";
			cots = "cot(90°):";
		} else if (angle == 0){
			sins = "sin(270°):";
			coss = "cos(180°):";
			tans = "tan(90°):";
			cots = "cot(0-360°):";
		} else if (angle == 359){
			sins = "sin(270°):";
			coss = "cos(180°):";
			tans = "tan(90°):";
			cots = "cot(0-360°):";
		}else if (angle == 180){
			sins = "sin(90°):";
			coss = "cos(0-360°):";
			tans = "tan(270°):";
			cots = "cot(180°):";
		}else if (angle == 270){
			sins = "sin(180°):";
			coss = "cos(90°):";
			tans = "tan(0-360°):";
			cots = "cot(270°):";
		}else {
			sins = "sin("+((angle + 270) % 360)+ "°):";
			coss = "cos("+((angle + 180) % 360)+ "°):";
			tans = "tan("+((angle + 90) % 360)+ "°):";
			cots = "cot("+angle+ "°):";
		}

	}


	void settexts(){
		sin.text = sins;
		cos.text = coss;
		tan.text = tans;
		cot.text = cots;
		sinn.text = sinns;
		cosn.text = cosns;
		tann.text = tanns;
		cotn.text = cotns;
	}


	void secondstringcolumn(int anglecot){
		if (anglecot == 30) {
			sinns = "-√3/2";
			cosns = "-√3/2";
			tanns = "-√3";
			cotns = "√3";
		}else if (anglecot == 45){
			sinns = "-√2/2";
			cosns = "-√2/2";
			tanns = "-1";
			cotns = "1";
		}else if (anglecot == 60){
			sinns = "-1/2";
			cosns = "-1/2";
			tanns = "-√3/3";
			cotns = "√3/3";
		}else if (anglecot == 90){
			sinns = "0";
			cosns = "0";
			tanns = "0";
			cotns = "0";
		}else if (anglecot == 120){
			sinns = "1/2";
			cosns = "1/2";
			tanns = "√3/3";
			cotns = "-√3/3";
		}else if (anglecot == 135){
			sinns = "√2/2";
			cosns = "√2/2";
			tanns = "1";
			cotns = "-1";
		}else if (anglecot == 150){
			sinns = "√3/2";
			cosns = "√3/2";
			tanns = "√3";
			cotns = "-√3";
		}else if (anglecot == 180){
			sinns = "1";
			cosns = "1";
			tanns = "Tanımsız";
			cotns = "Tanımsız";
		}else if (anglecot == 210){
			sinns = "√3/2";
			cosns = "√3/2";
			tanns = "-√3";
			cotns = "√3";
		}else if (anglecot == 225){
			sinns = "√2/2";
			cosns = "√2/2";
			tanns = "-1";
			cotns = "1";
		}else if (anglecot == 240){
			sinns = "1/2";
			cosns = "1/2";
			tanns = "-√3/3";
			cotns = "√3/3";
		}else if (anglecot == 270){
			sinns = "0";
			cosns = "0";
			tanns = "0";
			cotns = "0";
		}else if (anglecot == 300){
			sinns = "-1/2";
			cosns = "-1/2";
			tanns = "√3/3";
			cotns = "-√3/3";
		}else if (anglecot == 315){
			sinns = "-√2/2";
			cosns = "-√2/2";
			tanns = "1";
			cotns = "-1";
		}else if (anglecot == 330){
			sinns = "-√3/2";
			cosns = "-√3/2";
			tanns = "√3";
			cotns = "-√3";
		}else if (anglecot == 358){
			sinns = "-1";
			cosns = "-1";
			tanns = "Tanımsız";
			cotns = "Tanımsız";
		}else if (anglecot == 0){
			sinns = "-1";
			cosns = "-1";
			tanns = "Tanımsız";
			cotns = "Tanımsız";
		}else if (anglecot == 359){
			sinns = "-1";
			cosns = "-1";
			tanns = "Tanımsız";
			cotns = "Tanımsız";
		}
	}

	public void sifirlama(){
		secondstringcolumn(358);
		firststringcolumn (358);
		settexts ();
	}
}
