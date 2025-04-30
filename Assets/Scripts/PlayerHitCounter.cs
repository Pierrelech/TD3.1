using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCounter : MonoBehaviour
{
    public int collisionCount = -2;
    public EndGameDisplay endGameDisplay;
    private HashSet<GameObject> alreadyTouched = new HashSet<GameObject>();

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!alreadyTouched.Contains(hit.gameObject))
        {
            collisionCount++;
            alreadyTouched.Add(hit.gameObject);
            endGameDisplay.OnCollisionDetected();

            Debug.Log("Touché : " + hit.gameObject.name + " | Total : " + collisionCount);
        }
    }
}
