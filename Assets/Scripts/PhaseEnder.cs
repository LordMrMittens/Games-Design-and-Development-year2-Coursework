using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseEnder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseTwo)
            {
                GameManager.TGM.EndPhaseTwo();
            }
            

        }
    }
}
