using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Start()
    {
        if (Instance = null)
            Instance = this;
        else
            Destroy(Instance);
    }

    public void StartGame()
    {
        EventManager.Instance.StartGame();
    }

    public void EndGame()
    {
        GameSettings.Instance.SaveProgress();
    }

    public void RestartGame()
    {
        GameSettings.Instance.SaveProgress();

        AdTimer.Instance.ShoAd();

        SceneManager.LoadScene(0);
    }
}
