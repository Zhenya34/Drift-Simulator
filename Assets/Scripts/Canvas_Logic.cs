using PathCreation;
using PathCreation.Examples;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas_Logic : MonoBehaviour
{
    [SerializeField] private GameObject _canvasCamera;
    [SerializeField] private GameObject _carCamera;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GeneratePathExample _generatePathExample;

    private void Start()
    {
        _buttons.SetActive(true);
        _canvasCamera.SetActive(true);
        _carCamera.SetActive(false);
        _backButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        _buttons.SetActive(false);
        _canvasCamera.SetActive(false);
        _carCamera.SetActive(true);
        _backButton.SetActive(true);
        Time.timeScale = 1;
        _generatePathExample.GenerateNewPath();
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
    }
}
