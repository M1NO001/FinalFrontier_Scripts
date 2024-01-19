using UnityEngine;

public class PlayerSoundEffect : MonoBehaviour
{
    Player player;

    public AudioSource FootstepAudioSource;
    public AudioSource GunAudioSource;
    public AudioSource ReloadAudioSource;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void FootStepSoundEffect()
    {
        //if (FootstepAudioSource != null&& FootstepAudioSource.)
        //{
        //    FootstepAudioSource.Play();
        //}
    }

    public void FireSoundEffect()
    {
        if(GunAudioSource != null)
        {
            GunAudioSource.Play();
        }
    }

    public void ReloadSoundEffect()
    {
        if(ReloadAudioSource != null)
        {
            ReloadAudioSource.Play();
        }
    }
}
