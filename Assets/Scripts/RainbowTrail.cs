using UnityEngine;

public class RainbowTrail : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab; // ������ �����

    [SerializeField] private float spawnInterval;
    [SerializeField] private float colorChangeSpeed;

    private float spawnTimer;
    private float hue; // �������� ����� � ������� HSV
    private bool isSpawning = false; // ���� ��������� ������

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

        // ������ ������ ������
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnCircle();
            spawnTimer = 0f;
        }

        // ���������� �������� �����
        hue += colorChangeSpeed * Time.deltaTime;
        if (hue > 1f) hue -= 1f;
    }

    private void SpawnCircle()
    {
        if (circlePrefab == null) return;

        // ����� ����� � ������� �������
        GameObject circle = Instantiate(circlePrefab, transform.position, Quaternion.identity);

        // ��������� �����
        Color color = Color.HSVToRGB(hue, 1f, 1f); // ������� �������� HSV � RGB
        SpriteRenderer spriteRenderer = circle.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }
}
