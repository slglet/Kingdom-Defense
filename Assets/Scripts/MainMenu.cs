using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game"); 
    }

    public void OpenOptions()
    {
        Debug.Log("Options button pressed - add options functionality here!");
        SceneManager.LoadScene("Options");
        // Could open a panel or a new scene later
    }

    public void OpenStory()
    {
        SceneManager.LoadScene("Story");

    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed.");
        Application.Quit(); // Will not quit in the editor
    }
}
