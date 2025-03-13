using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBomb : Explosion
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Trigger()
    {

    }

    IEnumerator Execute(Transform target)
    {
        while (Vector2.Distance(this.transform.position, target.position) != 0)
        {
            Vector2.MoveTowards(this.transform.position, target.position, 0);
            yield return null;
        }
    }
}
