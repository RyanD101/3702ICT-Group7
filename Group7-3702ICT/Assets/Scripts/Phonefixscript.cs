using UnityEngine;

[ExecuteInEditMode]
public class QuadFrustumFitter : MonoBehaviour
{
    [Tooltip("The camera whose frustum we want to match")]
    public Camera targetCamera;
    [Tooltip("How far in front of the camera the quad should sit")]
    public float distance = 0.5f;

    void LateUpdate()
    {
        if (targetCamera == null) targetCamera = Camera.main;

        // 1) Compute world‐space positions of the viewport corners
        Vector3 bl = targetCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 tr = targetCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));
        Vector3 center = targetCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distance));

        // 2) Move & face the quad
        transform.position = center;
        transform.rotation = targetCamera.transform.rotation;

        // 3) Scale it so that its corners lie exactly at bl and tr
        Vector3 size = tr - bl;
        transform.localScale = new Vector3(size.x, size.y, 1);
    }
}
