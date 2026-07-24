using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PezGloboController : MonoBehaviour
{    
    public float fuerzaAtaque;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Dar ataque
    private void OnCollisionEnter2D(Collision2D other) {
        if (IsInState("inflado"))
        {
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
        } else
        {
            StartCoroutine(ResetCollision(GetComponent<Collider2D>()));
        }
    }

    IEnumerator ResetCollision(Collider2D other)
    {
        other.enabled = false;
        yield return new WaitForFixedUpdate();
        other.enabled = true;
    }

    // Verifica si el Animator está en un estado específico
    private bool IsInState(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    public void Inflar()
    {
        animator.SetBool("Inflar", true);
    }

}
    
    
