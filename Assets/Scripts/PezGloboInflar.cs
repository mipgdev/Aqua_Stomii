using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PezGloboInflar : MonoBehaviour
{
    public PezGloboController pez;

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.CompareTag("Player"))
        {
            pez.Inflar();
        }
    }
}
