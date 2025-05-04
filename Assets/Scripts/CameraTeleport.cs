using UnityEngine;


public class CameraTeleport : MonoBehaviour
{
    public int level;
    public void TeleportToPosition1()
    {
        transform.position = new Vector3(21.92f, 8.5f, 0.79f);
        transform.rotation = Quaternion.Euler(32.266f, 90f, 1.062f);
        level = 1;

    }

    public void TeleportToPosition2()
    {
        transform.position = new Vector3(8.3f, 9.1f, -26.19999f);
        transform.rotation = Quaternion.Euler(32.266f, -180f, 1.062f);
        level = 2;
    }

    public void TeleportToPosition3()
    {
        transform.position = new Vector3(-8.57f, 9.04f, 6.58f);
        transform.rotation = Quaternion.Euler(39.555f, -90f, 1.062f);
        level = 3;
    }
}
