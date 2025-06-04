using UnityEngine;

public class MobileCameraController : MonoBehaviour
{
    public float panSpeed = 0.5f;
    public float zoomSpeed = 0.1f;
    public float minZoom = 4f;
    public float maxZoom = 25f;

    private Camera cam;
    private Vector2 lastPanPosition;
    private int panFingerId; // Touch finger ID
    private bool isPanning;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (!cam.orthographic) cam.orthographic = true;
        cam.orthographicSize = Mathf.Clamp(10f, minZoom, maxZoom);
        cam.transform.position = new Vector3(0, 0, -10f);
    }

    void Update()
    {
        HandleTouch();
        HandleMouseEditor();
    }

    void HandleTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastPanPosition = touch.position;
                panFingerId = touch.fingerId;
                isPanning = true;
            }
            else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved && isPanning)
            {
                PanCamera(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isPanning = false;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);

            Vector2 prevPos1 = t1.position - t1.deltaPosition;
            Vector2 prevPos2 = t2.position - t2.deltaPosition;

            float prevDist = Vector2.Distance(prevPos1, prevPos2);
            float currentDist = Vector2.Distance(t1.position, t2.position);

            float delta = prevDist - currentDist;
            ZoomCamera(delta * zoomSpeed);
        }
    }

    void HandleMouseEditor()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1))
        {
            lastPanPosition = Input.mousePosition;
            isPanning = true;
        }
        else if (Input.GetMouseButton(1) && isPanning)
        {
            PanCamera(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isPanning = false;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            ZoomCamera(-scroll * 10f);
        }
#endif
    }

    void PanCamera(Vector2 newPanPosition)
    {
        Vector3 delta = cam.ScreenToWorldPoint(lastPanPosition) - cam.ScreenToWorldPoint(newPanPosition);
        cam.transform.Translate(delta, Space.World);
        lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + increment, minZoom, maxZoom);
    }
}
