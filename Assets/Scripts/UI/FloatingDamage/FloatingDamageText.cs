using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingDamageText : MonoBehaviour
{
    private TextMeshPro _damageText;
    private float _lifetime = 0.5f;
    private Color _originalColor;

    private void Awake()
    {
        _damageText = GetComponent<TextMeshPro>();
        _originalColor = _damageText.color;
    }

    private void Update()
    {
        UpdateUI();
    }
    
    public void SetDamage(float damage)
    {
        _damageText.text = damage.ToString();
        Destroy(gameObject, _lifetime);
    }

    private void UpdateUI()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
    }
}
