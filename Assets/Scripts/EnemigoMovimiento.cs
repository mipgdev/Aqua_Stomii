using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemigoMovimiento : MonoBehaviour
{
    public LayerMask capaSuelo;
    public BoxCollider2D piCollider;
    public bool isPatrol, izqRay, derRay;
    public float patrolDistancia, visionDistance = 4f;
    [Range(0f, 360f)] public float visionAngle = 30f;

    public bool MirandoDerecha { get; private set; }

    private float speed, pointA, pointB, timer = 0f, maxTimer= 0.3f;
    private Transform player;
    private Rigidbody2D rb;
    private PolygonCollider2D boxCollider;
    
    private bool detected = false, goToA = true, isWall, isColliding = false;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<Enemigo>().velocidad;
        boxCollider = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        MirandoDerecha = false;
        pointA = transform.position.x - patrolDistancia;
        pointB = transform.position.x + patrolDistancia;
    }

    // Update is called once per frame
    void Update()
    {
        isWall = HayPared();
        player = Player.Instance.Transform;
        detected = PlayerDetected();

        if (isWall)
        {
            Flip();
            if(isPatrol)
            {
                goToA = !goToA;
            }
        }

        if (isColliding){
            timer += Time.deltaTime;
            if (timer > maxTimer){
                Flip();
                timer = -100f;
            }
        }
    }

    private void FixedUpdate() {
        Movimiento();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;
            timer = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = false;
        }
    }

    bool HayPared()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(piCollider.bounds.center, new Vector2(piCollider.bounds.size.x, piCollider.bounds.size.y), 0f, Vector2.left, 0.2f, capaSuelo);
        if (raycastHit.collider == true)
        {
            return raycastHit.collider != null;
        } else
        {
            raycastHit = Physics2D.BoxCast(piCollider.bounds.center, new Vector2(piCollider.bounds.size.x, piCollider.bounds.size.y), 0f, Vector2.right, 0.2f, capaSuelo);
            return raycastHit.collider != null;
        }   
    }

    bool PlayerDetected()
    {
        bool detectado = false;

        if(player != null){
            // Calcular el origen del rayo en la mitad del eje Y
            Vector3 rayOrigin = GetRayOrigin();

            Vector2 playerVector = player.position - rayOrigin;

            // Determinar la dirección del personaje
            float directionMultiplier = transform.localScale.x >= 0 ? 1 : -1;

            Vector2 visionDirection;

            // Verificar el primer rayo si está habilitado
            if (izqRay)
            {
                visionDirection = transform.right * -directionMultiplier;

                if (Vector3.Angle(playerVector.normalized, visionDirection) < visionAngle * 0.5f &&
                    playerVector.magnitude < visionDistance)
                {
                    detectado = true;
                }
            }

            // Verificar el segundo rayo si está habilitado
            if (derRay)
            {
                visionDirection = transform.right * directionMultiplier;

                if (Vector3.Angle(playerVector.normalized, visionDirection) < visionAngle * 0.5f &&
                    playerVector.magnitude < visionDistance)
                {
                    detectado = true;
                }
            }
        }
        
        return detectado;
    }

    void Movimiento()
    {
        if(!GetComponent<Enemigo>().PuedeMoverse){return;}

        else if (isPatrol)
        {
            if (goToA)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                if((transform.position.x - pointA) < 0.2f)
                {
                    Flip();
                    goToA = false;
                }
            }else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                if((transform.position.x - pointB) > 0.2f)
                {
                    Flip();
                    goToA = true;
                }
            }

            FixedFlip();
        }

        else
        {   
            if (MirandoDerecha)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                
            } else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }

            FixedFlip();
        }
    }

    void Flip()
    {
        // Ejecutar codigo de volteado
        MirandoDerecha = !MirandoDerecha;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    void FixedFlip()
    {
        float movimiento = rb.velocity.x;
        // Si se cumple condicion
        if((MirandoDerecha && movimiento < 0)||(!MirandoDerecha && movimiento > 0))
        {
            // Ejecutar codigo de volteado
            MirandoDerecha = !MirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void OnDrawGizmos() {
        if (visionAngle <= 0f) return;

        float halfVisionAngle = visionAngle * 0.5f;

        Vector2 p1, p2, p3, p4;

        // Determinar la dirección del personaje
        float directionMultiplier = transform.localScale.x >= 0 ? 1 : -1;

        // Offset para mover la posición inicial al centro del eje Y
        Vector3 rayOrigin = GetRayOrigin();


        // Dibuja el primer rayo si está habilitado
        if (izqRay)
        {
            p1 = PointForAngle(halfVisionAngle, visionDistance) * -directionMultiplier;
            p2 = PointForAngle(-halfVisionAngle, visionDistance) * -directionMultiplier;

            Gizmos.color = detected ? Color.green : Color.red;
            Gizmos.DrawLine(rayOrigin, rayOrigin + (Vector3)p1);
            Gizmos.DrawLine(rayOrigin, rayOrigin + (Vector3)p2);
            Gizmos.DrawRay(rayOrigin, transform.right * visionDistance * -directionMultiplier);
        }

        // Dibuja el segundo rayo si está habilitado
        if (derRay)
        {
            p3 = PointForAngle(halfVisionAngle, visionDistance) * directionMultiplier;
            p4 = PointForAngle(-halfVisionAngle, visionDistance) * directionMultiplier;

            Gizmos.color = detected ? Color.green : Color.blue; // Diferente color para el segundo rayo
            Gizmos.DrawLine(rayOrigin, rayOrigin + (Vector3)p3);
            Gizmos.DrawLine(rayOrigin, rayOrigin + (Vector3)p4);
            Gizmos.DrawRay(rayOrigin, transform.right * visionDistance * directionMultiplier);
        }
    }

    Vector3 PointForAngle(float angle, float distance)
    {
        return transform.TransformDirection(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * distance);
    }

    private Vector3 GetRayOrigin()
    {
        if (boxCollider != null)
        {
            return boxCollider.bounds.center; // Usa el centro del boxCollider
        }

        // Si no hay un collider, usa la posición con un offset manual
        return transform.position + Vector3.up * (transform.localScale.y * 0.5f);
    }
}
