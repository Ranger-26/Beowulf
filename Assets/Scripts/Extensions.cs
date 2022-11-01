using UnityEngine;

public static class Extensions
{
        public static void SafePlayOneShot(this AudioSource audioSource, AudioClip clip, string clipName = "default")
        {
                if (clip != null)
                {
                        audioSource.PlayOneShot(clip);
                }
                else
                {
                        Debug.Log($"Clip {clipName} is null!");
                }
        }
}