using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnObjetos : MonoBehaviour
{
    public Transform[] puntos;
    public GameObject[] objetos;
    [Range(0f, 100f)] public float[] probabilidades;
    public int time;
    public bool activar;
    public float distanciaMaxima;

    private float minX, maxX, minY, maxY, timeNex=0, antposY, antposX;
    private int numEnem = 1, toleranciaEnem = 2;
    private Transform player;
    private List<GameObject> objetosActivos = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxY = puntos.Max(punto => punto.position.y);
        minY = puntos.Min(punto => punto.position.y);
        antposY = maxY;
        antposX = 1000;

        // Normalizar probabilidades si no suman 1
        float suma = probabilidades.Sum();
        if (suma != 1f)
        {
            for (int i = 0; i < probabilidades.Length; i++)
            {
                probabilidades[i] /= suma;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxY = puntos.Max(punto => punto.position.y);
        minY = puntos.Min(punto => punto.position.y);

        timeNex += Time.deltaTime;

        if (timeNex >= time)
        {
            timeNex = 0;
            if (activar){
                VerificarPosicion();
                if (numEnem <= toleranciaEnem)
                {
                    CrearEnemigo();
                }
                antposY = maxY;
            }
        }

        EliminarObjetosLejanos();
    }

    private void CrearEnemigo ()
    {
        int num = ElegirObjeto();
        float posX;
        do
        {
            posX = Random.Range(minX, maxX);
        } while (posX == antposX);
        antposX = posX;

        Vector2 ramdonPosition = new Vector2(posX, Random.Range(minY, maxY));
        GameObject objeto = Instantiate(objetos[num], ramdonPosition, Quaternion.identity);
        objetosActivos.Add(objeto);
    }

     private int ElegirObjeto()
    {
        float valorAleatorio = Random.value; // Número entre 0 y 1
        float acumulado = 0f;

        for (int i = 0; i < probabilidades.Length; i++)
        {
            acumulado += probabilidades[i];
            if (valorAleatorio <= acumulado)
            {
                return i;
            }
        }
        return objetos.Length - 1; // Retorno de seguridad
    }

    private void VerificarPosicion()
    {
        numEnem = (antposY == maxY) ? numEnem + 1 : 1;
    }

    private void EliminarObjetosLejanos()
    {
        for (int i = objetosActivos.Count - 1; i >= 0; i--)
        {
            // Verificamos si el objeto está debajo del jugador y demasiado lejos
            if (objetosActivos[i] != null && objetosActivos[i].transform.position.y < player.position.y &&
                Vector2.Distance(player.position, objetosActivos[i].transform.position) > distanciaMaxima)
            {
                Destroy(objetosActivos[i]); // Destruir objeto
                objetosActivos.RemoveAt(i); // Eliminar de la lista 
            }
        }
    }
}
