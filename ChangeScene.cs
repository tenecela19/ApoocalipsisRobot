using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public float timeChangeScene = 1f;
    public void Scene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void WaitForSceneToInvoke(string name) { 
        StartCoroutine(InvokeScene(name));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
     IEnumerator InvokeScene(string name)
    {
        yield return new WaitForSeconds(timeChangeScene);
        SceneManager.LoadScene(name);
    }
}
