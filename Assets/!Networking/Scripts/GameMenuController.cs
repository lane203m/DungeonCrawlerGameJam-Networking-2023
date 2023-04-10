using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void ReturnToMain()
    {
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(0);
    }
    public void WinScreen()
    {
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(2);
    }
    public void FailScreen()
    {
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(3);
    }
}
