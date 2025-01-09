using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpgradeMaxSpeed()
    {
        if (GameSettings.Instance.Money >= GameSettings.Instance.MaxSpeedPrice)
        {
            SoundManager.Instance.BuySound();

            GameSettings.Instance.Money -= GameSettings.Instance.MaxSpeedPrice;
            GameSettings.Instance.MaxSpeedPrice = Convert.ToInt32(GameSettings.Instance.MaxSpeedPrice * 1.05f) + 10;
            GameSettings.Instance.MaxSpeed += 10;

            GameSettings.Instance.SaveProgress();
        }
        else
        {
            SoundManager.Instance.BuyErrorSound();

            UIManager.Instance.OpenPanel(UIManager.Instance.BuyErrorPanel);
        }

        UIManager.Instance.UpdateShopText();
    }

    public void UpgradeLifeTime()
    {
        if (GameSettings.Instance.Money >= GameSettings.Instance.MaxLifeTimePrice)
        {
            SoundManager.Instance.BuySound();

            GameSettings.Instance.Money -= GameSettings.Instance.MaxLifeTimePrice;
            GameSettings.Instance.MaxLifeTimePrice += Convert.ToInt32(GameSettings.Instance.MaxLifeTimePrice * 1.05f) + 10;
            GameSettings.Instance.MaxLifeTime += 2;

            GameSettings.Instance.SaveProgress();
        }
        else
        {
            SoundManager.Instance.BuyErrorSound();

            UIManager.Instance.OpenPanel(UIManager.Instance.BuyErrorPanel);
        }

        UIManager.Instance.UpdateShopText();
    }

    public void UpgradeSpawnInterval()
    {
        if (GameSettings.Instance.Money >= GameSettings.Instance.SpawnIntervalPrice)
        {
            SoundManager.Instance.BuySound();

            GameSettings.Instance.Money -= GameSettings.Instance.SpawnIntervalPrice;
            GameSettings.Instance.SpawnIntervalPrice += Convert.ToInt32(GameSettings.Instance.SpawnIntervalPrice * 1.05f) + 10;

            if (GameSettings.Instance.SpawnInterval >= 0.06f)
                GameSettings.Instance.SpawnInterval -= 0.05f;

            GameSettings.Instance.SaveProgress();
        }
        else
        {
            SoundManager.Instance.BuyErrorSound();

            UIManager.Instance.OpenPanel(UIManager.Instance.BuyErrorPanel);
        }

        UIManager.Instance.UpdateShopText();
    }

    public void UpgradeColorInterval()
    {
        if (GameSettings.Instance.Money >= GameSettings.Instance.ColorIntervalPrice)
        {
            SoundManager.Instance.BuySound();

            GameSettings.Instance.Money -= GameSettings.Instance.ColorIntervalPrice;
            GameSettings.Instance.ColorIntervalPrice += Convert.ToInt32(GameSettings.Instance.ColorIntervalPrice * 1.05f) + 10;

            GameSettings.Instance.ColorInterval += 0.05f;

            GameSettings.Instance.SaveProgress();
        }
        else
        {
            SoundManager.Instance.BuyErrorSound();

            UIManager.Instance.OpenPanel(UIManager.Instance.BuyErrorPanel);
        }

        UIManager.Instance.UpdateShopText();
    }
}
