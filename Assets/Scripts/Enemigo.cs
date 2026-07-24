using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float velocidad;
    public float fuerzaAtaque;
    public bool PuedeMoverse { get; private set; }

    private void Start() {
        PuedeMoverse = true;
    }
    
    // Dar ataque
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
