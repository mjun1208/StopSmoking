using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource Sound;

    public AudioClip Hit;
    public AudioClip Hit2;
    public AudioClip Dead;
    public AudioClip AxeSwing;
    public AudioClip Swing;
    public AudioClip Fart;
    public AudioClip Jump;
    public AudioClip Stab;
    public AudioClip Spin;
    public AudioClip Talk_Sans;
    public AudioClip Talk_Al;
    public AudioClip Talk_Pa;
    public AudioClip PlayerDead;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayHit()
    {
        Sound.PlayOneShot(Hit);
    }
    public void PlayHit2()
    {
        Sound.PlayOneShot(Hit2);
    }
    public void PlayDead()
    {
        Sound.PlayOneShot(Dead);
    }

    public void PlayAxeSwing()
    {
        Sound.PlayOneShot(AxeSwing);
    }

    public void PlaySwing()
    {
        Sound.PlayOneShot(Swing);
    }

    public void PlayFart()
    {
        Sound.PlayOneShot(Fart);
    }

    public void PlayJump()
    {
        Sound.PlayOneShot(Jump);
    }

    public void PlayStab()
    {
        Sound.PlayOneShot(Stab);
    }

    public void PlaySpin()
    {
        Sound.PlayOneShot(Spin);
    }

    public void PlayTalk_Sans()
    {
        Sound.PlayOneShot(Talk_Sans);
    }

    public void PlayTalk_Al()
    {
        Sound.PlayOneShot(Talk_Al);
    }

    public void PlayTalk_Pa()
    {
        Sound.PlayOneShot(Talk_Pa);
    }

    public void PlayPlayerDead()
    {
        Sound.PlayOneShot(PlayerDead);
    }
}
