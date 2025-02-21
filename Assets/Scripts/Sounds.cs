﻿using System;
using AI;
using Player;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static Sounds Instance;

    public AudioSource AudioSource;

    public AudioClip YouDiedMusic;

    public AudioClip CelebrationMusic;
    
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        PlayerHealth.OnPlayerDie += OnPlayerDie;
        GrendelHealth.OnGrendelDie += OnGrendelDie;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerDie -= OnPlayerDie;
        GrendelHealth.OnGrendelDie -= OnGrendelDie;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Play()
    {
        AudioSource.Play();
    }

    public void OnPlayerDie()
    {
        AudioSource.Stop();
        AudioSource.SafePlayOneShot(YouDiedMusic, "DiedMusic");
    }
    
    public void OnGrendelDie()
    {
        AudioSource.Stop();
        AudioSource.SafePlayOneShot(CelebrationMusic, "CelebrationDMusic");
    }
}