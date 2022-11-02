using System;
using UnityEngine;

namespace AI
{
    public class GrendelEffects : MonoBehaviour
    {
        private AudioSource _audioSource;

        public AudioClip GrendelDamage;

        public AudioClip GrendelDie;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            GrendelHealth.OnGrendelHurt += OnGrendelHurt;
            GrendelHealth.OnGrendelDie += OnGrendelDie;
        }

        private void OnDestroy()
        {
            GrendelHealth.OnGrendelHurt -= OnGrendelHurt;
            GrendelHealth.OnGrendelDie -= OnGrendelDie;
        }

        private void OnGrendelHurt(int arg1, int arg2)
        {
            _audioSource.SafePlayOneShot(GrendelDamage, "GrendelHurt");

        }

        private void OnGrendelDie()
        {
            _audioSource.SafePlayOneShot(GrendelDie, "GrendelDie");
        }
    }
}