using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerCH : MonoBehaviour
{
    public void onResume()
    {
        GameManagerCH.instance.cuphead.GetComponent<CupheadScript>().Pause();
    }

    public void onExit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void onRetry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
