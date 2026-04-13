
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturntoMenu : MonoBehaviour
{
    [SerializeField] private string level;

    public void loadLevel()
    {
        SceneManager.LoadScene(level);
    }
}
