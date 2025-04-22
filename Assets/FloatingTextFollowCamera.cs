using UnityEngine;

public class FloatingTextFollowCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 offset = new Vector3(0, 0.5f, 2f); // distance devant les "yeux" + hauteur

    void Update()
    {
        if (cameraTransform != null)
        {
            transform.position = cameraTransform.position + cameraTransform.forward * offset.z + cameraTransform.up * offset.y;

            // Faire en sorte que le texte regarde la "caméra"
            transform.LookAt(cameraTransform);
            transform.Rotate(0, 180, 0); // éviter que le texte soit inversé
        }
    }
}
