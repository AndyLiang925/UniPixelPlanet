using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Galaxy : MonoBehaviour, IPlanet
{
    [SerializeField] private GameObject Square;
    private Material m_Square;
    [SerializeField] private GradientTextureGenerate _gradientSquare;
    private string gradient_vars = "_GradientTex";
    private GradientColorKey[] colorKey1 = new GradientColorKey[6];
    private GradientAlphaKey[] alphaKey1 = new GradientAlphaKey[6];
    private string[] _colors1 = new string[] { "#ffffeb", "#ffe478", "#8fde5d", "#3d6e70", "#323e4f", "#322947" };

    private float[] _color_times1 = new float[] { 0, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };
    void Awake()
    {
        m_Square = Square.GetComponent<Image>().material;
        SetInitialColors();
    }
    public void SetLight(Vector2 pos)
    {
        return;
    }
    public void SetPixel(float amount)
    {
        m_Square.SetFloat(ShaderProperties.Key_Pixels, amount);
    }
    public void SetSeed(float seed)
    {
        var converted_seed = seed % 1000f / 100f;
        m_Square.SetFloat(ShaderProperties.Key_Seed, converted_seed);
    }

    public void SetRotate(float r)
    {
        m_Square.SetFloat(ShaderProperties.Key_Rotation, r);
    }

    public void UpdateTime(float time)
    {
        m_Square.SetFloat(ShaderProperties.Key_time, time * 0.5f);
    }
    public void SetCustomTime(float time)
    {
        m_Square.SetFloat(ShaderProperties.Key_time, time);
    }
    public void SetInitialColors()
    {
        setGragientColor();
    }
    private void setGragientColor()
    {
        for (int i = 0; i < colorKey1.Length; i++)
        {
            colorKey1[i].color = default(Color);
            ColorUtility.TryParseHtmlString(_colors1[i], out colorKey1[i].color);

            colorKey1[i].time = _color_times1[i];
            alphaKey1[i].alpha = 1.0f;
            alphaKey1[i].time = _color_times1[i];
        }
        _gradientSquare.SetColors(colorKey1, alphaKey1, gradient_vars);
    }
    public Color[] GetColors()
    {
        var colors = new Color[6];
        var gradColors = _gradientSquare.GetColorKeys();
        for (int i = 0; i < gradColors.Length; i++)
        {
            colors[i] = gradColors[i].color;
        }
        return colors;
    }
    public void SetColors(Color[] _colors)
    {
        for (int i = 0; i < colorKey1.Length; i++)
        {
            colorKey1[i].color = _colors[i];
            colorKey1[i].time = _color_times1[i];
            alphaKey1[i].alpha = 1.0f;
            alphaKey1[i].time = _color_times1[i];
        }
        _gradientSquare.SetColors(colorKey1, alphaKey1, gradient_vars);
    }
}
