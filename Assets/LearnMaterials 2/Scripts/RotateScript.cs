using UnityEngine;

public class RotateScript : SampleScript
{
    [SerializeField] private Vector3 rotationAngles = new Vector3(0f, 90f, 0f);
    [SerializeField] private float rotationSpeed = 10f;

    private Quaternion startRotation;
    private bool isRotating = false;
    private float progress = 0f;

    private void Start()
    {
        startRotation = transform.rotation;
    }


    public override void Use()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCoroutine());
        }
    }

    private System.Collections.IEnumerator RotateCoroutine()
    {
        isRotating = true;
        progress = 0f;

        Quaternion targetRotation = startRotation * Quaternion.Euler(rotationAngles);
        float totalRotation = Quaternion.Angle(startRotation, targetRotation);
        float duration = totalRotation / rotationSpeed;

        while (progress < 1f)
        {
            progress += Time.deltaTime / duration;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);
            yield return null;
        }

        isRotating = false;
    }
}