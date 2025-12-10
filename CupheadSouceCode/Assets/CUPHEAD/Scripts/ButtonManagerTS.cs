using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerTS : MonoBehaviour
{
    public void onStart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }
}
