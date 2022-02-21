using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseOne)
            {
                GameManager.TGM.LoadPhaseTwo();
            } else { GameManager.TGM.LoadPhaseThree(); }

        }
    }
}
