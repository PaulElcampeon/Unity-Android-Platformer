using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    private Spider spider;

    public void Start()
    {
        spider = transform.parent.GetComponent<Spider>();
    }

    public void Fire()
    {
        spider.Attack();
    }
}
