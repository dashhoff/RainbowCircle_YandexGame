using UnityEditor;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    public static RandomPosition Instance;

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private Transform target;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RandomizePosition()
    {
        float minX = Mathf.Min(pointA.position.x, pointB.position.x);
        float maxX = Mathf.Max(pointA.position.x, pointB.position.x);
        float minZ = Mathf.Min(pointA.position.y, pointB.position.y);
        float maxZ = Mathf.Max(pointA.position.y, pointB.position.y);

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minZ, maxZ);

        target.position = new Vector3(randomX, randomY, target.position.z);
    }

    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, new Vector3(pointB.position.x, pointA.position.y, pointA.position.z));
            Gizmos.DrawLine(pointA.position, new Vector3(pointA.position.x, pointB.position.y, pointB.position.z));
            Gizmos.DrawLine(pointB.position, new Vector3(pointA.position.x, pointB.position.y, pointB.position.z));
            Gizmos.DrawLine(pointB.position, new Vector3(pointB.position.x, pointA.position.y, pointA.position.z));
        }
    }
}
