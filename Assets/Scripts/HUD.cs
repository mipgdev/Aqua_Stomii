using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Image = UnityEngine.UI.Image;

public class HUD : MonoBehaviour
{
    
    public TextMeshProUGUI Mejor;
    public TextMeshProUGUI puntos;
    public Sprite Vida;
    public Sprite mitadVida;
    public Sprite ceroVida;
    public GameObject[] vidas;

    // Update is called once per frame
    void Update()
    {
        puntos.text = GameManager.Instance.PuntosTotales.ToString();
    }

     // Añade este método Start
    void Start()
    {
        ActualizaMejor();
    }

    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();
    }
    
    public void ActualizaMejor()
    {
        Mejor.text = SQLiteDB.instance.ObtenerMaximoPuntaje().ToString();
    }

    public void DesactivarVida(int indice, double vida)
    {
        if (vida == 0.5)
        {   
            vidas[indice].GetComponent<Image>().sprite = mitadVida;
        } else
        {
            vidas[indice].GetComponent<Image>().sprite = ceroVida;
        }
    }

    public void ActivarVida(int indice, double vida)
    {
        if (vida == 1)
        {   
            vidas[indice].GetComponent<Image>().sprite = Vida;
        } else
        {
            vidas[indice].GetComponent<Image>().sprite = mitadVida;
        }
    }
}
