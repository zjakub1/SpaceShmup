using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [SerializeField] float gameOverDelay = 2f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        Debug.Log("Starting Wait and load");
        // the coroutine is called, this starts a timer
        // the originating method is returned to
        yield return new WaitForSeconds(gameOverDelay);
        Debug.Log("Finished 2 second delay");
        SceneManager.LoadScene(2);
        Debug.Log("finished loading scene");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
