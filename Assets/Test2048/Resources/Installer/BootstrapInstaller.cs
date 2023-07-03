using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    private void Awake()
    {
        StartCoroutine("LoadNextScene");
    }

    public override void InstallBindings()
    {
       BindSoundController();
       
    }

    public void BindSoundController()
    {
        Container
            .Bind<SoundController>().FromInstance(GetComponentInChildren<SoundController>());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level");
    }
}