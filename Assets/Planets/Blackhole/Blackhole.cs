using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Blackhole : MonoBehaviour, IPlanet
{
    [SerializeField] private GameObject BlackholeObj;
    [SerializeField] private GameObject BlackholeRing;
    private Material m_BlackholeObj;
    private Material m_BlackholeRing;
    [SerializeField] private GradientTextureGenerate _gradientBlackholeObj;
    [SerializeField] private GradientTextureGenerate _gradientBlackholeRing;

    private string gradient_vars = "_GradientTex";
    private GradientColorKey[] colorKey1 = new GradientColorKey[3];
    private GradientColorKey[] colorKey2 = new GradientColorKey[5];
    private GradientAlphaKey[] alphaKey1 = new GradientAlphaKey[3];
    private GradientAlphaKey[] alphaKey2 = new GradientAlphaKey[5];
    private string[] _colors1 = new string[] { "#272736", "#ffffeb", "#ed7b39" };
    private string[] _colors2 = new string[] { "#ffffeb", "#fff540", "#ffb84a", "#ed7b39", "#bd4035" };
    private float[] _color_times1 = new float[3] { 0f, 0.5f, 1.0f };
    private float[] _color_times2 = new float[5] { 0f, 0.2f, 0.4f, 0.6f, 1.0f };
    private void Awake()
    {
        m_BlackholeObj = BlackholeObj.GetComponent<Image>().material;
        m_BlackholeRing = BlackholeRing.GetComponent<Image>().material;
        SetInitialColors();
    }
    public void SetPixel(float amount)
    {
        m_BlackholeObj.SetFloat(ShaderProperties.Key_Pixels, amount);
        m_BlackholeRing.SetFloat(ShaderProperties.Key_Pixels, amount);
    }
    public void SetLight(Vector2 pos)
    {
        return;
    }
    public void SetSeed(float seed)
    {
        var converted_seed = seed % 1000f / 100f;
        m_BlackholeObj.SetFloat(ShaderProperties.Key_Seed, converted_seed);
        m_BlackholeRing.SetFloat(ShaderProperties.Key_Seed, converted_seed);
    }
    public void SetRotate(float r)
    {
        m_BlackholeObj.SetFloat(ShaderProperties.Key_Rotation, r);
        m_BlackholeRing.SetFloat(ShaderProperties.Key_Rotation, r);
    }
    public void UpdateTime(float time)
    {
        m_BlackholeObj.SetFloat(ShaderProperties.Key_time, time);
        m_BlackholeRing.SetFloat(ShaderProperties.Key_time, time);
    }
    public void SetCustomTime(float time)
    {
        m_BlackholeObj.SetFloat(ShaderProperties.Key_time, time);
        m_BlackholeRing.SetFloat(ShaderProperties.Key_time, time);
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


        for (int i = 0; i < colorKey2.Length; i++)
        {
            colorKey2[i].color = default(Color);
            ColorUtility.TryParseHtmlString(_colors2[i], out colorKey2[i].color);

            colorKey2[i].time = _color_times2[i];
            alphaKey2[i].alpha = 1.0f;
            colorKey2[i].time = _color_times2[i];
        }
        _gradientBlackholeObj.SetColors(colorKey1, alphaKey1, gradient_vars);
        _gradientBlackholeRing.SetColors(colorKey2, alphaKey2, gradient_vars);
    }

    public Color[] GetColors()
    {
        var colors = new Color[8];
        var gradColors = _gradientBlackholeObj.GetColorKeys();
        for (int i = 0; i < gradColors.Length; i++)
        {
            colors[i] = gradColors[i].color;
        }
        var size = gradColors.Length;

        var gradColors2 = _gradientBlackholeRing.GetColorKeys();
        for (int i = 0; i < gradColors2.Length; i++)
        {
            colors[i + size] = gradColors2[i].color;
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
        _gradientBlackholeObj.SetColors(colorKey1, alphaKey1, gradient_vars);
        var size = colorKey1.Length;
        
        for (int i = 0; i < colorKey2.Length; i++)
        {
            colorKey2[i].color = _colors[i + size];
            colorKey2[i].time = _color_times2[i];
            alphaKey2[i].alpha = 1.0f;
            alphaKey2[i].time = _color_times2[i];
        }
        _gradientBlackholeRing.SetColors(colorKey2, alphaKey2, gradient_vars);

    }
}
