
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHydra : AgentMover
{
    enum status { Passive, Active, Attached }
    status state;
    public bool isDivided = false;
    public bool isAttached = false;
    [SerializeField] float timeBetweenMovements;
    float movementCounter;
    [SerializeField] GameObject hydraPrefab;
    [SerializeField] int offSpringHealth;
    [SerializeField] float offSpringSizeDividedBy;
    public int damage;
    [SerializeField] float timeBetweenDamage;
    float damageCounter;
    HealthManager playerHealthController;
    PlayerMovementController playerController;
    public override void Start()
    {
        base.Start();
        movementCounter = timeBetweenMovements;
        if (isDivided)
        {
            agent.enabled = true;
            viewDistance = 100;
        }
        else
        {
            agent.enabled = false;
        }
    }
    public override void Update()
    {
        switch (state)
        {
            case status.Passive:
                MoveErratically();
                FindPlayer();
                break;
            case status.Active:
                ChasePlayer(player);
                KeepFacingCenter();
                break;
            case status.Attached:
                damageCounter += Time.deltaTime;
                AttachToPlayer();
                MoveErratically();
                if (damageCounter > timeBetweenDamage)
                {
                    playerHealthController.TakeDamage(damage);
                    damageCounter = 0;
                }
                break;
        }
        if (!GameManager.TGM.playerIsAlive)
        {
            state = status.Passive;
        }
    }
    private void MoveErratically()
    {
        movementCounter -= Time.deltaTime;
        if (movementCounter < 0)
        {
            horizontalMovement = Random.Range(-1, 2);
            verticalMovement = Random.Range(-1, 2);
            movementCounter = timeBetweenMovements;
        }
        Move(horizontalMovement, verticalMovement);
    }
    public override void FindPlayer()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < viewDistance)
            {
                agent.enabled = true;
                viewDistance = 100;
                state = status.Active;
            }
        }
        else
        {
            if (Time.frameCount % searchInterval == 0)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
    public void DivideOnDeath()
    {
        if (!isDivided && !isAttached)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject offspring = Instantiate(hydraPrefab, transform.position, Quaternion.identity);
                EnemyHydra hydraControl = offspring.GetComponent<EnemyHydra>();
                hydraControl.rotation = rotation;
                hydraControl.altitude = altitude;
                hydraControl.isDivided = true;
                hydraControl.damage = damage / 2;
                hydraControl.state = state;
                offspring.GetComponent<HealthManager>().health = offSpringHealth;
                offspring.transform.localScale = transform.localScale / offSpringSizeDividedBy;
                GameManager.TGM.CountEnemyUp();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            agent.enabled = false;
            state = status.Attached;
            playerHealthController = other.GetComponent<HealthManager>();
            playerController = other.GetComponent<PlayerMovementController>();
            isAttached = true;
        }
    }
    void AttachToPlayer()
    {
        rotation = playerController.rotation;
        altitude = playerController.altitude;
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHydra : AgentMover
{
    enum status { Passive, Active, Attached }
    status state;
    public bool isDivided = false;
    public bool isAttached = false;
    [SerializeField] float timeBetweenMovements;
    float movementCounter;
    [SerializeField] GameObject hydraPrefab;
    [SerializeField] int offSpringHealth;
    [SerializeField] float offSpringSizeDividedBy;
    public int givenDamage;
    public int damage;
    [SerializeField] float timeBetweenDamage;
    float damageCounter;
    HealthManager playerHealthController;
    PlayerMovementController playerController;
    public override void Start()
    {
        base.Start();
        
    }
    public void ResetStats()
    {
        base.Start();
        movementCounter = timeBetweenMovements;
        if (isDivided)
        {
            Debug.Log(rotation + "spawned");
            agent.enabled = true;
            viewDistance = 100;
            damage /= 2;
            transform.localScale /= offSpringSizeDividedBy;
            state = status.Passive;
        }
        else
        {
            
            viewDistance = setViewDistance;
            agent.enabled = false;
            isAttached = false;
            isDivided = false;
            state = status.Passive;
            damage = givenDamage;
        }

    }
    void Update()
    {
        
        
        switch (state)
        {
            case status.Passive:
                MoveErratically();
                FindPlayer();
                break;
            case status.Active:
                  
                ChasePlayer(player);
                break;
            case status.Attached:
                damageCounter += Time.deltaTime;
                AttachToPlayer();
                MoveErratically();
                if (damageCounter > timeBetweenDamage)
                {
                    playerHealthController.TakeDamage(damage);
                    damageCounter = 0;
                }
                break;
        }
        if (!GameManager.gameManager.playerIsAlive)
        {
            state = status.Passive;
        }
    }
    private void MoveErratically()
    {
        movementCounter -= Time.deltaTime;
        if (movementCounter < 0)
        {
            horizontalMovement = Random.Range(-1, 2);
            verticalMovement = Random.Range(-1, 2);
            movementCounter = timeBetweenMovements;
        }
        Move(horizontalMovement, verticalMovement);
    }
    public override void FindPlayer()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < viewDistance)
            {
                agent.enabled = true;
                viewDistance = 100;
                state = status.Active;
            }
        }
        else
        {
            if (Time.frameCount % searchInterval == 0)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
    public override void ChasePlayer(GameObject target)
    {
        base.ChasePlayer(target);

        KeepFacingCenter();
    }
    public void DivideOnDeath()
    {
        state = status.Passive;
        if (!isDivided && !isAttached)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject offspringHydra = ObjectPooler.pooler.GetPooledObject(ObjectPooler.pooler.pooledEnemyHydras);
                if (offspringHydra != null)
                {
                    offspringHydra.transform.position = transform.position;
                    offspringHydra.transform.rotation = transform.rotation;
                    EnemyHydra hydraManager = offspringHydra.GetComponent<EnemyHydra>();
                    offspringHydra.GetComponent<HealthManager>().health = offSpringHealth;
                    hydraManager.isDivided = true;

                    offspringHydra.SetActive(true);
                    hydraManager.ResetStats();
                    //hydraManager.PlaceEnemy(rotation, altitude);
                    
                    GameManager.gameManager.CountEnemyUp();
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            agent.enabled = false;
            state = status.Attached;
            playerHealthController = other.GetComponent<HealthManager>();
            playerController = other.GetComponent<PlayerMovementController>();
            isAttached = true;
        }
    }
    void AttachToPlayer()
    {
        rotation = playerController.rotation;
        altitude = playerController.altitude;
    }
    private void OnDisable()
    {
        transform.localScale = new Vector3(1, 1, 1);
        agent.enabled = false;
    }
}
*/