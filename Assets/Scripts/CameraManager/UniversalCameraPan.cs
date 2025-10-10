using UnityEngine;

public class UniversalCameraPan : MonoBehaviour
{
    public float panSpeed = 1f;
    public float dragDepth = 10f; // расстояние от камеры до "плоскости" карты

    private Vector3? lastWorldPoint;

    void Update()
    {
        // 🖱️ Мышь
        if (Input.GetMouseButtonDown(0))
            lastWorldPoint = GetWorldPoint(Input.mousePosition);

        else if (Input.GetMouseButton(0) && lastWorldPoint.HasValue)
        {
            Vector3 currentWorldPoint = GetWorldPoint(Input.mousePosition);
            Vector3 delta = lastWorldPoint.Value - currentWorldPoint;
            transform.position += delta;
        }

        else if (Input.GetMouseButtonUp(0))
            lastWorldPoint = null;

        // 📱 Тач
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                lastWorldPoint = GetWorldPoint(touch.position);

            else if (touch.phase == TouchPhase.Moved && lastWorldPoint.HasValue)
            {
                Vector3 currentWorldPoint = GetWorldPoint(touch.position);
                Vector3 delta = lastWorldPoint.Value - currentWorldPoint;
                transform.position += delta;
            }

            else if (touch.phase == TouchPhase.Ended)
                lastWorldPoint = null;
        }
    }

    Vector3 GetWorldPoint(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        return ray.GetPoint(dragDepth); // точка на луче на заданной глубине
    }
}
