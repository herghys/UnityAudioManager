using UnityEngine;

namespace Herghys.SimpleAudioManager
{
    public class AudioManager : MonoBehaviour
    {
        #region Variables
        [Tooltip("Play StartUp Sound")]
        [SerializeField] bool playStartUp;

        [Tooltip("Persist audio manager across scenes, trigger don't Don't Destroy On Load")]
        [SerializeField] bool persistAcrossScenes;

        [Tooltip("StartUp track to play")]
        [SerializeField] string startUpTrack = string.Empty;

        [SerializeField] AudioData[] sounds = null;
        [SerializeField] AudioSource sourcePrefab = null;

        public static bool isMuted;
        public static AudioManager Instance = null;
        #endregion
        private void Awake()
        {
            if (Instance != null)
            { Destroy(gameObject); }
            else
            {
                Instance = this;

                if (persistAcrossScenes)
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

        #region Non Looping Sound
        /// <summary>
        /// Play sound by name, cascading to other sounds
        /// </summary>
        /// <param name="name">Audio name</param>
        public void PlaySound(string name)
        {
            AudioData sound = GetSound(name);
            if (sound != null)
            {
                sound.Play();
                sound.Played = true;
            }
        }

        /// <summary>
        /// Play sound by sound index, cascading to other sounds
        /// </summary>
        /// <param name="index">Audio index</param>
        public void PlaySound(int index)
        {
            AudioData sound = GetSound(index);
            if (sound != null)
            {
                sound.Play();
                sound.Played = true;
            }
        }

        /// <summary>
        /// Play sound by name, stop other playing sounds
        /// </summary>
        /// <param name="name">Audio name</param>
        public void PlayOneSound(string name)
        {
            StopAll();
            AudioData sound = GetSound(name);
            if (sound != null)
            {
                sound.Play();
                sound.Played = true;
            }
        }

        /// <summary>
        /// Play sound by index, stop other playing sounds
        /// </summary>
        /// <param name="name">Audio index</param>
        public void PlayOneSound(int index)
        {
            StopAll();
            AudioData sound = GetSound(index);
            if (sound != null)
            {
                sound.Play();
                sound.Played = true;
            }
        }

        /// <summary>
        /// Stop sound by name
        /// </summary>
        /// <param name="name">Audio name</param>
        public void StopSound(string name)
        {
            AudioData sound = GetSound(name);
            if (sound != null)
            {
                sound.Stop();
                sound.Played = false;
            }
        }

        /// <summary>
        /// Stop sound by index
        /// </summary>
        /// <param name="index">Audio index</param>
        public void StopSound(int index)
        {
            AudioData sound = GetSound(index);
            if (sound != null)
            {
                sound.Stop();
                sound.Played = false;
            }
        }

        /// <summary>
        /// Stop All playing Sounds
        /// </summary>
        public void StopAll()
        {
            foreach (var sound in sounds)
            {
                sound.Stop();
            }
        }
        #endregion

        #region Audio Loop
        /// <summary>
        /// Play looping audio
        /// </summary>
        /// <param name="name">Audio name</param>
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

        /// <summary>
        /// Play looping audio by index
        /// </summary>
        /// <param name="index">Audio index</param>
        public void PlayLoop(int index)
        {
            AudioData sound = GetSound(index);
            if (sound != null)
            {
                sound.Loop = true;
                sound.Play();
                sound.Played = true;
            }
        }

        /// <summary>
        /// Stop looping audio by name
        /// </summary>
        /// <param name="name">Audio name</param>
        public void StopLoop(string name)
        {
            AudioData sound = GetSound(name);
            if (sound != null) sound.Loop = false;
        }

        /// <summary>
        /// Stop looping audio by index
        /// </summary>
        /// <param name="index">Audio index</param>
        public void StopLoop(int index)
        {
            AudioData sound = GetSound(index);
            if (sound != null) sound.Loop = false;
        }

        /// <summary>
        /// Stop all audio also stop the loops
        /// </summary>
        public void StopAllLoop()
        {
            foreach (var sound in sounds)
            {
                sound.Stop();
                sound.Loop = false;
            }
        }
        #endregion
        #endregion

        #region Getters
        public bool PlayStartUp { get => playStartUp; }
        public bool PersistAcroosScenes { get => persistAcrossScenes; }

        /// <summary>
        /// Get sound by name
        /// </summary>
        /// <param name="name">Audio name</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get sound by index
        /// </summary>
        /// <param name="index">Audio index</param>
        /// <returns></returns>
        AudioData GetSound(int index) => sounds[index];
        #endregion
    }
}