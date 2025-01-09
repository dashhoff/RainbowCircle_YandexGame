using System.Collections;
using UnityEngine;
using YG;
using TMPro;
using UnityEngine.Rendering.RenderGraphModule;

public class AdTimer : MonoBehaviour
{
    public static AdTimer Instance;

    [SerializeField] private TMP_Text _counterText;

    [SerializeField] private GameObject _adPanel;
    [SerializeField] private GameObject _afterAdPanel;

    [SerializeField] private int _timer = 65;
    [SerializeField] private bool _showAd;
    [SerializeField] private bool _afterShowAd;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        //StartCoroutine(AutoAd());
    }

    private void OnEnable()
    {
        YandexGame.CloseFullAdEvent += EndAd;
    }

    private void OnDisable()
    {
        YandexGame.CloseFullAdEvent -= EndAd;
    }

    private void EndAd()
    {
        _showAd = false;
        _afterShowAd = true;
        _afterAdPanel.SetActive(true);
        Time.timeScale = 0;
        AudioListener.volume = 0;
        _adPanel.SetActive(false);
    }

    public void CloseAdterAd()
    {
        _afterShowAd = false;
        _showAd = false;

        _afterAdPanel.SetActive(false);

        EventManager.Instance.EndAd();

        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    public void ShoAd()
    {
        YandexGame.FullscreenShow();
    }

    IEnumerator AutoAd()
    {
        while (true)
        {
            if (_afterShowAd)
            {
                Time.timeScale = 0;
                AudioListener.volume = 0;
            }

            if (_showAd)
            {
                _afterShowAd = true;
                YandexGame.FullscreenShow();
                EndAd();
                _adPanel.SetActive(false);
                _afterAdPanel.SetActive(true);
                _timer = YandexGame.Instance.infoYG.fullscreenAdInterval;
            }
            else
            {
                if (_timer > 0)
                {
                    yield return new WaitForSecondsRealtime(1);
                    _timer--;
                }
                else
                    _showAd = true;

                if (_timer < 3 && _timer > 0)
                {
                    EventManager.Instance.StartAd();

                    Time.timeScale = 0;
                    AudioListener.volume = 0;
                    _adPanel.SetActive(true);

                    if (YandexGame.savesData.language == "ru")
                        _counterText.text = "РЕКЛАМА ЧЕРЕЗ: " + _timer;
                    else
                        _counterText.text = "ADVERTISING VIA: " + _timer;
                }
            }
            yield return null;
        }
    }
}
