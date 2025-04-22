using UnityEngine;

public class ShowFinishText : MonoBehaviour
{
    public GameObject finishTextPrefab;
    private GameObject currentText;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cherche la caméra du joueur
            Camera cam = Camera.main;
            if (cam == null)
            {
                Debug.LogError("Aucune caméra Main trouvée !");
                return;
            }

            // Position du texte devant la caméra
            Vector3 position = cam.transform.position + cam.transform.forward * 2f + Vector3.up * 0.5f;
            currentText = Instantiate(finishTextPrefab, position, Quaternion.identity);

            // Le texte regarde la caméra
            currentText.transform.LookAt(cam.transform);
            currentText.transform.Rotate(0, 180f, 0);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && currentText != null)
        {
            Destroy(currentText);
        }
    }
}
