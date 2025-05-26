using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDBehavior : MonoBehaviour
{
    [SerializeField] private TMP_Text _uiScore;
    [SerializeField] private TMP_Text _gameOverUIScore;

    [SerializeField] private TMP_Text _waveScore;
    [SerializeField] private TMP_Text _gameOverWaveScore;

    [SerializeField] private GameObject GameoverScreen;

    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _quitButton;


    void OnEnable()
    {
        PlayerBehavior.onPlayerDeath += ShowGameoverScreen;
        _retryButton.onClick.AddListener(() => ReloadScene());
        _quitButton.onClick.AddListener(() => GameManager.instance.Quit());
    }

    void OnDisable()
    {
        PlayerBehavior.onPlayerDeath -= ShowGameoverScreen;
        _retryButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();

    }

    void Start()
    {
        _uiScore.text = 0.ToString();
        _gameOverUIScore.text = 0.ToString();
        _waveScore.text = 0.ToString();
        _gameOverWaveScore.text = 0.ToString();
    }

    void Update()
    {
        _uiScore.text = GameManager.instance.currentScore.ToString();
        _waveScore.text = GameManager.instance.currentWave.ToString();
    }


    public void ShowGameoverScreen()
    {
        GameoverScreen.SetActive(true);
        _gameOverUIScore.text = _uiScore.text;
        _gameOverWaveScore.text = _waveScore.text;

    }


    public void ReloadScene()
    {
        //reloads the scene

        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


     
}
