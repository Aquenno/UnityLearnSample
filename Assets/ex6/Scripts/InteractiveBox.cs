using UnityEngine;

public class InteractiveBox : MonoBehaviour, IConnectable
{
    [SerializeField] private InteractiveBox next;
    [SerializeField] private Color lineColor = Color.green;

    private void Update()
    {
        if (next != null)
        {
            // Отрисовка луча в редакторе
            Debug.DrawLine(transform.position, next.transform.position, lineColor);

            // Проверка на препятствия
            CheckForObstacles();
        }
    }

    public void AddNext(InteractiveBox box)
    {
        if (next != null && next != box)
        {
            Debug.LogWarning("This box already has a next connection!");
            return;
        }

        next = box;
    }

    private void CheckForObstacles()
    {
        RaycastHit hit;
        Vector3 direction = next.transform.position - transform.position;
        float distance = Vector3.Distance(transform.position, next.transform.position);

        if (Physics.Raycast(transform.position, direction, out hit, distance))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.GetDamage(Time.deltaTime); // Урон распределенный по времени
            }
        }
    }
}

public interface IConnectable
{
    void AddNext(InteractiveBox box);
}