using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public TimerManager timerManager;
    public EndGameDisplay endGameDisplay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerManager.StopTimer();
            endGameDisplay.ShowEndGameResults();
        }
    }
}
