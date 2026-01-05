using UnityEngine;
using MyStudio.Core.Architecture; 

namespace MyStudio.Core.Systems
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        [Header("Settings")]
        [Range(0f, 1f)] public float musicVolume = 1f;
        [Range(0f, 1f)] public float sfxVolume = 1f;

        protected override void Awake()
        {
            base.Awake();
            
            if (_musicSource == null) _musicSource = gameObject.AddComponent<AudioSource>();
            if (_sfxSource == null) _sfxSource = gameObject.AddComponent<AudioSource>();

            _musicSource.loop = true;
        }
        
        public void PlayMusic(AudioClip clip)
        {
            if (_musicSource.clip == clip) return;

            _musicSource.clip = clip;
            _musicSource.volume = musicVolume;
            _musicSource.Play();
        }

      
        public void PlaySFX(AudioClip clip)
        {
            if (clip == null) return;
            _sfxSource.PlayOneShot(clip, sfxVolume);
        }

        //mute global
        public void MuteAll(bool isMuted)
        {
            _musicSource.mute = isMuted;
            _sfxSource.mute = isMuted;
        }
        
        // setting volume (bisa untuk UI)
        public void SetMusicVolume(float vol) 
        {
            musicVolume = vol;
            _musicSource.volume = musicVolume;
        }
    }
}