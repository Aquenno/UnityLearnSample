using UnityEngine;

public class CloneScript : SampleScript
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int cloneCount = 5;
    [SerializeField] private float stepDistance = 1f;
    [SerializeField] private Vector3 direction = Vector3.right;


    public override void Use()
    {
        for (int i = 0; i < cloneCount; i++)
        {
            Vector3 position = transform.position + direction * (i + 1) * stepDistance;
            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}