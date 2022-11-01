using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerEffects : MonoBehaviour
    {
        public AudioClip TakeDamage;

        public AudioClip DieSound;

        private AudioSource _audioSource;

        private Animator _animator;
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
            PlayerHealth.OnPlayerDie += OnPlayerDie;
        }

        private void OnDestroy()
        {
            PlayerHealth.OnPlayerDie -= OnPlayerDie;
        }

        public void OnPlayerDie()
        {
            _animator.Play("Death");
            _audioSource.SafePlayOneShot(DieSound);
        }
    }
}