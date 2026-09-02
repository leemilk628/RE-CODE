using UnityEngine;

public class ReflectionCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera reflectionCamera;
    [SerializeField] private float waterY = 0f;

    private void LateUpdate()
    {
        Vector3 mainPos = mainCamera.transform.position;

        float mirroredY = 2f * waterY - mainPos.y;

        reflectionCamera.transform.position =
            new Vector3(mainPos.x, mirroredY, mainPos.z);

        reflectionCamera.transform.rotation = mainCamera.transform.rotation;
        reflectionCamera.transform.localScale = new Vector3(1f, -1f, 1f);

        if (mainCamera.orthographic)
        {
            reflectionCamera.orthographic = true;
            reflectionCamera.orthographicSize = mainCamera.orthographicSize;
        }
    }
}