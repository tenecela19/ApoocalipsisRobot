using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cinematics : MonoBehaviour
{
    public GameObject[] Imagenes;
   private int value =0 ;
    private int ImagenesaDesplazar;
    public  bool DisplayImage =true;
    // Use this for initialization
    private void Start()
    {
        ImagenesaDesplazar = Imagenes.Length;
        for (int i = 1; i < Imagenes.Length; i++)
        {
            Imagenes[i].SetActive(false);
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayImage = true;
            StartCoroutine(UpdateImagenes());
        }
        if(value == ImagenesaDesplazar)
        {
            SceneManager.LoadScene("Main");
        }
    }
    public IEnumerator UpdateImagenes()
    {
            value++;
        if (value < ImagenesaDesplazar)
        {
            Imagenes[value].SetActive(true);
            DisplayImage = false;
            yield break;
        }

      
    }
    
}
