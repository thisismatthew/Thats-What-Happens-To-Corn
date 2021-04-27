using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaballsSettingsChanger : MonoBehaviour
{
    public MetaballRender2D renderFeature;

    [Range(0f, 1f), Tooltip("Outline size.")]
    public float outlineSize = 1.0f;

    [Tooltip("Inner color.")]
    public Color innerColor = Color.white;

    [Tooltip("Outline color.")]
    public Color outlineColor = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        renderFeature.settings.outlineSize = outlineSize;
        renderFeature.settings.innerColor = innerColor;
        renderFeature.settings.outlineColor = outlineColor;

    }
}
