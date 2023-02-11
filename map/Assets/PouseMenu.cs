using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PouseMenu : MonoBehaviour
{
    public static bool gameIsPoused = false;
    public GameObject pousedMenuUI;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;

            if (gameIsPoused)
            {
                Resume();
            }
            else
            {
                Pouse();
            }
        }

    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pousedMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPoused = false;
    }

    public void Pouse()
    {
        
        pousedMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPoused= true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
