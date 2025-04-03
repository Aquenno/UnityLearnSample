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
        if (Input.GetMouseButtonDown(0)) // ����� ����
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // ������� ��������� ���� �� InteractiveBox
            if (Physics.Raycast(ray, out RaycastHit boxHit, Mathf.Infinity, interactiveBoxLayer))
            {
                InteractiveBox clickedBox = boxHit.collider.GetComponent<InteractiveBox>();

                if (selectedBox == null)
                {
                    // �������� ������ ���
                    selectedBox = clickedBox;
                    Debug.Log("First box selected");
                }
                else if (selectedBox != clickedBox)
                {
                    // ��������� ��������� ��� � �����
                    selectedBox.AddNext(clickedBox);
                    Debug.Log("Boxes connected");
                    selectedBox = null;
                }
            }
            // ���� �� ������ �� ����, ��������� ���������
            else if (Physics.Raycast(ray, out RaycastHit planeHit, Mathf.Infinity, interactivePlaneLayer))
            {
                CreateNewBox(planeHit);
            }
        }

        if (Input.GetMouseButtonDown(1)) // ������ ����
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactiveBoxLayer))
            {
                Destroy(hit.collider.gameObject);
                selectedBox = null; // ����� ������ ���� ������� ���������
            }
        }
    }

    private void CreateNewBox(RaycastHit hit)
    {
        // ��������� ������ �������
        Bounds prefabBounds = prefab.GetComponent<Renderer>().bounds;
        float offset = prefabBounds.extents.y;

        // ������� ��� � ������ ������� �����������
        Vector3 spawnPosition = hit.point + hit.normal * offset;
        Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        Instantiate(prefab, spawnPosition, spawnRotation);
    }
}