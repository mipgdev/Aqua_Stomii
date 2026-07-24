using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionEstatica : MonoBehaviour
{
    public float distancia;

    private float nextY;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nextY = player.position.y + distancia;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y >= nextY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + distancia, transform.position.z);
            nextY += distancia;
        }
    }
}
