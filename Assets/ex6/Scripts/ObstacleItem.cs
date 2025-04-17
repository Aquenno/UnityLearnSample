using UnityEngine;
using UnityEngine.Events;

public class ObstacleItem : MonoBehaviour
{
    [Range(0, 1)] public float currentValue = 1f;
    public UnityEvent onDestroyObstacle; // Событие при разрушении

    private Renderer _renderer;
    private Color _originalColor;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
    }

    private void Update()
    {
        // Плавное изменение цвета от белого (1) к красному (0)
        _renderer.material.color = Color.Lerp(Color.red, _originalColor, currentValue);
    }

    public void GetDamage(float damage)
    {
        currentValue -= damage;

        if (currentValue <= 0)
        {
            onDestroyObstacle.Invoke(); // Запускаем событие
            Destroy(gameObject); // Уничтожаем препятствие
        }
    }
}