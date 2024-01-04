using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackGroundMusicPlayer : SingletonBase<BackGroundMusicPlayer>
{
    [SerializeField] private AudioClip m_BGM;
    
    private AudioSource m_SoundSpeaker;

    private void Start()
    {
        m_SoundSpeaker = GetComponent<AudioSource>();
        m_SoundSpeaker.clip = m_BGM;
        PlayMusic();
    }
    public void PlayMusic()
    {
        m_SoundSpeaker.Play();
    }
    public void StopMusic()
    {
        m_SoundSpeaker.Stop();
    }
}
