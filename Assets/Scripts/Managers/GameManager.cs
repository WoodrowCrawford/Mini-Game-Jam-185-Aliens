using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public int currentScore;
    public int currentWave;


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("On scene loaded:" + scene.name);
        Debug.Log("mode");

        //if the scene is the main menu scene
        if (scene == SceneManager.GetSceneByBuildIndex(0))
        {

        }

        //if the scene is the game scene
        else if (scene == SceneManager.GetSceneByBuildIndex(1))
        {
            currentScore = 0;
            currentWave = 0;

            SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.instance.mainMusicClip, this.transform, true, 0f);
        }
    }

    public void OnSceneUnloaded(Scene scene)
    {
      

       

    }



    public void LoadScene(string sceneName)
    {
        //starts the loading scene
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        //reloads the scene
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


    public void Quit()
    {
        Application.Quit();
    }

    
   
}
