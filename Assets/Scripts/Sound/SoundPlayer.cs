using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : SingletonBase<SoundPlayer>
{
    [SerializeField] private SoundBank m_SoundBank;
    //[SerializeField] private AudioClip m_BGM;
    private AudioSource m_AS;  

    /*
    private new void Awake()
    {
        m_AS = GetComponent<AudioSource>();
        base.Awake();
        Instance.m_AS.clip = m_BGM;
        Instance.m_AS.Play();
    }
    */
    private void Start()
    {      
        m_AS = GetComponent<AudioSource>();
    }  
    public void Play(Sound sound)
    {
        m_AS.PlayOneShot(m_SoundBank[sound]);
    }
}
