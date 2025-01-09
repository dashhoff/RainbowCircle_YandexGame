using UnityEngine;
using YG;

public class AdManager : MonoBehaviour
{
    [SerializeField] private int _moneyAfterAdvertising = 15;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    private void Rewarded(int id)
    {
        switch (id)
        {
            case 1:
                AddMoney(_moneyAfterAdvertising);
                UIManager.Instance.UpdateMoneyText();
                break;
        }
    }

    public void OpenRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    private void AddMoney(int value)
    {
        GameSettings.Instance.Money += value;
        GameSettings.Instance.SaveProgress();

        UIManager.Instance.UpdateMoneyText();
    }
}


