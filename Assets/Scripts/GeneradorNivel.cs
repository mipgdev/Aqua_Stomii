using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorNivel : MonoBehaviour
{
    public GameObject[] partesNivel;
    public float distancia, lejos;
    public Transform puntoFinal;
    public int cantidadInicial;

    private Transform player;
    private List<GameObject> partesActivas = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < cantidadInicial; i++)
        {
            GenerarParte();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.position, puntoFinal.position) < distancia)
        {
            GenerarParte();
        }

        EliminarPartesLejanas();
    }

    private void GenerarParte() {
        int num = Random.Range(0, partesNivel.Length);
        GameObject nivel = Instantiate(partesNivel[num], puntoFinal.position, Quaternion.identity);

        partesActivas.Add(nivel);
        puntoFinal = BuscarPunto(nivel, "PuntoFinal");
    }

    private Transform BuscarPunto (GameObject parteNivel, string tag) {
        Transform punto = null;

        foreach (Transform ubi in parteNivel.transform)
        {
            if (ubi.CompareTag(tag))
            {
                punto = ubi;
                break;
            }
        }
        return punto;
    }

    private void EliminarPartesLejanas()
    {
        for (int i = partesActivas.Count - 1; i >= 0; i--)
        {
            if (Vector2.Distance(player.position, partesActivas[i].transform.position) > lejos)
            {
                Destroy(partesActivas[i]); // Destruye la parte del nivel
                partesActivas.RemoveAt(i); // La elimina de la lista
            }
        }
    }
}
