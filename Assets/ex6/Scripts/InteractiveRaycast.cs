using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private LayerMask interactivePlaneLayer;
    [SerializeField] private LayerMask interactiveBoxLayer;


    private Camera mainCamera;
    private InteractiveBox selectedBox;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleMouseClicks();
    }

    private void HandleMouseClicks()
    {
        if (Input.GetMouseButtonDown(0)) // Левый клик
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // Сначала проверяем клик по InteractiveBox
            if (Physics.Raycast(ray, out RaycastHit boxHit, Mathf.Infinity, interactiveBoxLayer))
            {
                InteractiveBox clickedBox = boxHit.collider.GetComponent<InteractiveBox>();

                if (selectedBox == null)
                {
                    // Выбираем первый куб
                    selectedBox = clickedBox;
                    Debug.Log("First box selected");
                }
                else if (selectedBox != clickedBox)
                {
                    // Связываем выбранный куб с новым
                    selectedBox.AddNext(clickedBox);
                    Debug.Log("Boxes connected");
                    selectedBox = null;
                }
            }
            // Если не попали по кубу, проверяем плоскость
            else if (Physics.Raycast(ray, out RaycastHit planeHit, Mathf.Infinity, interactivePlaneLayer))
            {
                CreateNewBox(planeHit);
            }
        }

        if (Input.GetMouseButtonDown(1)) // Правый клик
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactiveBoxLayer))
            {
                Destroy(hit.collider.gameObject);
                selectedBox = null; // Сброс выбора если удалили выбранный
            }
        }
    }

    private void CreateNewBox(RaycastHit hit)
    {
        // Учитываем размер префаба
        Bounds prefabBounds = prefab.GetComponent<Renderer>().bounds;
        float offset = prefabBounds.extents.y;

        // Создаем куб с учетом нормали поверхности
        Vector3 spawnPosition = hit.point + hit.normal * offset;
        Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        Instantiate(prefab, spawnPosition, spawnRotation);
    }
}