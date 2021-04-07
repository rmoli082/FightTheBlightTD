using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeScreen : MonoBehaviour
{
    public RawImage morashSplash;
    public Animator titleText;
    public GameObject startButton;
    public GameObject demonBoss;
    public GameObject slimes;
    public GameObject heroImage;

    private void Start()
    {
        StartCoroutine(SplashyText());
    }

    private IEnumerator SplashyText()
    {
        morashSplash.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        morashSplash.gameObject.SetActive(false);
        titleText.gameObject.SetActive(true);
        titleText.SetTrigger("animate");
        yield return new WaitForSeconds(0.75f);
        demonBoss.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        heroImage.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        slimes.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        startButton.SetActive(true);
    }

    public void StartButton()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }
}
