using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    public GameObject prefab;
    private InteractiveBox selectedBox;

    private void Update()
    {
        // Левый клик
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Клик по InteractiveBox
                InteractiveBox box = hit.collider.GetComponent<InteractiveBox>();
                if (box != null)
                {
                    if (selectedBox == null)
                    {
                        selectedBox = box;
                    }
                    else if (selectedBox != box)
                    {
                        selectedBox.AddNext(box);
                        selectedBox = null;
                    }
                    return;
                }

                // Клик по плоскости
                if (hit.collider.CompareTag("InteractivePlane"))
                {
                    // Корректное размещение с учетом нормали и размера объекта
                    Vector3 spawnPosition = hit.point + hit.normal * (prefab.transform.localScale.y / 2);
                    Instantiate(prefab, spawnPosition, Quaternion.identity);
                }
            }
        }

        // Правый клик - удаление
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                InteractiveBox box = hit.collider.GetComponent<InteractiveBox>();
                if (box != null)
                {
                    Destroy(box.gameObject);
                    if (selectedBox == box) selectedBox = null;
                }
            }
        }
    }
}