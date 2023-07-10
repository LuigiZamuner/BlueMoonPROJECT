using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField]
    public CanvasGroup windowCanvasGroup;

    public static LevelChanger instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public IEnumerator MenuDisplay(bool isOpen)
    {
        float value = isOpen? 1f : 0f;
        float valueAdd = isOpen? 0.01f : -0.01f;

        while(windowCanvasGroup.alpha != value)
        {
            yield return new WaitForFixedUpdate();
            windowCanvasGroup.alpha += valueAdd;
        }
    }
    public IEnumerator Fade()
    {
        yield return StartCoroutine(MenuDisplay(true));
        yield return new WaitForSecondsRealtime(1f);
        yield return StartCoroutine(MenuDisplay(false));
    }
    public IEnumerator SceneChanger(string sceneName)
    {
        yield return StartCoroutine(MenuDisplay(true));
        SceneManager.LoadScene(sceneName);
        yield return StartCoroutine(MenuDisplay(false));
    }
}
