using System;
using UnityEngine;

namespace AI
{
    public class GrendelEffects : MonoBehaviour
    {
        private AudioSource _audioSource;

        public AudioClip GrendelDamage;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            GrendelHealth.OnGrendelHurt += OnGrendelHurt;
        }

        private void OnDestroy()
        {
            GrendelHealth.OnGrendelHurt -= OnGrendelHurt;
        }

        private void OnGrendelHurt(int arg1, int arg2)
        {
            _audioSource.SafePlayOneShot(GrendelDamage, "GrendelHurt");

        }
    }
}