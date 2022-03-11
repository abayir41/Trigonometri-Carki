using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {

	public GameObject Cark,CarkCanvas,Menu,MiniTest,CarkDonmeyeri;
	private bool menumod;
	public string[] Sorular;
	public string[] Cevaplar;
	public float sure;
	public int dorucevap,kacincisoru,bilinensoru;
	public bool sorubool;
	public Image bir, iki,sliderrenk;
	public Button birb,ikib;
	public Color kirmizi,yesil,normal;
	public Text Sorutext, birtext, ikitext,basaritext,suretext,sonuc,sorusayisi;
	public Slider Basarislid;
	public Color kotu, orta, iyi;
	public GameObject Hazirtusu,birg,ikig,sonucg,sureg,sorug,sorusayisig,geributon;

	// Use this for initialization
	void Start () {
		Cark.SetActive (true);
		CarkCanvas.SetActive (false);
		Menu.SetActive (true);
		MiniTest.SetActive (false);
		menumod = true;
		kacincisoru = 1;
		basari ();
	}
	
	// Update is called once per frame
	void Update () {
		if (menumod){
			CarkDonmeyeri.transform.Rotate (0,0,10 * Time.deltaTime);
		}
		if (sorubool){
			sure -= Time.deltaTime;
			suretext.text = ((int)sure).ToString();
			if (sure < 0){
				StartCoroutine(Sorucevaplandi ());
			}
		}

	}

	public void Carkbut(){
		Cark.SetActive (true);
		CarkCanvas.SetActive (true);
		Menu.SetActive (false);
		MiniTest.SetActive (false);
		menumod = false;
		CarkDonmeyeri.transform.eulerAngles = Vector3.zero;
	}
	public void Test(){
		Cark.SetActive (false);
		CarkCanvas.SetActive (false);
		Menu.SetActive (false);
		MiniTest.SetActive (true);
		menumod = false;
	}
	public void Geri(){
		Cark.SetActive (true);
		CarkCanvas.SetActive (false);
		Menu.SetActive (true);
		MiniTest.SetActive (false);
		menumod = true;
	}

	public void Hazir(){
		Soruhazirla ();
		sure = 6.00f;
		sorubool = true;
		Hazirtusu.SetActive (false);
		birg.SetActive (true);
		ikig.SetActive (true);
		sorug.SetActive(true);
		sureg.SetActive (true);
		sorusayisig.SetActive (true);
		geributon.SetActive (false);
	}

	public IEnumerator Sorucevaplandi(){
		sorubool = false;
		sure = 6.00f;
		suretext.text = "";
		if (kacincisoru == 11) {
			cevap ();
			birb.interactable = false;
			ikib.interactable = false;
			PlayerPrefs.SetInt ("oncebilinen", bilinensoru);
			sonucg.SetActive (true);
			basari ();
			yield return new WaitForSeconds (2.5f);
			sonucg.SetActive (false);
			kacincisoru = 1;
			Hazirtusu.SetActive (true);
			sorusayisig.SetActive (false);
			birg.SetActive (false);
			ikig.SetActive (false);
			geributon.SetActive (true);
			sorug.SetActive(false);
			sureg.SetActive (false);
			birb.interactable = true;
			ikib.interactable = true;
			bilinensoru = 0;
			//menu
		} else {
			cevap();
			birb.interactable = false;
			ikib.interactable = false;
			yield return new WaitForSeconds (1);

			birb.interactable = true;
			ikib.interactable = true;
			Soruhazirla ();
		}
	}

	int tempx = 0;
	public void Soruhazirla(){
		kacincisoru++;
		sorusayisi.text = "Soru: "+(kacincisoru - 1);
		bir.color = normal;
		iki.color = normal;
		int x = Random.Range (0, Sorular.Length);
		while (x == tempx) {
			 x = Random.Range (0, Sorular.Length);
		}
		tempx = x;
		int y = Random.Range (0,Sorular.Length);
		while (Cevaplar[y] == Cevaplar[x]){
			 y = Random.Range (0,Sorular.Length);
		}
		int i = Random.Range (0, 10);
		Sorutext.text = Sorular [x];
		if (i <= 5) {
			dorucevap = 1;
			birtext.text = Cevaplar [x];
			ikitext.text = Cevaplar [y];
		} else {
			dorucevap = 2;
			birtext.text = Cevaplar [y];
			ikitext.text = Cevaplar [x];
		}
		sorubool = true;
	}

	void cevap(){
		if (dorucevap == 1) {
			bir.color = yesil;
			iki.color = kirmizi;
		} else {
			bir.color = kirmizi;
			iki.color = yesil;
		}
	}

	public void birincibuton(){
		if (dorucevap == 1) {
			bilinensoru++;
		} 
			StartCoroutine (Sorucevaplandi());

	}
	public void ikincibuton(){
		if (dorucevap == 2) {
			bilinensoru++;
		}
			StartCoroutine (Sorucevaplandi());
	}

	void basari(){
		Basarislid.value = PlayerPrefs.GetInt ("oncebilinen") * 10;
		sliderrenk.color = basaricolor ();
		sonuc.text = "%" + PlayerPrefs.GetInt ("oncebilinen") * 10 + " Başarı";
		sonuc.color = basaricolor ();
		basaritext.text = "%" + PlayerPrefs.GetInt ("oncebilinen") * 10 + " Başarı";
	}

	Color basaricolor(){
		if (PlayerPrefs.GetInt ("oncebilinen") * 10 > 80) {
			return iyi;
		} else if (PlayerPrefs.GetInt ("oncebilinen") * 10 < 50) {
			return kotu;
		} else {
			return orta;
		}
	}

}
