using System;
using Framework;
using UnityEngine;

namespace Test
{
    public class PanelGenerate : MonoBehaviour
    {
        private void Start()
        {
            UGUIManager.Instance.ShowPanel<UGUITest>(PanelType.PanelAudio);
        }
    }
}