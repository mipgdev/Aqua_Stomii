using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public Transform Transform { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.Log("Cuidado! Hay más de un Player en escena.");
        }
    }

    private void Update() {
        Transform = transform;
    }
}
