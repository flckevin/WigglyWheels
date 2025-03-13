using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuocAnh.pattern;

[RequireComponent(typeof(AudioSource))]

public class AudioManager : Singleton<AudioManager>
{

    public AudioClip[] commonAudio; // all common audio file that going to be play alot of times

    private Dictionary<string,AudioClip> _audioClipDic; // dictionary of all audioclip so we can search faster
    private AudioSource _mAudioSource; // audio source to play sound effect


    private void Awake()
    {
        //*GET
        _mAudioSource = GetComponent<AudioSource>();

        //*SET*

        #region get all audio clip
        //loop all audio in array
        for(int i =0;i< commonAudio.Length; i++) 
        {
            //add all audio into dictionary
            _audioClipDic.Add(commonAudio[i].name, commonAudio[i]);  
        
        }
        #endregion
    
    }

    /// <summary>
    /// function to play audio using current audio source
    /// </summary>
    /// <param name="_audioName"></param>
    /// <param name="_volume"></param>
    public void PlayAudio(string _audioName,float _volume = 1) 
    {
        //play audio using given audio source
        _mAudioSource.PlayOneShot(_audioClipDic[_audioName], _volume);
    
    }


    /// <summary>
    /// function to play custom audio source
    /// </summary>
    /// <param name="_audioName"> audio name </param>
    /// <param name="_volume"> volume value </param>
    /// <param name="_audioSrc"> audio source </param>
    public void PlayAudio(string _audioName, float _volume = 1,AudioSource _audioSrc = null)
    {
        //if audio source does exist
        if (_audioSrc != null)
        {
            //play audio with given audio source
            _audioSrc.PlayOneShot(_audioClipDic[_audioName], _volume);
        }
        else  // audio does not exist 
        {
            //play audio using manager audio source
            _mAudioSource.PlayOneShot(_audioClipDic[_audioName], _volume);

        }
        

    }

}
