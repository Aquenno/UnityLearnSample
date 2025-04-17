using System.Diagnostics;
using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    [HideInInspector] public InteractiveBox next;
    private LineRenderer lineRenderer;

    private void Start()
    {
        // Инициализация LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Standard")) { color = Color.blue };
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (next != null)
        {
            // Отрисовка луча
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, next.transform.position);


            // Проверка попадания в препятствия
            RaycastHit hit;
            Vector3 direction = next.transform.position - transform.position;
            if (Physics.Raycast(transform.position, direction, out hit, direction.magnitude))
            {
                ObstacleItem obstacle = hit.collider.GetComponent<ObstacleItem>();
                if (obstacle != null)
                {
                    obstacle.GetDamage(Time.deltaTime);
                }
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public void AddNext(InteractiveBox box)
    {
        if (box == this) return; // Нельзя назначить самого себя
        next = box;
    }
}