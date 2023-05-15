using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioText : MonoBehaviour
{
    [SerializeField] protected SoundProfileData soundProfileData;
    [SerializeField] protected SoundProfileData soundProfileBGMData;

    private AudioManager audioManager => AudioManager.Instance;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.PlayOneShot(soundProfileData.GetRandomClip());
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            audioManager.PlayMusic(soundProfileBGMData.GetRandomClip());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            audioManager.FadeInMuisc(soundProfileBGMData.GetClipIndex(0), 3.0f);
        }
    }
}
