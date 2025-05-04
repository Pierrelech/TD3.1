using UnityEngine;

public class ClickObject : MonoBehaviour
{
    private GameObject selectedCube;
    private Vector3 offset;
    private Plane dragPlane;

    public float scrollSpeed = 2.0f; // Vitesse de déplacement sur l'axe Y

    void Update()
    {
        // Clic souris : sélection du cube
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Draggable"))
                {
                    FindObjectOfType<EvaluationManager>().RegisterClick();
                    selectedCube = hit.collider.gameObject;

                    // On crée un plan horizontal à la hauteur ACTUELLE du cube sélectionné
                    dragPlane = new Plane(Vector3.up, selectedCube.transform.position);

                    if (dragPlane.Raycast(ray, out float enter))
                    {
                        Vector3 hitPoint = ray.GetPoint(enter);
                        offset = selectedCube.transform.position - hitPoint;
                    }
                }
            }
        }

        // Déplacement du cube avec la souris (seulement X et Z)
        if (Input.GetMouseButton(0) && selectedCube != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 targetPos = hitPoint + offset;
                targetPos.y = selectedCube.transform.position.y; // On garde Y inchangé pendant le drag
                selectedCube.transform.position = targetPos;
            }
        }

        // Déplacement avec la molette
        if (selectedCube != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f) // évite les petites valeurs inutiles
            {
                Vector3 pos = selectedCube.transform.position;
                pos.y += scroll * scrollSpeed;
                selectedCube.transform.position = pos;

                // Pas besoin de recréer le dragPlane ici !
            }
        }

        // Relâchement de la souris
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedCube)
            {
                if (selectedCube.activeSelf)
                {
                    FindObjectOfType<EvaluationManager>().RegisterError();
                }
            }
            selectedCube = null;
        }
    }
}
