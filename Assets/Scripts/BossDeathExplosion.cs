using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathExplosion : MonoBehaviour
{
    [SerializeField] float extendedSize;
    [SerializeField] float explosionTime;
    float width = .01f;
    float size = .01f;
    float t;
    public bool exploding=false;

    private void Update()
    {

        if (exploding)
        {
            t += Time.deltaTime * explosionTime;
            size = Mathf.Lerp(0, extendedSize, t);
            transform.localScale = new Vector3(size, size, size);
            if (size >= extendedSize)
            {
                GameManager.TGM.GameOver();
            }
        }

    }
}
