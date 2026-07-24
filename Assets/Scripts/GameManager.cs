using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public HUD hud;
    public double maxVidas;
    public int timePuntos;

    private double vidas = 1;
    private int indVidas = 2;

    public int PuntosTotales { get; private set; }
    public static GameManager Instance { get; private set; }

    void Start() {
        InvokeRepeating("SegundosPuntos", timePuntos, timePuntos);
    }

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.Log("Cuidado! Hay más de un GameManager en escena");
        }
    }

    public void SumarPuntos(int puntosASumar)
    {
        PuntosTotales += puntosASumar;
        hud.ActualizarPuntos(PuntosTotales);
    }

    void SegundosPuntos()
    {
        PuntosTotales ++;
        hud.ActualizarPuntos(PuntosTotales);
    }

    public void PerderVida()
    {
        vidas -= 0.5;
        maxVidas -= 0.5;
        if (maxVidas == 0.0)
        {
            player.GetComponent<PlayerController>().Die();
            Invoke("Reiniciar", 1f);
        }
        hud.DesactivarVida(indVidas,vidas);
        if (vidas == 0.0)
        {
            vidas = 1.0;
            indVidas -= 1;
            if (indVidas == -1){
                indVidas = 0;
                vidas = 0.5;
            }
        }
    }

    public void RecuperarVida()
    {
        if (maxVidas == 3.0)
        {
            return;
        }

        maxVidas += 0.5;

        if (vidas == 1.0)
        {
            vidas = 0.5;
            indVidas += 1;
        }else
        {
            vidas += 0.50;
        }
        hud.ActivarVida(indVidas,vidas);
    }

     void Reiniciar()
    {
        //Jamal DB:
        SQLiteDB.instance.GuardarPuntuacion(PuntosTotales);
        // Reiniciar nivel
        FindAnyObjectByType<GameOver>().MostrarGameOver();
        // SceneManager.LoadScene(0);
    }
}
