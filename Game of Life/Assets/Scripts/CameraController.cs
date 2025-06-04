using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 10f;
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 40f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam.orthographic == false)
            cam.orthographic = true;
    }

    void Update()
    {
        // WASD for camera pan
        float moveX = 0, moveY = 0;
        if (Input.GetKey(KeyCode.W)) moveY = 1;
        if (Input.GetKey(KeyCode.S)) moveY = -1;
        if (Input.GetKey(KeyCode.A)) moveX = -1;
        if (Input.GetKey(KeyCode.D)) moveX = 1;

        Vector3 pan = new Vector3(moveX, moveY, 0f) * panSpeed * Time.deltaTime;
        transform.position += pan;

        // Right mouse drag for camera pan
        if (Input.GetMouseButton(1))
        {
            float dx = -Input.GetAxis("Mouse X") * panSpeed * 0.1f;
            float dy = -Input.GetAxis("Mouse Y") * panSpeed * 0.1f;
            transform.position += new Vector3(dx, dy, 0f);
        }

        // Zoom with mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            float newZoom = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
            cam.orthographicSize = newZoom;
        }
    }
}
