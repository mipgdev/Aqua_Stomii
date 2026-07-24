using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnMines : MonoBehaviour
{
    public Transform[] puntos;
    public GameObject[] minas;
    public bool activar;

    private Dificultad dificultad;
    private float minX, maxX, minY, maxY, timeNex=0;

    // Start is called before the first frame update
    void Start()
    {
        dificultad = GetComponent<Dificultad>();
        maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxY = puntos.Max(punto => punto.position.y);
        minY = puntos.Min(punto => punto.position.y);
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
                CrearEnemigo();
            }
        }
    }

    private void CrearEnemigo ()
    {
        int num = Random.Range(0, minas.Length);
        Vector2 ramdonPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        Instantiate(minas[num], ramdonPosition, Quaternion.identity);
    }
}
