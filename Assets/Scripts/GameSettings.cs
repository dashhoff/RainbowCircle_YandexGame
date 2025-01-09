using NaughtyAttributes;
using UnityEngine;
using YG;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    public int Money;

    [Space(20f)]
    [Header("Игровые настройки")]
    public bool GameMoted;
    public bool FxOn;
    public bool CameraShakeOn;

    [Space(20f)]
    [Header("Настройки шара")]
    public float MaxLifeTime;
    public float MaxSpeed;

    [Space(20f)]
    [Header("Настройки следа")]
    public float SpawnInterval;
    public float ColorInterval;

    [Space(20f)]
    [Header("Цены")]
    public int MaxLifeTimePrice;
    public int MaxSpeedPrice;

    public int SpawnIntervalPrice;
    public int ColorIntervalPrice;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadProgress();

        UIManager.Instance.UpdateMoneyText();
    }

    private void OnEnable()
    {
        EventManager.EndingGame += SaveProgress;
    }

    private void OnDisable()
    {
        EventManager.EndingGame -= SaveProgress;
    }

    public void LoadProgress()
    {
        Money = YandexGame.savesData.money;

        GameMoted = YandexGame.savesData.gameMoted;
        FxOn = YandexGame.savesData.fxOn;
        CameraShakeOn = YandexGame.savesData.cameraShakeOn;

        MaxLifeTime = YandexGame.savesData.ballLifeTime;
        MaxSpeed = YandexGame.savesData.ballMaxSpeed;

        MaxSpeedPrice = YandexGame.savesData.maxSpeedPrice;
        MaxLifeTimePrice = YandexGame.savesData.lifeTimePrice;
        SpawnIntervalPrice = YandexGame.savesData.spawnIntervalPrice;
        ColorIntervalPrice = YandexGame.savesData.colorIntervalPrice;

        SpawnInterval = YandexGame.savesData.rainbowCircleSpawnInterval;
        ColorInterval = YandexGame.savesData.rainbowCircleColorChangeSpeed;
    }

    public void SaveProgress()
    {
        YandexGame.savesData.money = Money;

        YandexGame.savesData.gameMoted = GameMoted;
        YandexGame.savesData.fxOn = FxOn;
        YandexGame.savesData.cameraShakeOn = CameraShakeOn;

        YandexGame.savesData.ballLifeTime = MaxLifeTime;
        YandexGame.savesData.ballMaxSpeed = MaxSpeed;

        YandexGame.savesData.maxSpeedPrice = MaxSpeedPrice;
        YandexGame.savesData.lifeTimePrice = MaxLifeTimePrice;
        YandexGame.savesData.spawnIntervalPrice = SpawnIntervalPrice;
        YandexGame.savesData.colorIntervalPrice = ColorIntervalPrice;

        YandexGame.savesData.rainbowCircleSpawnInterval = SpawnInterval;
        YandexGame.savesData.rainbowCircleColorChangeSpeed = ColorInterval;

        YandexGame.SaveProgress();
    }

    [Button]
    public void ResetProgress()
    {
        YandexGame.savesData.money = 10;

        YandexGame.savesData.gameMoted = false;
        YandexGame.savesData.fxOn = true;
        YandexGame.savesData.cameraShakeOn = true;

        YandexGame.savesData.ballLifeTime = 20;
        YandexGame.savesData.ballMaxSpeed = 10;

        YandexGame.savesData.maxSpeedPrice = 5;
        YandexGame.savesData.lifeTimePrice = 5;
        YandexGame.savesData.spawnIntervalPrice = 5;
        YandexGame.savesData.colorIntervalPrice = 5;

        YandexGame.savesData.rainbowCircleSpawnInterval = 0.3f;
        YandexGame.savesData.rainbowCircleColorChangeSpeed = 0.3f;

        YandexGame.SaveProgress();

        LoadProgress();

        UIManager.Instance.UpdateShopText();
        UIManager.Instance.UpdateMoneyText();
    }
}
