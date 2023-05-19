using System;
using Framework;
using UnityEngine;
using UnityEngine.UI;
using AudioType = Framework.AudioType;

namespace Test.AudioTest
{
    public class AudioTest : MonoBehaviour
    {
        public Slider bgmVolume;
        public Slider uIVolume;
        
        public void PlayBGM()
        {
            AudioCenter.Instance.AudioPlaySync(new AudioAsset(AudioType.BGM,"BGM"));
        }
        
        public void PlayUI()
        {
            AudioCenter.Instance.AudioPlaySync(new AudioAsset(AudioType.UI,"GetProps"));
        }
        
        public void PlayEffect()
        {
            AudioCenter.Instance.AudioPlaySync(new AudioAsset(AudioType.Effect,"Button"));
        }

        public void BGMVolume()
        {
            AudioCenter.Instance.SetVolume(AudioType.BGM,bgmVolume.value);
        }
        
        public void UIVolume()
        {
            AudioCenter.Instance.SetVolume(AudioType.UI,uIVolume.value);
        }

        public void MuteBGM()
        {
            AudioCenter.Instance.SwitchMuteState(AudioType.BGM);
        }
        
        public void MuteUI()
        {
            AudioCenter.Instance.SwitchMuteState(AudioType.UI);
        }
    }
}
