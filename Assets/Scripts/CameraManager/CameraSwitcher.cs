using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    void Start()
    {
        camera1.gameObject.SetActive(true);
        camera2.gameObject.SetActive(false);
    }

    void Update()
    {
        // ПК: клик мышью по объекту
        if (Input.GetMouseButtonDown(0))
        {
            CheckTapOrClick(Input.mousePosition);
        }

        // Смартфон: тапы
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            CheckTapOrClick(Input.touches[0].position);
        }
    }

    void CheckTapOrClick(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                ToggleCameras();
            }
        }
    }

    void ToggleCameras()
    {
        bool cam1Active = camera1.gameObject.activeSelf;

        camera1.gameObject.SetActive(!cam1Active);
        camera2.gameObject.SetActive(cam1Active);
    }
}