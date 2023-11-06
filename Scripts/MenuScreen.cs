using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public static void StartEasyDifficulty()
    {
        SceneManager.LoadScene("EasyScene");
    }

    public static void StartMediumDifficulty()
    {
        SceneManager.LoadScene("MediumScene");
    }

    public static void StartHardDifficulty()
    {
        SceneManager.LoadScene("HardScene");
    }
}