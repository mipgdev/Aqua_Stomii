using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;

public class Pause : MonoBehaviour
{
    // public TMP_Text textPuntos;
    public LoadScene loadScene;
    public GameObject pausaPanel;
    public GameObject pausaBoton;
    public GameObject volverBoton;
    public AudioSource musica;
    public AudioSource audios;
    public GameObject audioMute;
    public GameObject audioNoMute;
    
    public GameObject musicaMute;
    public GameObject musicaNoMute;
    public static bool pausa = false;

    void Start()
    {
        audioMute.SetActive(PlayerPrefs.GetInt("AudioMute", 0) == 0);
        audioNoMute.SetActive(PlayerPrefs.GetInt("AudioMute", 0) == 1);
        musicaMute.SetActive(PlayerPrefs.GetInt("MusicaMute", 0) == 0);
        musicaNoMute.SetActive(PlayerPrefs.GetInt("MusicaMute", 0) == 1);
    }

    public void Pausar(){
        Time.timeScale=0f;
        pausaPanel.SetActive(true);
        pausaBoton.SetActive(false);
        volverBoton.SetActive(false);
        pausa = true;
        // textPuntos.text=(("Puntos: ")+FindAnyObjectByType<GameManager>().PuntosTotales).ToString();
    }

    public void ResumeGame()
    {
        pausaBoton.SetActive(true);
        volverBoton.SetActive(true);
        pausaPanel.SetActive(false);
        pausa = false;
        Time.timeScale = 1f; // Reanuda el juego
    }

    public void Reiniciar(){
        Time.timeScale = 1f; // Reanuda el juego
        pausa = false;
        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    }
    
    public void MenuPrincipal(){
        pausa = false;
        Time.timeScale = 1f; // Reanuda el juego
        loadScene.LoadNextScene(0);
    }

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