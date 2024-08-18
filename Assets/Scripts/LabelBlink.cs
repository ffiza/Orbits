using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LabelBlink : MonoBehaviour
{
    [SerializeField] private float _blinkFrequency;
    private TextMeshProUGUI _text;
    private float _showTime;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        Show();
    }

    private void Update()
    {
        _text.alpha = 0.3f * Mathf.Cos(_blinkFrequency * (Time.unscaledTime - _showTime)) + 0.7f;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        _showTime = Time.unscaledTime;
        gameObject.SetActive(true);
    }
}
