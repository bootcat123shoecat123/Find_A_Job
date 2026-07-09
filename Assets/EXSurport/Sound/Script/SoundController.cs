using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static Unity.VisualScripting.Member;

namespace SupSystem
{
    public class SoundController : MonoBehaviour
    {
        // Start is called before the first frame update
        public static SoundController Instance;
        public List<AudioClip> BGM;
        public List<AudioClip> SE;
        public List<AudioClip> Sound;
        public List<AudioClip> Special;
        public bool WipSence;
        public List<AudioSource> playingAudio;
        [SerializeField] GameObject AudioSource;
        public AudioMixer Mixer;
        void Start()
        {
            if (FindObjectsByType<SoundController>(0).Length > 1)
            {
                Destroy(gameObject);
            }
            if (WipSence)
            {
                DontDestroyOnLoad(gameObject);
            }
            Instance=this;
        }
        
        public void PlayAudio(AudioClip sound, SoundChannel audioType, bool isLoop = false)
        {
            GameObject audio = Instantiate(AudioSource,transform);
            AudioSource source = audio.GetComponent<AudioSource>();
            source.outputAudioMixerGroup = Mixer.FindMatchingGroups(Enum.GetName(typeof(SoundChannel), audioType))[0];
            source.loop = isLoop;
            source.clip = sound;
            playingAudio.Add(source);
            source.Play();
            if (!isLoop) StartCoroutine(RemoveSound(source, source.clip.length + 0.1f));
        }
        public void PlayAudio(string sound, SoundChannel audioType, bool isLoop = false)
        {
            GameObject audio = Instantiate(AudioSource, transform);
            AudioSource source = audio.GetComponent<AudioSource>();
            source.outputAudioMixerGroup = Mixer.FindMatchingGroups(Enum.GetName(typeof(SoundChannel), audioType))[0];
            source.loop = isLoop;
            List<AudioClip> TargetList=null;
            switch (audioType)
            {
                
                case SoundChannel.BGM:
                    TargetList = BGM;
                    break;
                case SoundChannel.SE:
                    TargetList = SE;

                    break;
                case SoundChannel.Sound:

                    TargetList = Sound;
                    break;
                case SoundChannel.Special:
                    TargetList = Special;
                    break;
                default:
                    Debug.LogError("Don't input other type without List.");
                    break;
            }
            source.clip=TargetList.Find(e=>e.name== sound);
            playingAudio.Add(source);
            if (source.clip == null)
            {
                Debug.LogWarning("Can't find the music in this list.");
            }
            source.Play();
            if (!isLoop) StartCoroutine(RemoveSound(source, source.clip.length + 0.1f));
        }
        public void StopPlay(string name)
        {
            AudioSource source=playingAudio.Find(e=>e.clip.name== name);
            source.Pause(); StartCoroutine(RemoveSound(source, source.clip.length + 0.1f));
        }
        public void ControllMixerVolume(SoundChannel audioType, float vol)
        {
            Mixer.SetFloat(Enum.GetName(typeof(SoundChannel), audioType) + "Vol", vol);
        }
        public float GetVolume(SoundChannel audioType)
        {
            float vol;
            Mixer.GetFloat(Enum.GetName(typeof(SoundChannel), audioType) + "Vol", out vol);
            return vol;
        }
        IEnumerator RemoveSound(AudioSource sound,float time)
        {
            yield return new WaitForSeconds(time);
            playingAudio.Remove(sound);
            Destroy(sound.gameObject, 0);

        }
        public enum SoundChannel
        {
            Master,
            BGM,
            SE,
            Sound,
            Special
        }
    }
}