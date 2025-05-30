using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject pauseUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseUI.activeSelf == true)
            {
                pauseUI.SetActive(false);
            }

            else
            {
                pauseUI.SetActive(true);
            }
        }
    }

    public void ChangeScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
