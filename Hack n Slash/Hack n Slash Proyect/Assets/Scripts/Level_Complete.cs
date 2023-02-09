using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Complete : MonoBehaviour
{
    public void LoadNextLevel()
    {
        FindObjectOfType<AudioManager>().Stop("DMC5");
        FindObjectOfType<AudioManager>().Play("DOOM");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
