using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cinematics : MonoBehaviour
{
    #region Singleton
    private static Cinematics _instance;
    public static Cinematics Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Cinematics>();
            }

            return _instance;
        }
    }
    #endregion
    public GameObject dialogueText;
    public GameObject[] Imagenes;
   public int value =0 ;
    public int ImagenesaDesplazar;
    public  bool DisplayImage =true;
    // Use this for initialization
    private void Start()
    {
        ImagenesaDesplazar = Imagenes.Length ;
    }
    public IEnumerator UpdateImagenes()
    {
            value++;
            Imagenes[value].SetActive(true);     
            DisplayImage = false;
            dialogueText.SetActive(true);
            yield break;
      
    }
}
