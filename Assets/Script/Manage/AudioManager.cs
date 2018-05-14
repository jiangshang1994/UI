using Assets.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Manage
{
    public class AudioManager
    {
        private static AudioManager instance;
        public int MAX_CHANEL = 3; //同时播放的数量
        private AudioSource audioPlayer;
        private AudioClip[] audioClips;
        private float defaultVolumn;

        public void Play(AudioClip clip, bool isLoop = false, int volumn = 1)
        {
            AudioChanel.GetChanel().Play(clip, isLoop,volumn);
        } 

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioManager();
                }
                return instance;
            }
        }

        public float DefaultVolumn
        {
            get
            {
                return defaultVolumn;
            }

            set
            {
                defaultVolumn = value;
            }
        }

        private AudioManager()
        {
            this.audioPlayer = new AudioSource();
            this.audioClips = new AudioClip[MAX_CHANEL];
        }
    }
}
