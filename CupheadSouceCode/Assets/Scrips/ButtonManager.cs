using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void onResume()
    {
        GameManager.Instance.player.GetComponent<PlayerScript>().Pause(); 
    }

    public void onExit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
