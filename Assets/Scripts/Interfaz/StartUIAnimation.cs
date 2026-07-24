using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartUIAnimation : MonoBehaviour
{
    public GameObject[] animaciones;
    
    public void StartAnim ()
    {
        if (animaciones != null){
            foreach (var anim in animaciones)
            {
                anim.GetComponent<UIAnimacion>().StartAnimacion();
            }
        }
    }
}
