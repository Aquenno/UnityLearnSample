using System.Collections;
using UnityEngine;

public class ShrinkDestroyScript : SampleScript
{
    [SerializeField] private Transform target;
    [SerializeField] private float shrinkSpeed = 1f;


    public override void Use()
    {
        if (target != null)
        {
            StartCoroutine(ShrinkAndDestroyCoroutine());
        }
    }

    private IEnumerator ShrinkAndDestroyCoroutine()
    {
        // ������� ����� ������ �������� ��������
        Transform[] children = new Transform[target.childCount];
        for (int i = 0; i < target.childCount; i++)
        {
            children[i] = target.GetChild(i);
        }

        // ������� ������
        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * shrinkSpeed;
            foreach (Transform child in children)
            {
                if (child != null)
                {
                    child.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, progress);
                }
            }
            yield return null;
        }

        // �������� ��������
        foreach (Transform child in children)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }
    }
}