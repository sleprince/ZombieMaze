using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    public void NewGame()
    {
        SceneManager.LoadScene("MainScene");

    }

    public void ExitGame()
    {
        Application.Quit();

    }


}
