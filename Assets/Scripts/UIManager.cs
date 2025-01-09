using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Space(20f)]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _menuPanel;

    [Space(20f)]
    [Header("UI верхнего меню")]
    [SerializeField] private GameObject _shopUpIcon;

    [Space(20f)]
    [Header("UI Завершения")]
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private Image _endBackground;
    [SerializeField] private GameObject _endPopupPanel;

    [Header("UI Магазина")]
    [Space(20f)]
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private Image _shopBackground;
    [SerializeField] private GameObject _shopPopupPanel;
    public GameObject BuyErrorPanel;

    [Space(20f)]
    [Header("UI Настроек")]
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Image _settingsBackground;
    [SerializeField] private GameObject _settingsPopupPanel;

    [Space(20f)]
    [Header("Настройка времени")]
    [SerializeField] private float _openPopupDuration = 0.5f;
    [SerializeField] private float _closePopupDuration = 0.5f;

    [Space(5f)]
    [SerializeField] private float _fadeInDuration = 0.5f;
    [SerializeField] private float _fadeOutDuration = 0.5f;

    [Space(20f)]
    [Header("Настройка текста")]
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _lifeTimeText;

    [Space(5f)]
    [SerializeField] private TMP_Text _maxSpeedShopText;
    [SerializeField] private TMP_Text _lifeTimeShopText;
    [SerializeField] private TMP_Text _spawnIntervalShopText;
    [SerializeField] private TMP_Text _colorintervalShopText;

    [Space(5f)]
    [SerializeField] private TMP_Text _maxSpeedPriceText;
    [SerializeField] private TMP_Text _lifeTimePriceText;
    [SerializeField] private TMP_Text _spawnIntervalPriceText;
    [SerializeField] private TMP_Text _colorIntervalPriceText;

    private void Start()
    {
        Instance = this;

        UpdateShopText();

        _lifeTimeText.text = "" + GameSettings.Instance.MaxLifeTime;
    }

    private void OnEnable()
    {
        EventManager.StartingGame += StartGame;
        EventManager.StartingGame += HideShop;

        EventManager.EndingGame += EndGame;

        EventManager.BallCollision += UpdateMoneyText;
    }

    private void OnDisable()
    {
        EventManager.StartingGame -= StartGame;
        EventManager.StartingGame -= HideShop;

        EventManager.EndingGame -= EndGame;

        UpdateMoneyText();

        EventManager.BallCollision -= UpdateMoneyText;
    }

    private void StartGame()
    {
        _menuPanel.SetActive(false);
    }

    private void EndGame()
    {
        _endPanel.SetActive(true);

        FadeInPanel(_endBackground);
        OpenPopup(_endPopupPanel);
    }

    private void HideShop()
    {
        ClosePanel(_shopUpIcon);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void OpenPopup(GameObject popup, float startDelay)
    {
        DOTween.Sequence()
            .AppendInterval(startDelay)
            .Append(popup.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), _openPopupDuration))
            .Append(popup.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
    }

    public void OpenPopup(GameObject popup)
    {
        DOTween.Sequence()
            .Append(popup.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), _openPopupDuration))
            .Append(popup.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
    }

    public void ClosePopup(GameObject popup, float startDelay)
    {
        DOTween.Sequence()
            .AppendInterval(startDelay)
            .Append(popup.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f))
            .Append(popup.transform.DOScale(new Vector3(0, 0, 0), _closePopupDuration));
    }

    public void ClosePopup(GameObject popup)
    {
        DOTween.Sequence()
            .Append(popup.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f))
            .Append(popup.transform.DOScale(new Vector3(0, 0, 0), _closePopupDuration));
    }

    public void FadeInPanel(Image panel, float startDelay)
    {
        DOTween.Sequence()
            .AppendInterval(startDelay)
            .Append(panel.DOFade(0.45f, _fadeInDuration));
    }

    public void FadeInPanel(Image panel)
    {
        panel.gameObject.SetActive(true);

        DOTween.Sequence()
            .Append(panel.DOFade(0.45f, _fadeInDuration));
    }

    public void FageOutPanel(Image panel, float startDelay)
    {
        DOTween.Sequence()
            .AppendInterval(startDelay)
            .Append(panel.DOFade(0f, _fadeOutDuration))
            .OnComplete(() => ClosePanel(panel.gameObject));
    }

    public void FageOutPanel(Image panel)
    {
        DOTween.Sequence()
            .Append(panel.DOFade(0f, _fadeOutDuration))
            .OnComplete(() => ClosePanel(panel.gameObject));
    }

    public void UpdateMoneyText()
    {
        _moneyText.text = "" + GameSettings.Instance.Money;
    }

    public void UpdateLifeTimeText()
    {
        _lifeTimeText.text = "" + Ball.Instance.LifeTime;
    }

    public void UpdateShopText()
    {
        _lifeTimePriceText.text = GameSettings.Instance.MaxLifeTimePrice.ToString();
        _maxSpeedPriceText.text = GameSettings.Instance.MaxSpeedPrice.ToString();
        _spawnIntervalPriceText.text = GameSettings.Instance.SpawnIntervalPrice.ToString();
        _colorIntervalPriceText.text = GameSettings.Instance.ColorIntervalPrice.ToString();
    }
}
