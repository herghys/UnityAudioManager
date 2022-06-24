using UnityEngine;

namespace Herghys.SimpleAudioManager
{
    [CreateAssetMenu()]
    public class AudioData : ScriptableObject
    {
        #region Variables
        [Header("Data")]
        private bool isPlayed;

        [Tooltip("Audio Name")]
        [SerializeField] string audioName;
        [Tooltip("Audio Clip")]
        [SerializeField] AudioClip clip = null;

        #region Properties
        /// <summary>
        /// Get Audio Name
        /// </summary>
        public string Name { get => audioName; } //Audio Name

        /// <summary>
        /// Get Audio Clip
        /// </summary>
        public AudioClip Clip { get => clip; } //Audio Clip

        /// <summary>
        /// Get and Set audio played status
        /// </summary>
        public bool Played { get => isPlayed; set => isPlayed = value; } //Check if Played
        #endregion

        public AudioSource Source = null;

        [Header("Parameter")]
        [Range(0, 1)]
        [Tooltip("Volume Range 0 - 1, Default 1")]
        public float Volume = 1;

        [Range(-5, 5)]
        [Tooltip("Pitch Range -5 - 5, Default 1")]
        public float Pitch = 1;

        [Tooltip("Is this Audio may loop?")]
        public bool Loop = false;
        #endregion

        /// <summary>
        /// Play Audio Sources
        /// </summary>
        internal void Play()
        {
            Source.clip = Clip;
            Source.volume = Volume;
            Source.pitch = Pitch;
            Source.loop = Loop;

            Source.Play();
        }

        /// <summary>
        /// Stop Audio Sources
        /// </summary>
        internal void Stop()
        {
            Source.Stop();
        }
    }
}