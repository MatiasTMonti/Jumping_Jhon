using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    static int mainScene = 0;

    public void ShopFunction()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayFunction()
    {
        SceneManager.LoadScene(2);
    }

    public static void LoadMainScene()
    {
        SceneManager.LoadScene(mainScene);
    }
}
