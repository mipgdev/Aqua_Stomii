using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {
	public float fuerzaAtaque;
    public AudioClip explosion;

	void Start () {
		AudioManager.Instance.ReproducirSonido(explosion);
	}

	private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>().EsVulnerable())
            {
                // Perder vida
                GameManager.Instance.PerderVida();

                // Aplicar golpe
                other.gameObject.GetComponent<PlayerController>().Hurt(fuerzaAtaque, transform.position);
            }
        }
    }
}
