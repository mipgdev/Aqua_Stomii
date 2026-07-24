using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private float seg = 1f;
    private Animator anima;

    void Start() {
        anima = GetComponentInChildren<Animator>();
    }

    public void LoadNextScene(int num){
        StartCoroutine(SceneLoad(num));
    }

    public IEnumerator SceneLoad (int index){
        anima.SetTrigger("Start");
        yield return new WaitForSeconds(seg);
        SceneManager.LoadScene(index);
    }
}
