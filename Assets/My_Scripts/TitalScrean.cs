using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitalSrean : MonoBehaviour
{
    [SerializeField] private AudioSource audioo;
    [SerializeField] private string start;

    [SerializeField] private Image startButton;
    [SerializeField] private Image exitButton;


    void Start()
    {
        audioo.Play();
        startButton.color = Color.green;
        exitButton.color = Color.green;
    }
    public void startExperiment()
    {
        SceneManager.LoadScene(start);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void OnHoverEnter(Image button)
    {
        button.color = Color.red;
    }
}
