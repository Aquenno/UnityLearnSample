using UnityEngine;

public class MoveScript : SampleScript
{
    [SerializeField] private Vector3 targetPosition = new Vector3(3f, 0f, 0f);
    [SerializeField] private float speed = 1f;

    private Vector3 startPosition;
    private bool isMoving = false;
    private float progress = 0f;

    private void Start()
    {
        startPosition = transform.position;
    }


    public override void Use()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine());
        }
    }

    private System.Collections.IEnumerator MoveCoroutine()
    {
        isMoving = true;
        progress = 0f;

        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / speed;

        while (progress < 1f)
        {
            progress += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            yield return null;
        }

        isMoving = false;
    }
}