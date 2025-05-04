using UnityEngine;

public class EndZoneChecker : MonoBehaviour
{
    [SerializeField] private GameObject assignedCube;
    [SerializeField] private GameObject ShowCube;
    [SerializeField] private float tolerance = 0.01f;

    private Collider zoneCollider;

    void Awake()
    {
        zoneCollider = GetComponent<Collider>();
    }

    void Update()
    {
        // Lors du relâchement de la souris
        if (Input.GetMouseButtonUp(0))
        {
            if (IsCubeInside(assignedCube))
            {
                Debug.Log($"Cube {assignedCube.name} bien placé dans sa zone !");
                FindObjectOfType<EvaluationManager>().RegisterSuccess();

                assignedCube.SetActive(false); // ou autre action
                ShowCube.SetActive(true);
            }
        }
    }

    private bool IsCubeInside(GameObject cube)
    {
        Bounds zoneBounds = zoneCollider.bounds;
        Bounds cubeBounds = cube.GetComponent<Collider>().bounds;

        Vector3[] bottomCorners = new Vector3[4];
        bottomCorners[0] = new Vector3(cubeBounds.min.x, cubeBounds.min.y, cubeBounds.min.z);
        bottomCorners[1] = new Vector3(cubeBounds.max.x, cubeBounds.min.y, cubeBounds.min.z);
        bottomCorners[2] = new Vector3(cubeBounds.min.x, cubeBounds.min.y, cubeBounds.max.z);
        bottomCorners[3] = new Vector3(cubeBounds.max.x, cubeBounds.min.y, cubeBounds.max.z);

        foreach (Vector3 corner in bottomCorners)
        {
            if (!zoneBounds.Contains(corner + Vector3.one * tolerance))
                return false;
        }

        return true;
    }
}
