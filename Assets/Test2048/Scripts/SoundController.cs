using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��� ������ � ������

public class SoundController : MonoBehaviour
{
    [SerializeField] public AudioSource soundMusic;
    [SerializeField] public AudioSource soundEffects;
    public AudioClip collisionSound;
    public AudioClip pushSound;



    public void PlayCollisionSound()
    {
        playSoundEffect(collisionSound);
    }

    public void PlayPushSound()
    {
        playSoundEffect(pushSound);
    }
    
    
    private void playSoundEffect(AudioClip clip)
    {
        soundEffects.clip = clip;
        soundEffects.Play();
     
    }


   
}
