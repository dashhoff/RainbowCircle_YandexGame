using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Space(20f)]
    public static Ball Instance;

    [Space(20f)]
    [Header("Параметры")]
    public float LifeTime;
    [SerializeField] private float _maxSpeed;

    [Space(20f)]
    [SerializeField] private Rigidbody2D _rb2D;

    [Space(20f)]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        Instance = this;

        _rb2D = GetComponent<Rigidbody2D>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void SetParameters()
    {
        _maxSpeed = GameSettings.Instance.MaxSpeed;

        LifeTime = GameSettings.Instance.MaxLifeTime;
    }

    private void StartGame()
    {
        RandomPosition.Instance.RandomizePosition();

        SetParameters();

        _rb2D.gravityScale = 1;

        StartCoroutine(LifeTimeCoroutine());
    }

    private void FixedUpdate()
    {
        if (_rb2D.linearVelocity.magnitude > _maxSpeed)
            _rb2D.linearVelocity = _rb2D.linearVelocity.normalized * _maxSpeed;
    }

    private void OnEnable()
    {
        EventManager.StartingGame += StartGame;
    }

    private void OnDisable()
    {
        EventManager.StartingGame -= StartGame;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EventManager.Instance.BallCollided();

        if (collision.contacts.Length > 0)
        {
            Vector2 contactPoint = collision.contacts[0].point;
            FXManager.Instance.BallCollided(contactPoint);
        }

        GameSettings.Instance.Money++;
    }

    private IEnumerator LifeTimeCoroutine()
    {
        while (true)
        {
            if (Time.timeScale != 1)
                continue;

            if (LifeTime <= 0)
            {
                EventManager.Instance.EndGame();

                _rb2D.simulated = false;

                break;
            }

            yield return new WaitForSeconds(1);

            LifeTime--;

            UIManager.Instance.UpdateLifeTimeText();
        }
    }
}
