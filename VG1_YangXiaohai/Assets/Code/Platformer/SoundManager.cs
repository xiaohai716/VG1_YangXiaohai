using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        //Outlets
        AudioSource audioSource;
        public AudioClip missSound;
        public AudioClip hitSound;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySoundHit()
        {
            audioSource.PlayOneShot(hitSound);
        }

        public void PlaySoundMiss()
        {
            audioSource.PlayOneShot(missSound);
        }
    }
}
