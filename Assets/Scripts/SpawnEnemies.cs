using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnEnemies : MonoBehaviour
{
    public Transform[] puntos;
    public GameObject[] enemigos;
    public bool activar;
    public float distanciaMaxima;

    private Dificultad dificultad;
    private float minX, maxX, minY, maxY, timeNex=0, antpos;
    private int numEnem = 1, toleranciaEnem = 1;
    private Transform player;
    private List<GameObject> enemigosActivos = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        dificultad = GetComponent<Dificultad>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxY = puntos.Max(punto => punto.position.y);
        minY = puntos.Min(punto => punto.position.y);
        antpos = maxY;
    }

    // Update is called once per frame
    void Update()
    {
        maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxY = puntos.Max(punto => punto.position.y);
        minY = puntos.Min(punto => punto.position.y);

        timeNex += Time.deltaTime;

        if (timeNex >= dificultad.time)
        {
            timeNex = 0;
            if (activar){
                VerificarPosicion();
                if (numEnem <= toleranciaEnem)
                {
                    CrearEnemigo();
                }
                antpos = maxY;
            }
        }

        EliminarEnemigosLejanos();
    }

    private void CrearEnemigo ()
    {
        int num = Random.Range(0, enemigos.Length);
        Vector2 ramdonPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        GameObject enemigo = Instantiate(enemigos[num], ramdonPosition, Quaternion.identity);
        enemigosActivos.Add(enemigo);
    }

    private void EliminarEnemigosLejanos()
    {
        for (int i = enemigosActivos.Count - 1; i >= 0; i--)
        {
            if (enemigosActivos[i].transform.position.y < player.position.y && 
                Vector2.Distance(player.position, enemigosActivos[i].transform.position) > distanciaMaxima)
            {
                Destroy(enemigosActivos[i]); // Destruye el enemigo
                enemigosActivos.RemoveAt(i); // Lo elimina de la lista
            }
        }
    }

    private void VerificarPosicion()
    {
        numEnem = (antpos == maxY) ? numEnem + 1 : 1;
    }
}
