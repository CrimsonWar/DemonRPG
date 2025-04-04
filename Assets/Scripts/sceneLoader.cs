using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{

    public void loadOverworld()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void loadBattle()
    {
        SceneManager.LoadScene("Battle");
    }
}
