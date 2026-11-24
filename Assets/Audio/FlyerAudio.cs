using UnityEngine;

public class FlyerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource flouf;
    public void AudioFlouf()
    {
        flouf.Play();
    }
}
