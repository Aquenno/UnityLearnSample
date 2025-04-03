using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]
public class ObstacleItem : MonoBehaviour, IDamageable
{
    [Range(0, 1)]
    [SerializeField] private float currentValue = 1f;
    [SerializeField] private UnityEvent onDestroyObstacle;

    private Renderer obstacleRenderer;
    private Color originalColor;

    private void Awake()
    {
        obstacleRenderer = GetComponent<Renderer>();
        originalColor = obstacleRenderer.material.color;
        UpdateColor();
    }

    public void GetDamage(float value)
    {
        if (currentValue <= 0) return; // Уже уничтожен

        currentValue = Mathf.Clamp(currentValue - value, 0, 1);
        UpdateColor();

        if (currentValue <= 0)
        {
            onDestroyObstacle?.Invoke();
            Destroy(gameObject);
        }
    }

    private void UpdateColor()
    {
        // Плавный переход от белого (1) к красному (0)
        obstacleRenderer.material.color = Color.Lerp(Color.red, originalColor, currentValue);
    }
}

public interface IDamageable
{
    void GetDamage(float value);
}