using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //private static SceneLoader _instance;
    //public static SceneLoader Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //            Debug.Log("Init you Singelton instance ");
    //        return _instance;
    //    }
    //}

    //private void Awake()
    //{
    //    _instance = this;
    //}
    
    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
