using UnityEngine;

public class ClickObject : MonoBehaviour
{
    private GameObject selectedCube;
    private Vector3 offset;
    private Plane dragPlane;

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

                    // On crée un plan horizontal à la hauteur du cube
                    dragPlane = new Plane(Vector3.up, new Vector3(0, selectedCube.transform.position.y, 0));

                    // Calcul du point d'intersection entre rayon et plan
                    if (dragPlane.Raycast(ray, out float enter))
                    {
                        Vector3 hitPoint = ray.GetPoint(enter);
                        offset = selectedCube.transform.position - hitPoint;
                    }
                }
            }
        }

        // Déplacement du cube
        if (Input.GetMouseButton(0) && selectedCube != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                selectedCube.transform.position = hitPoint + offset;
            }
        }

        // Relâchement de la souris
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedCube.activeSelf && selectedCube)
            {
                FindObjectOfType<EvaluationManager>().RegisterError();
            }
            selectedCube = null;
            
        }
    }

}
