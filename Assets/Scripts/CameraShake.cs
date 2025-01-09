using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _strenght;

    [SerializeField] private GameObject _cameraShakeOn;
    [SerializeField] private GameObject _cameraShakeOff;

    public static CameraShake Instance;

    private void Start()
    {
        Instance = this;

        if (GameSettings.Instance.CameraShakeOn)
        {
            _cameraShakeOn.SetActive(true);
            _cameraShakeOff.SetActive(false);
        }
        else
        {
            _cameraShakeOff.SetActive(true);
            _cameraShakeOn.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventManager.BallCollision += Shake;
    }

    private void OnDisable()
    {
        EventManager.BallCollision -= Shake;
    }

    public void ChangeShakeActive()
    {
        GameSettings.Instance.CameraShakeOn = !GameSettings.Instance.CameraShakeOn;
        GameSettings.Instance.SaveProgress();

        if (GameSettings.Instance.CameraShakeOn)
        {
            _cameraShakeOn.SetActive(true);
            _cameraShakeOff.SetActive(false);
        }
        else
        {
            _cameraShakeOff.SetActive(true);
            _cameraShakeOn.SetActive(false);
        }
    }

    public void Shake()
    {
        if (!GameSettings.Instance.CameraShakeOn) return;

        gameObject.transform.
            DOShakePosition(_duration, _strenght);
    }
}
