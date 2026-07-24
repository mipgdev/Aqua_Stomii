using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;

public class Opciones : MonoBehaviour
{
    public GameObject opcPanel;
    public GameObject opcBoton;
    public GameObject punPanel;
    public GameObject punBoton;
    public GameObject punVolver;
    public TMP_Text textPuntosUltima;
    public TMP_Text textPuntosDIA;
    public TMP_Text textPuntosSemana;
    public TMP_Text textPuntosHistorico;
    public GameObject insPanel;
    public GameObject insBoton;
    public GameObject insVolver;
    public GameObject titulo;
    public AudioSource musica;
    public AudioSource audios;
    public GameObject audioMute;
    public GameObject audioNoMute;
    public GameObject musicaMute;
    public GameObject musicaNoMute;
    public static bool opc = false, ins = false, pun = false;

    void Start()
    {
        audioMute.SetActive(PlayerPrefs.GetInt("AudioMute", 0) == 0);
        audioNoMute.SetActive(PlayerPrefs.GetInt("AudioMute", 0) == 1);
        musicaMute.SetActive(PlayerPrefs.GetInt("MusicaMute", 0) == 0);
        musicaNoMute.SetActive(PlayerPrefs.GetInt("MusicaMute", 0) == 1);
    }

    // Boton Opciones
    public void Opcion(){
        opcPanel.SetActive(true);
        opcBoton.SetActive(false);
        if (ins) {insVolver.SetActive(false);}
        else if (pun) {punVolver.SetActive(false);}
        opc = true;
    }

    public void NoOpcion()
    {
        opcBoton.SetActive(true);
        opcPanel.SetActive(false);
        if (ins) {insVolver.SetActive(true);}
        else if (pun) {punVolver.SetActive(true);}
        opc = false;
    }

    // Boton Puntaciones
    public void Puntaciones(){
        //Jamal DB:
        textPuntosUltima.text = SQLiteDB.instance.ObtenerPuntajeMasReciente().ToString();
        textPuntosDIA.text = SQLiteDB.instance.ObtenerMaximoPuntajeUltimas24Horas().ToString();
        textPuntosSemana.text = SQLiteDB.instance.ObtenerMaximoPuntajeUltimaSemana().ToString();
        textPuntosHistorico.text = SQLiteDB.instance.ObtenerMaximoPuntaje().ToString();

        punPanel.SetActive(true);
        punBoton.SetActive(false);
        titulo.SetActive(false);
        pun = true;
    }

    public void NoPuntaciones()
    {
        punBoton.SetActive(true);
        titulo.SetActive(true);
        punPanel.SetActive(false);
        pun = false;
    }

    // Boton Instrucciones
    public void Instrucciones(){
        insPanel.SetActive(true);
        insBoton.SetActive(false);
        titulo.SetActive(false);
        ins = true;
    }

    public void NoInstrucciones()
    {
        insBoton.SetActive(true);
        titulo.SetActive(true);
        insPanel.SetActive(false);
        ins = false;
    }

    // Boton Mute
    public void MuteActivar(){
        PlayerPrefs.SetInt("AudioMute", 1);
        PlayerPrefs.Save();
        audios.mute = true;
        audioNoMute.SetActive(true);
        audioMute.SetActive(false);
    }

    public void MuteDesactivar(){
        PlayerPrefs.SetInt("AudioMute", 0);
        PlayerPrefs.Save();
        audios.mute = false;
        audioMute.SetActive(true);
        audioNoMute.SetActive(false);
    }

    public void MusicaActivar(){
        PlayerPrefs.SetInt("MusicaMute", 1);
        PlayerPrefs.Save();
        musica.mute = true; // Pausa la música
        musicaNoMute.SetActive(true);
        musicaMute.SetActive(false);
    }

    public void MusicaDesactivar(){
        PlayerPrefs.SetInt("MusicaMute", 0);
        PlayerPrefs.Save();
        musica.mute = false; // Pausa la música
        musicaMute.SetActive(true);
        musicaNoMute.SetActive(false);
    }
}
