using UnityEngine;
using Audio;

public class AudioButton : MonoBehaviour
{
    public void PlayAudio(string audioName)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(audioName);
        }
    }

    public void StopAudio(string audioName)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopSound(audioName);
        }
    }

    public void PlayLoopAudio(string audioName)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayLoop(audioName);
        }
    }

    public void StopLoopAudio(string audioName)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopLoop(audioName);
            AudioManager.Instance.StopSound(audioName);
        }
    }

    public void StopAllAudio()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.StopAll();
    }
}
