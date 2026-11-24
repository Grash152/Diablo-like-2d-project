using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource slash;
    [SerializeField] private AudioSource bigSlash;
    
    public void audioSlash()
    {
        slash.Play();
    }
    public void audioBigSlash()
    {
        bigSlash.Play();
    }
}
