using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float tiempoVulnerable;
    public float moveSpeed = 7f; 
    public float horizontalSpeed = 7f;
    public AudioClip sonidoHurt;

    private Rigidbody2D rigiBody;
    // private BoxCollider2D boxCollider;
    private Animator animator;
    private Vector2 movement;
    private bool puedeMoverse = true;
    private bool estaVivo = true;
    private float tiempoUltimaVez = -Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        rigiBody = GetComponent<Rigidbody2D>();
        // boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento vertical constante hacia arriba
        if (puedeMoverse){
            rigiBody.velocity = new Vector2(rigiBody.velocity.x, moveSpeed);
        }

        // Movimiento horizontal
        rigiBody.velocity = new Vector2(movement.x * horizontalSpeed, rigiBody.velocity.y);
    }

    public void MoveRight() 
    {
        if(!estaVivo){return;}
        movement.x = 1;
        // spriteRenderer.flipX = false; // No voltear (mirar a la derecha)
    }

    public void MoveLeft() 
    {
        if(!estaVivo){return;}
        movement.x = -1;
        // spriteRenderer.flipX = true; // Voltear en X (mirar a la izquierda)
    }

    public void StopMoving() => movement.x = 0;

    public bool EsVulnerable()
    {
        if (Time.time - tiempoUltimaVez >= tiempoVulnerable)
        {
            tiempoUltimaVez = Time.time;
            return true;
        }

        return false;
    }

    public void Hurt(float fuerzaGolpe, Vector2 collision)
    {
        puedeMoverse = false;
        
        if(estaVivo){
            animator.SetTrigger("hurt");
            AudioManager.Instance.ReproducirSonido(sonidoHurt);
        }

        Vector2 direccionGolpe = (transform.position - (Vector3)collision).normalized;

        if (Mathf.Abs(direccionGolpe.y) < 0.1f){
            direccionGolpe = Vector2.down;
        }
        
        rigiBody.AddForce(direccionGolpe * fuerzaGolpe, ForceMode2D.Impulse);
        StartCoroutine(EsperarYActivarMovimiento());
    }

    IEnumerator EsperarYActivarMovimiento()
    {
        // Esperar antes de comprobar si está en el suelo
        yield return new WaitForSeconds(0.5f);
        puedeMoverse = true;
    }

    public void Die(){
        puedeMoverse = false;
        estaVivo = false;
        animator.SetBool("isDeath", true);
    }

    public void Stop (){ 
        puedeMoverse = false;
        StartCoroutine(EsperarYDetener());
    }

    IEnumerator EsperarYDetener()
    {
        // Esperar antes de comprobar si está en el suelo
        yield return new WaitForSeconds(0.5f);
        rigiBody.velocity = new Vector2(0, 0);
    }
}
