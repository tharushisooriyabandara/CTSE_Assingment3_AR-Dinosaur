using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLevel : MonoBehaviour
{
    private void Awake()
    {
       // Debug.Log("text");
    }
    
    public void PlayGame()
    {
        //Debug.Log("click");
        SceneManager.LoadSceneAsync(1);
    }
}
