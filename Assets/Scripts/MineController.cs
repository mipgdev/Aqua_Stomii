using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour {
	
	public AudioClip fall;
	public GameObject explosion;

	void Start () {
		AudioManager.Instance.ReproducirSonido(fall);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" || other.tag == "DedLine"){
			Destroy (gameObject);
			Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation);
		}	
	}
}
