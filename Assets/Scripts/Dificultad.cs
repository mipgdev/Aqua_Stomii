using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dificultad : MonoBehaviour
{
    public float time, minTime, distanciaSubida;

    private float nextY;
    private Transform player;

    private 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nextY = player.position.y + distanciaSubida;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y >= nextY)
        {
            if(time > minTime){
                time--;
            }
            nextY += distanciaSubida;
        }
    }
}
