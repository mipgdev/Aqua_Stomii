using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public AudioClip sonidoVida;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.RecuperarVida();
            Destroy(this.gameObject);
            AudioManager.Instance.ReproducirSonido(sonidoVida);
        }
    }
}
