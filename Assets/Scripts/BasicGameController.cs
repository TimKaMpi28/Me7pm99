using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicGameController : MonoBehaviour
{

    public static void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void Credits()
    {

    }

    public static void SetObjectActive(GameObject obj)
    {
        bool active = obj.activeSelf;
        obj.SetActive(!active);
    }
}
