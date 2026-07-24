using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonSonido : MonoBehaviour
{
    public AudioClip sonidoClick;   // Sonido del botón

    void Start()
    {   
        GetComponent<Button>().onClick.AddListener(ReproducirSonido);
    }

    void ReproducirSonido()
    {
        AudioManager.Instance.ReproducirSonido(sonidoClick);
    }
}
