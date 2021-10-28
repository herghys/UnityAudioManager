using System.Collections;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        #region Variables
        public bool playStartUp;
        public static bool isMuted;
        public static AudioManager Instance = null;

        [SerializeField] AudioData[] sounds = null;
        [SerializeField] AudioSource sourcePrefab = null;

        [SerializeField] string startUpTrack = string.Empty;
        #endregion
        private void Awake()
        {
            if (Instance != null)
            { Destroy(gameObject); }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            InitSounds();
        }

        void Start()
        {
            if (string.IsNullOrEmpty(startUpTrack) && !playStartUp)
            {
                if (string.IsNullOrEmpty(startUpTrack) != true && playStartUp)
                {
                    PlaySound(startUpTrack);
                }
            }
        }

        #region Audio Function
        void InitSounds()
        {
            foreach (var sound in sounds)
            {
                AudioSource source = Instantiate(sourcePrefab, gameObject.transform);
                source.name = sound.name;

                sound.Source = source;
            }
        }

        public void PlaySound(string name)
        {
            AudioData sound = GetSound(name);
            if (sound != null)
            {
                sound.Play();
                sound.Played = true;
            }
        }

        public void StopSound(string name)
        {
            AudioData sound = GetSound(name);
            if (sound != null)
            {
                sound.Stop();
                sound.Played = false;
            }
        }

        public void PlayLoop(string name)
        {
            AudioData sound = GetSound(name);

            if (sound != null)
            {
                sound.Loop = true;
                sound.Play();
                sound.Played = true;
            }
        }

        public void StopLoop(string name)
        {
            AudioData sound = GetSound(name);
            if(sound != null) sound.Loop = false;
        }
        
        public void StopAll()
        {
            foreach (var sound in sounds)
            {
                sound.Stop();
            }
        }

        /*public void ToggleMusic(string name, bool paused)
        {
            AudioData sound = GetSound(name);
            if (sound != null)
            {
                if (paused == false)
                {

                    StartCoroutine(SoundPause(name, paused, 0.4f));
                }
                else
                {
                    StartCoroutine(SoundPause(name, paused, 0));
                    StopSound(name);
                }
            }
        }*/

        /*IEnumerator SoundPause(string name, bool paused, float targetSound)
        {
            AudioData sound = GetSound(name);
            if (paused && sound != null)
            {
                float time = 1f;
                while (time > targetSound)
                {
                    time -= Time.deltaTime;
                    sound.Source.volume = time;
                    yield return 0;
                }

            }
            else if (!paused && sound != null)
            {
                float time = 0f;
                while (time < targetSound)
                {
                    time += Time.deltaTime;
                    sound.Source.volume = time;
                    yield return 0;
                }
            }

        }*/
        #endregion

        #region Getters
        /*public bool GetMusicState(string name)
        {
            AudioData sound = GetSound(name);
            if (sound != null)
            {
                return sound.Played;
            }
            return sound.Played;
        }*/

        AudioData GetSound(string name)
        {
            foreach (AudioData sound in sounds)
            {
                if (sound.Name == name)
                {
                    return sound;
                }
            }
            return null;
        }
        #endregion
    }
}