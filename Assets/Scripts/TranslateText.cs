using TMPro;
using UnityEngine;
using YG;

public class TranslateText : MonoBehaviour
{
    [SerializeField] private string _ruText;
    [SerializeField] private string _enText;

    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        if (YandexGame.lang == "ru")
            _text.text = _ruText;
        else
            _text.text = _enText;
    }
}
