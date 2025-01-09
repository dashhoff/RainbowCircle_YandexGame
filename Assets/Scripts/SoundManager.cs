using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Space(20)]
    [SerializeField] private GameObject _ballCollisionSound;
    [SerializeField] private float _ball_minPitch;
    [SerializeField] private float _ball_maxPitch;

    [Space(20)]
    [SerializeField] private GameObject _soundOnIcon;
    [SerializeField] private GameObject _soundOffIcon;

    [Space(20)]
    [SerializeField] private GameObject _buySound;
    [SerializeField] private GameObject _buyErrorSound;

    [Space(20)]
    [SerializeField] private GameObject _uiSound;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (GameSettings.Instance.GameMoted)
        {
            _soundOnIcon.SetActive(false);
            _soundOffIcon.SetActive(true);
        }
        else
        {
            _soundOnIcon.SetActive(true);
            _soundOffIcon.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventManager.BallCollision += BallCollided;
    }

    private void OnDisable()
    {
        EventManager.BallCollision -= BallCollided;
    }

    public void ChangeGameMuted()
    {
        GameSettings.Instance.GameMoted = !GameSettings.Instance.GameMoted;
        GameSettings.Instance.SaveProgress();

        if (GameSettings.Instance.GameMoted)
        {
            _soundOnIcon.SetActive(false);
            _soundOffIcon.SetActive(true);
        }
        else
        {
            _soundOnIcon.SetActive(true);
            _soundOffIcon.SetActive(false);
        }
    }

    public void BuySound()
    {
        if (GameSettings.Instance.GameMoted) return;

        Instantiate(_buySound);
    }

    public void BuyErrorSound()
    {
        if (GameSettings.Instance.GameMoted) return;

        Instantiate(_buyErrorSound);
    }

    public void UISound()
    {
        if (GameSettings.Instance.GameMoted) return;

        Instantiate(_uiSound);
    }

    private void BallCollided()
    {
        if (GameSettings.Instance.GameMoted) return;

        AudioSource ballSound= _ballCollisionSound.GetComponent<AudioSource>();

        ballSound.pitch = Random.RandomRange(_ball_minPitch, _ball_maxPitch);

        Instantiate(ballSound);
    }
}
