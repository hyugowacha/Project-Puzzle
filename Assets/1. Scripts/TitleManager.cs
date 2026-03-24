using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void OnStartGame()
    {
        SceneManager.LoadScene("IngameScene");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
