using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PlayRandomAudio : MonoBehaviour
    {
        [SerializeField] private float minPitch = 0.9f;
        [SerializeField] private float maxPitch = 1.1f;
        [SerializeField] private float minVolume = 0.8f;
        [SerializeField] private float maxVolume = 1f;
        public AudioClip[] audioClips;
        public AudioSource audioSource;
        private int audioIndex = 0;

        private void PlayNext()
        {
            RandomizeVolume();
            RandomizePitch();

            if (audioIndex >= audioClips.Length) audioIndex = 0;

            PlayAudioAtIndex(audioIndex);
            audioIndex++;
        }

        public void PlayRandom()
        {
            audioIndex = (int)GetNewRandomIndex(audioIndex, audioClips.Length);
            RandomizeVolume();
            RandomizePitch();
            PlayAudioAtIndex(audioIndex);
        }

        private int GetNewRandomIndex(int currentIndex, int range)
        {
            //if (range <= 1) return 0;

            float newIndex = Random.Range(0f, range);

            while (newIndex == currentIndex)
            {
                newIndex = Random.Range(0f, range);
            }

            return (int)newIndex;
        }

        private void RandomizeVolume()
        {
            audioSource.volume = Random.Range(minVolume, maxVolume);
        }

        private void RandomizePitch()
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
        }

        private void PlayAudioAtIndex(int index)
        {
            audioSource.PlayOneShot(audioClips[index]);
        }
    }
}
