﻿using System;
using Framework;
using UnityEngine.UI;

namespace Test
{
    public class UGUITest : UGUIBase
    {
        protected override void Awake()
        {
            FindChildrenUIControl<Button>();
        }
        private void Start()
        {
            GetUIControl<Button>("BGM").onClick.AddListener(PlayBGM);
        }

        private void PlayBGM()
        {
            AudioCenter.Instance.AudioPlayAsync(new AudioAsset(AudioType.BGM,"BGM"));
        }

        
    }
}