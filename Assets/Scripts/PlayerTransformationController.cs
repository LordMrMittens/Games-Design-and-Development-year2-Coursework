using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationController : MonoBehaviour
{
    [SerializeField] GameObject[] spaceShipModels;
    [SerializeField] GameObject[] sideGuns;
    [SerializeField] GameObject[] tankModels;
    [SerializeField] ParticleSystem transformParticles;
    public bool sidegunsActive;
    bool hasDoneTransformation=false;
    // Start is called before the first frame update
    void Start()
    {
        ResetPlayerTransformation();
    }

    private void ResetPlayerTransformation()
    {
        sidegunsActive = false;
        if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseOne)
        {
            TransformIntoTank();
        }
        else
        {
            TransformIntoShip();
        }
    }

    public void TransformIntoShip()
    {
        foreach (GameObject shipPart in spaceShipModels)
        {

                shipPart.SetActive(true);
            
        }
        if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseOne && !hasDoneTransformation)
        {
            Instantiate(transformParticles, transform.position, Quaternion.identity);
            hasDoneTransformation = true;
        }
        foreach (GameObject tankPart in tankModels)
        {
            tankPart.SetActive(false);
        }
        DisableSideGuns();
    }

    public void TransformIntoTank()
    {
        foreach (GameObject shipPart in spaceShipModels)
        {
            shipPart.SetActive(false);
        }
        foreach (GameObject tankPart in tankModels)
        {
            tankPart.SetActive(true);
            
        }
        DisableSideGuns();
    }
    public void ActivateSideGuns()
    {
        foreach (GameObject sideGun in sideGuns)
        {
            sideGun.SetActive(true);
        }
    }
    public void DisableSideGuns()
    {
        foreach (GameObject sideGun in sideGuns)
        {
            sideGun.SetActive(false);
        }
    }
}
