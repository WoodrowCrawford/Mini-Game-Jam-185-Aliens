using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private bool _isPaused = false;

    void OnEnable()
    {
        PlayerBehavior.onPauseButtonPressed += TogglePause;
    }

    void OnDisable()
    {
        PlayerBehavior.onPauseButtonPressed -= TogglePause;
    }



    public void TogglePause()
    {
        if (!_isPaused)
        {
            //play the pause sound
            SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.instance.pauseClip, this.transform, false, 0f);

            _isPaused = true;
            Time.timeScale = 0.0001f;
        }

        else if (_isPaused)
        {   
            //play the pause sound
            SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.instance.pauseClip, this.transform, false, 0f);

            _isPaused = false;
            Time.timeScale = 1f;
        }
    }
}
