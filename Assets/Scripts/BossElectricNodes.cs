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

    // Start is called before the first frame update
    void Start()
    {
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

        StartCoroutine(ElectricAttackSequence());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.down * speed) * Time.deltaTime, Space.World);
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ElectricAttackSequence());
        }
    }

    public IEnumerator ElectricAttackSequence()
    {

        speed = moveSpeed;
        yield return new WaitUntil(() => transform.localPosition.y <= positionTwo);
        speed = 0;
        yield return new WaitForSeconds(3);
        speed = moveSpeed * -1;
        Debug.Log("Changing Speed");
        yield return new WaitUntil(() => transform.localPosition.y >= positionOne);
        speed = 0;
        Debug.Log("EndofRoutine");
        yield return new WaitForSeconds(3);
    }

}
