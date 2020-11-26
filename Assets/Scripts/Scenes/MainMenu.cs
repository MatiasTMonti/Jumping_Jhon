using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    static int mainScene = 0;

    [SerializeField] private AudioClip ClickSfx = null;

    public void ShopFunction()
    {
        SceneManager.LoadScene(1);
        AudioController.instance.PlaySound(ClickSfx);
    }

    public void PlayFunction()
    {
        SceneManager.LoadScene(2);
        AudioController.instance.PlaySound(ClickSfx);
    }

    public static void LoadMainScene()
    {
        SceneManager.LoadScene(mainScene);
    }

    public void SetVolume(float vol)
    {
        AudioController.instance.SetVolume(vol);
    }
}
