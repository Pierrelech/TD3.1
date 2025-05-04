using System.Collections;
using UnityEngine;

public class ObjectClick : MonoBehaviour
{
    public float force = 5;
    public Animator anim;
    private bool etatF = true;
    private bool etatO = false;
    private bool interract1 = false;
    private bool interract2 = false;

    public DragObject dragObjectScript; // référence à DragObject

    private bool canUse = true; // bloque l'accès à Manage pendant un petit temps

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            var rig = selection.GetComponent<Rigidbody>();

            // Vérifier si l'objet a un Rigidbody avant de l'utiliser
            if (rig != null)
            {
                if (hit.collider.gameObject.name == "Cube1")
                {
                    if (Input.GetMouseButton(0))
                    {
                        rig.AddForce(Camera.main.transform.forward * 10);
                        interract1 = true;
                    }
                }

                if (hit.collider.gameObject.name == "Cube")
                {
                    if (Input.GetMouseButton(0))
                    {
                        rig.AddForce(rig.transform.up * force, ForceMode.Impulse);
                        interract2 = true;
                    }
                }
            }

            // Vérifie séparément la porte sans essayer d'utiliser Rigidbody
            if (hit.collider.gameObject.name == "Porte" && interract1 && interract2 && dragObjectScript.interract3)
            {
                if (Input.GetMouseButtonDown(0) && canUse)
                {
                    StartCoroutine(Manage());
                }
            }
        }
    }


    IEnumerator Manage()
    {
        canUse = false;

        if (etatF)
        {
            anim.Play("DoorOpen");
            etatF = false;
            etatO = true;
        }
        else if (etatO)
        {
            anim.Play("DoorClose");
            etatO = false;
            etatF = true;
        }

        yield return new WaitForSeconds(0.2f);
        canUse = true;
    }
}
