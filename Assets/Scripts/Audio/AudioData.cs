using UnityEngine;

namespace Audio
{
    [CreateAssetMenu()]
    public class AudioData : ScriptableObject
    {
        #region Variables
        [Header("Data")]

        [SerializeField] string audioName;
        [SerializeField] AudioClip clip = null;
        [SerializeField] bool isPlayed;

        #region Properties
        public string Name { get => audioName; } //Audio Name
        public AudioClip Clip { get => clip; } //Audio Clip
        public bool Played { get => isPlayed; set => isPlayed = value; } //Check if Played
        #endregion

        public AudioSource Source = null;

        [Header("Parameter")]
        [Range(0, 1)]
        public float Volume = 1;
        [Range(-3, 3)]
        public float Pitch = 1;
        public bool Loop = false;
        #endregion

        public void Play()
        {
            Source.clip = Clip;
            Source.volume = Volume;
            Source.pitch = Pitch;
            Source.loop = Loop;

            Source.Play();
        }

        public void Stop()
        {
            Source.Stop();
        }
    }
}