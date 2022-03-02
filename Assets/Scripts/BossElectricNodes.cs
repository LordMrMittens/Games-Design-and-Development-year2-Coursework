using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElectricNodes : Gun
{
    public float positionOne;
    public float positionTwo;
    public float positionTwoOffset;
    public float moveSpeed;
    float speed;
    BossController bossController;
    [SerializeField] float electricAttackDuration;
    [SerializeField] float electricAttackTimer=0;
    [SerializeField] Transform[] targets;
    bool isAttackingelectric;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        bossController = GetComponentInParent<BossController>();
        speed = 0;
        positionOne = transform.localPosition.y;
            positionTwo = positionOne - positionTwoOffset;
    }
    new void Update()
    {
        transform.Translate((Vector3.down * speed) * Time.deltaTime, Space.World);
        if (isAttackingelectric)
        {
            electricAttackTimer += Time.deltaTime;
        }
    }
    public void ElectricAttack()
    {
        if (gameObject.activeInHierarchy == true)
        {
            electricAttackTimer = 0;
            StartCoroutine(ElectricAttackSequence());
        }
    }
    public void Shoot(Transform target)
    {
        GameObject bullet = ObjectPooler.pooler.GetPooledObject(ObjectPooler.pooler.pooledBossBullets);
        if (bullet != null)
        {
            bullet.transform.position = cannon.transform.position;
            bullet.transform.rotation = cannon.transform.rotation;
            bullet.SetActive(true);
            RotateAroundProjectile bulletManager = bullet.GetComponent<RotateAroundProjectile>();
            bullet.GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * (maxBulletSpeed*3), ForceMode.Impulse);
            bulletManager.damage = damage;
        }
    }

    IEnumerator ElectricAttackSequence()
    {
        bossController.electricTimer = -1000;
        bossController.electricIsReady = false;
        speed = moveSpeed;
        yield return new WaitUntil(() => transform.localPosition.y <= positionTwo);
        speed = 0;
        yield return new WaitForSeconds(1.5f);
        isAttackingelectric = true;
        while (electricAttackTimer < electricAttackDuration)
        {
            foreach (var target in targets)
            {
                Shoot(target);
            }
            yield return new WaitForSeconds(.05f);
        }
        isAttackingelectric = false;
        yield return new WaitForSeconds(3);
        speed = moveSpeed * -1;
        
        yield return new WaitUntil(() => transform.localPosition.y >= positionOne);
        speed = 0;
        
        yield return new WaitForSeconds(3);
        bossController.electricTimer = 0;
        bossController.electricIsReady = false;
        
    }

}
