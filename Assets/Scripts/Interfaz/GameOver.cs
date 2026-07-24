using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public LoadScene loadScene;
    public TMP_Text textPuntos;
    public TMP_Text textPuntosDIA;
    public TMP_Text textPuntosSemana;
    public TMP_Text textPuntosHistorico;
    public GameObject gameOverPanel;
    public GameObject jugador;

    public void MostrarGameOver(){
        Time.timeScale=0f;
        gameOverPanel.SetActive(true);
        jugador.SetActive(false);
        textPuntos.text=(("")+FindAnyObjectByType<GameManager>().PuntosTotales).ToString();
        //Jamal DB:
        textPuntosDIA.text = SQLiteDB.instance.ObtenerMaximoPuntajeUltimas24Horas().ToString();
        textPuntosSemana.text = SQLiteDB.instance.ObtenerMaximoPuntajeUltimaSemana().ToString();
        textPuntosHistorico.text = SQLiteDB.instance.ObtenerMaximoPuntaje().ToString();
    }
   
    public void ReiniciarJuego(){
        Time.timeScale = 1f; // Reanuda el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuPrincipal(){
        Time.timeScale = 1f;
        loadScene.LoadNextScene(0);
    }
}
