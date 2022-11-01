using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerEffects : MonoBehaviour
    {
        public AudioClip TakeDamage;

        public AudioClip DieSound;

        public AudioSource AudioSource;

        private Animator _animator;
        
        public static PlayerEffects Instance;

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
        
        
        private void Start()
        {
            AudioSource = GetComponent<AudioSource>();
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
            AudioSource.SafePlayOneShot(DieSound, "Death");
        }
    }
}