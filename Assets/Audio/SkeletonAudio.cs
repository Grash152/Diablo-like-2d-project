using UnityEngine;

public class SkeletonAudio : MonoBehaviour
{
    [SerializeField] private AudioSource Bonk;
    public void AudioBonk()
    {
        Bonk.Play();
    }
}
