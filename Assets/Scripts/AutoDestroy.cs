using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float _timeToLife;

    private void Start()
    {
        Destroy(gameObject, _timeToLife);
    }
}
