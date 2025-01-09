using UnityEngine;

public class FXManager : MonoBehaviour
{
    [SerializeField] private GameObject _ballCollisionFX;

    [SerializeField] private GameObject _fxOnIcon;
    [SerializeField] private GameObject _fxOffIcon;

    public static FXManager Instance;

    private void Start()
    {
        Instance = this;

        if (GameSettings.Instance.FxOn)
        {
            _fxOnIcon.SetActive(true);
            _fxOffIcon.SetActive(false);
        }
        else
        {
            _fxOffIcon.SetActive(true);
            _fxOnIcon.SetActive(false);
        }
    }

    public void ChangeFxActive()
    {
        GameSettings.Instance.FxOn = !GameSettings.Instance.FxOn;
        GameSettings.Instance.SaveProgress();

        if (GameSettings.Instance.FxOn)
        {
            _fxOnIcon.SetActive(true);
            _fxOffIcon.SetActive(false);
        }
        else
        {
            _fxOffIcon.SetActive(true);
            _fxOnIcon.SetActive(false);
        }
    }

    public void BallCollided(Vector3 pos)
    {
        if (!GameSettings.Instance.FxOn) return;

        Instantiate(_ballCollisionFX, pos, Quaternion.identity);
    }
}
