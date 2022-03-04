using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathLaser : MonoBehaviour
{
    [SerializeField] float extendedLength;
    [SerializeField] float extendedWidth;
    [SerializeField] float laserTime;
    [SerializeField] BossDeathExplosion explosion;
    float width = .01f;
    float y = .01f;

    float t;
    float wt;
    public bool shootingLaser= false;
    private void Start()
    {
        shootingLaser = false;
    }

    private void Update()
    {
         if (shootingLaser)
            {
                t += Time.deltaTime*laserTime;
                y = Mathf.Lerp(0, extendedLength, t);
                if (y >= extendedLength)
                {
                    wt += Time.deltaTime;
                    width = Mathf.Lerp(0.1f, extendedWidth, wt);
                }
                transform.localScale = new Vector3(width, y, width);
                   if(width >= extendedWidth)
            {
                explosion.exploding = true;
            }
         }
    }

    public void ShootLaser()
    {
        shootingLaser = true;
    }
    IEnumerator ShootTheLaser() {
        yield break;
    }
}
