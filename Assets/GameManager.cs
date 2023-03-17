using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject winPanel;
    public GameObject losePanel;

    public void Lose()
    {
        losePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Win()
    {
        winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void AgainButton()
    {
        SceneManager.LoadScene("Mummy");
        
    }

    public void QuitButton()
    {
        Application.Quit();

    }

    // Update is called once per frame
    void Update()
    {

    }

}
