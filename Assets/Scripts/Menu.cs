using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public void Start()
    {
        
    }

    public void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Scene3");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
