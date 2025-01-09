using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<DontDestroy>().Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
}
