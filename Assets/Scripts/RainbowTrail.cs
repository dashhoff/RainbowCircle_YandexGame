using UnityEngine;

public class RainbowTrail : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab; // Префаб круга

    [SerializeField] private float spawnInterval;
    [SerializeField] private float colorChangeSpeed;

    private float spawnTimer;
    private float hue; // Значение цвета в формате HSV
    private bool isSpawning = false; // Флаг активации спавна

    public static RainbowTrail Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        SetParameters();
    }

    private void SetParameters()
    {
        spawnInterval = GameSettings.Instance.SpawnInterval;
        colorChangeSpeed = GameSettings.Instance.ColorInterval;
    }

    private void OnEnable()
    {
        EventManager.StartingGame += StartSpawning;
        EventManager.StartingGame += SetParameters;
        EventManager.EndingAd += StartSpawning;

        EventManager.EndingGame += StopSpawning;
        EventManager.StartingAd += StopSpawning;
    }

    private void OnDisable()
    {
        EventManager.StartingGame -= StartSpawning;
        EventManager.StartingGame -= SetParameters;
        EventManager.EndingAd -= StartSpawning;

        EventManager.EndingGame -= StopSpawning;
        EventManager.StartingAd -= StopSpawning;
    }

    private void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private void Update()
    {
        if (!isSpawning) return;

        // Таймер спавна кругов
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnCircle();
            spawnTimer = 0f;
        }

        // Обновление значения цвета
        hue += colorChangeSpeed * Time.deltaTime;
        if (hue > 1f) hue -= 1f;
    }

    private void SpawnCircle()
    {
        if (circlePrefab == null) return;

        // Спавн круга в позиции объекта
        GameObject circle = Instantiate(circlePrefab, transform.position, Quaternion.identity);

        // Установка цвета
        Color color = Color.HSVToRGB(hue, 1f, 1f); // Перевод значения HSV в RGB
        SpriteRenderer spriteRenderer = circle.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }
}
