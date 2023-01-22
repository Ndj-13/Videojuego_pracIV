using System.Collections.Generic;
using UnityEngine;
using Components.GameManagement.SoundManager;
using UnityEngine.UI;

public class SoundManager : AGameService
{
    public SoundList soundList;
    //public Sprite musicButton1;
    //public Sprite musicButton2;
    //[SerializeField] private Button musicButton;
    public bool musicActive;

    private Dictionary<string, Sound> _sounds = new Dictionary<string, Sound>();

    private AudioSource _audioSource; //referencia a AudioSource de GameManager

    private void Start()
    {
        musicActive = true;
        //Me registro como servicio
        RegisterService("SoundManager", true, true);

        if (!_audioSource) { _audioSource = GetComponent<AudioSource>(); } 

        //Pila de sonidos:
        for (int i = 0; i < soundList.Sounds.Length; i++)
        {
            Sound sound = soundList.Sounds[i];
            _sounds.Add(sound.name, sound);
        }

        //  musicButton.onClick.AddListener(MusicButtonClickedEvent);
    }

    private void Update()
    {
        if (musicActive)
        {
            _audioSource.mute = false;
        }
        else if (!musicActive)
        {
            _audioSource.mute = true;
        }
    }

    /*   public void MusicButtonClickedEvent()
       {
           if (musicActive)
           {
               musicButton.image.sprite = musicButton2;
               musicActive = false;            
               _audioSource.mute = true;
           }
           else if (!musicActive)
           {
               musicButton.image.sprite = musicButton1;
               musicActive = true;            
               _audioSource.mute = false;
           }
       }
    */
    //Meotodo para reproducir sonidos
    public void Play(string name)
    {
        Sound sound = _sounds[name];
        _audioSource.clip = sound.soundClip;
        _audioSource.volume = sound.volume;
        _audioSource.pitch = sound.pitch;
        _audioSource.Play();
    }

    public void Stop() { _audioSource.Stop(); }

    public bool IsActive() { return !_audioSource.mute; }

    public void ChangeMute(bool active)
    {
        musicActive = active;
        Debug.Log("Music is now mute: " + musicActive);

    }
}
