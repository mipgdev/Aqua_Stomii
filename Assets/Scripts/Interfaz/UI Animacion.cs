using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimacion : MonoBehaviour
{
    public Image image;
    public List<Sprite> sprites;
    public float animSpeed;
    public bool inicioStar = false;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        if (inicioStar)
        {
            StartCoroutine(StartAnim());
        }
    }

    public void StartAnimacion(){
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        while(true)
        {
            yield return new WaitForSeconds(animSpeed);
            index++;
            if(index >= sprites.Count){
                index = 0;
                image.sprite = sprites[index];
            }
            else
            image.sprite = sprites[index];
        }
    }
}
