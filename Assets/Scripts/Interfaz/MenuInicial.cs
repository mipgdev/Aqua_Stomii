using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour
{
    public LoadScene loadScene;

    public void Jugar(){
        loadScene.LoadNextScene(1);
    }

    public void Salir(){
        Debug.Log("Saliendo del juego...");
        
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Detiene el modo play en el editor
        #else
        Application.Quit(); // Cierra la aplicación en una compilación
        #endif
    }
    
    public void VolverMenu(){
        SceneManager.LoadScene(0);
    }
    
}