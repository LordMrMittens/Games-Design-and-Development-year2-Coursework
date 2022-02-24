using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElectricNodes : MonoBehaviour
{
    public float positionOne;
    public float positionTwo;
    public float positionTwoOffset;
    public float moveSpeed;
    float speed;
    public bool isParentNode;
    BossController bossController;
    // Start is called before the first frame update
    void Start()
    {
        bossController = GetComponentInParent<BossController>();
        speed = 0;
        positionOne = transform.localPosition.y;
        if (isParentNode)
        {
            positionTwo = positionOne - positionTwoOffset;
            
        } else
        {
            positionTwo = gameObject.GetComponentInParent<BossElectricNodes>().positionTwo - positionTwoOffset;
            moveSpeed /= gameObject.GetComponentInParent<BossElectricNodes>().moveSpeed;
        }
    }
    void Update()
    {
        transform.Translate((Vector3.down * speed) * Time.deltaTime, Space.World);
    }
    public void ElectricAttack()
    {
        StartCoroutine(ElectricAttackSequence());
    }

    IEnumerator ElectricAttackSequence()
    {
        bossController.electricTimer = -1000;
        bossController.electricIsReady = false;
        speed = moveSpeed;
        yield return new WaitUntil(() => transform.localPosition.y <= positionTwo);
        speed = 0;
        yield return new WaitForSeconds(3);
        speed = moveSpeed * -1;
        
        yield return new WaitUntil(() => transform.localPosition.y >= positionOne);
        speed = 0;
        
        yield return new WaitForSeconds(3);
        bossController.electricTimer = 0;
        bossController.electricIsReady = false;
        
    }

}
