using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] List<Scrolling> scrollableLayers;

    public void Pause()
    {
        scrollableLayers.ForEach(layer => layer.Pause());
    }

    public void Continue()
    {
        scrollableLayers.ForEach(layer => layer.Continue());
    }
}
