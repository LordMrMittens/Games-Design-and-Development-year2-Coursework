using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
   [SerializeField] CityController cityController;
    private void OnTriggerEnter(Collider other)
    {
        MainMenuManager menuManager = GameManager.TGM.gameObject.GetComponent<MainMenuManager>();
        if (other.tag == "Player")
        {
            if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseOne)
            {
                menuManager.LoadScreenTwo();
            } else { menuManager.LoadScreenThree(); }

        }
    }
}
