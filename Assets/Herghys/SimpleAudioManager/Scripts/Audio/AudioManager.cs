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
        /// <summary>
        /// Play Sound, cascading to other sounds
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
        /// Play one sound, stop other playing sounds
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
        /// Stop All playing Sounds
        /// </summary>
        public void StopAll()
        {
            foreach (var sound in sounds)
            {
                sound.Stop();
            }
        }

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
        /// Stop Audio Loop
        /// </summary>
        /// <param name="name">Audio name</param>
        public void StopLoop(string name)
        {
            AudioData sound = GetSound(name);
            if(sound != null) sound.Loop = false;
        }
        #endregion

        #region Getters
        public bool PlayStartUp { get => playStartUp; }
        public bool PersistAcroosScenes { get => persistAcrossScenes; }
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