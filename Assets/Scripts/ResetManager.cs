using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ResetTaskManager : MonoBehaviour
{
    [System.Serializable]
    public class CubeData
    {
        public GameObject cube;              // Cube � r�initialiser
        public GameObject showCube;          // Objet qui s'affiche une fois le cube bien plac�
        [HideInInspector] public Vector3 startPosition;
        [HideInInspector] public Quaternion startRotation;
    }

    [Header("Cubes � r�initialiser")]
    public List<CubeData> cubes = new List<CubeData>();

    [Header("UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI clickText;
    public TextMeshProUGUI errorText;

    [Header("Compteurs")]
    public float timeElapsed = 0f;
    public int clickCount = 0;
    public int errorCount = 0;

    void Start()
    {
        // Sauvegarder la position de d�part de chaque cube
        foreach (var cube in cubes)
        {
            if (cube.cube != null)
            {
                cube.startPosition = cube.cube.transform.position;
                cube.startRotation = cube.cube.transform.rotation;
            }
        }

        ResetTask();
    }

    public void ResetTask()
    {
        Debug.Log("R�initialisation de la t�che...");

        // R�initialise les compteurs
        timeElapsed = 0f;
        clickCount = 0;
        errorCount = 0;

        // R�initialise chaque cube
        foreach (var cube in cubes)
        {
            if (cube.cube != null)
            {
                cube.cube.SetActive(true);
                cube.cube.transform.position = cube.startPosition;
                cube.cube.transform.rotation = cube.startRotation;
            }

            if (cube.showCube != null)
            {
                cube.showCube.SetActive(false);
            }
        }
        FindObjectOfType<EvaluationManager>().ResetAll();
    }
}
