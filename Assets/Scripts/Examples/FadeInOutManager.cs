using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeInOutManager : Singleton<FadeInOutManager>
{
    protected FadeInOutManager() { }    
    private Material fadeMaterial;
    private float fadeOutTime, fadeInTime;
    private Color fadeColor;
    private string navigateToLevelName = "";
    private int navigateToLevelIndex = 0;
    private bool fading = false;

    public static bool Fading
    {
        get { return Instance.fading;}
    }
	void Awake()
    {
        fadeMaterial = new Material(Shader.Find("Default")); //DA CONTROLLARE CON MARCO
    }
    private IEnumerator Fade()
    {
        float t = 0.0f;
        while(t < 1.0f)
        {
            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(t + Time.deltaTime / fadeOutTime);
            DrawingUtilities.DrawQuad(fadeMaterial, fadeColor, t);
        }
        if (navigateToLevelName != "")
            SceneManager.LoadScene(navigateToLevelName);            
        else
            SceneManager.LoadScene(navigateToLevelIndex);

        while(t > 0.0f)
        {
            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(t - Time.deltaTime / fadeInTime);
            DrawingUtilities.DrawQuad(fadeMaterial, fadeColor, t);
        }
        fading = false; 
    }
    
    private void StartFade(float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        fading = true;
        Instance.fadeOutTime = aFadeOutTime;
        Instance.fadeInTime = aFadeInTime;
        Instance.fadeColor = aColor;
        StopAllCoroutines();
        StartCoroutine("Fade");
    }       

    public static void FadeToLevel(string aLevelName, float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        if (Fading) return;
        Instance.navigateToLevelName = aLevelName;
        Instance.StartFade(aFadeOutTime, aFadeInTime, aColor);
    }
}
