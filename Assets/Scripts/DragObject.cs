using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    public bool interract3 = false;

    void OnMouseDown()
    {
        // Sauvegarder la position Z de l'objet par rapport à la caméra
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;

        // Calculer l'offset entre la position de l'objet et celle de la souris
        offset = transform.position - GetMouseWorldPos();
        interract3 = true;
    }

    void OnMouseDrag()
    {
        // Déplacer l'objet en suivant la souris avec l'offset
        transform.position = GetMouseWorldPos() + offset;
        interract3 = true;
    }

    // Convertir la position de la souris en position dans le monde (3D)
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        // Utiliser la coordonnée Z capturée pour la profondeur
        mousePoint.z = zCoord;
        interract3 = true;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
