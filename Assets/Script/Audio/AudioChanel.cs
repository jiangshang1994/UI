using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Audio
{
    /// <summary>
    /// 默认的声道为4个。如果不够用会自动扩充。
    /// 每次增加2个
    /// </summary>
    public class AudioChanel
    {
        private bool isIdel = true;
        private AudioSource audioSource;
        public const int DFAULT_CHANEL_COUNT = 4;
        private bool isLoop;

        /// <summary>
        /// </summary>
        /// <param name="level">初始为4个</param>
        public static AudioChanel GetChanel()
        {
            foreach (AudioChanel chanel in AudioChanels)
            {
                if (chanel.isIdel == true)
                {
                 
                    return chanel;
                }
            }
            AudioChanels = new AudioChanel[audioChanels.Length +2];
            return GetChanel();
        }
        /// <summary>
        /// </summary>
        /// <param name="audioClip">根据audioClip停止</param>
        /// <param name="isContinue"></param>
        public static void EndChanel(AudioClip audioClip)
        {
            foreach (AudioChanel chanel in AudioChanels)
            {
                if (chanel.AudioSource.clip.Equals(audioClip))
                {
                    chanel.EndChanel();
                }
            }
        }

        public void PauseChanel()
        {
            this.AudioSource.Pause();
        }

        /// <summary>
        /// 会在控件或者面板销毁时自动调用。
        /// 如果未销毁chanel会一直被占用
        /// </summary>
        public void EndChanel()
        {
            this.AudioSource.Stop();
            this.Clear();
        }

        private void Clear()
        {
            this.IsIdel = true;
            this.IsLoop = false;
            this.AudioSource.clip = null;
        }

        public void Play(AudioClip audioClip, bool isLoop = false, float volumn = 1)
        {
            this.IsLoop = isLoop;
            this.isIdel = false;
            this.audioSource.clip = audioClip;
            this.audioSource.volume = volumn;
            this.audioSource.Play();
        }

        private AudioChanel()
        {
            this.audioSource = new AudioSource();
        }

        public bool IsIdel
        {
            get
            {
                return isIdel;
            }

            set
            {
                isIdel = value;
            }
        }

        private static AudioChanel[] audioChanels;
        private static AudioChanel[] AudioChanels
        {
            get
            {
                if (audioChanels == null)
                {
                    audioChanels = new AudioChanel[DFAULT_CHANEL_COUNT];
                    for (int i = 0; i < DFAULT_CHANEL_COUNT; i++)
                    {
                        audioChanels[i] = new AudioChanel();
                    }
                }
                return audioChanels;
            }
            set
            {
                audioChanels = null;
                audioChanels = value;
            }
        }

        public bool IsLoop
        {
            get
            {
                return isLoop;
            }

            set
            {
                this.audioSource.loop = value;
            }
        }

        public AudioSource AudioSource
        {
            get
            {
                return audioSource;
            }

            set
            {
                audioSource = value;
            }
        }
    }
}
