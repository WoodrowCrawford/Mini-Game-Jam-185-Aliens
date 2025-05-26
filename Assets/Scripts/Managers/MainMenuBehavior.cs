using UnityEngine;
using UnityEngine.UI;

public class MainMenuBehavior : MonoBehaviour
{
    public Button playButton;
    public Button QuitButton;


    void OnEnable()
    {
        playButton.onClick.AddListener(() => GameManager.instance.LoadScene("TestScene"));
        QuitButton.onClick.AddListener(() => GameManager.instance.Quit());
    }

    void OnDisable()
    {
        playButton.onClick.RemoveAllListeners();
        QuitButton.onClick.RemoveAllListeners();
    }
}
