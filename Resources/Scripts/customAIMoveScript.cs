using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;


public class customAIMoveScript : MonoBehaviour
{
    Seeker seeker;
    Path pathToFollow;

    public Transform target;


    void Start()
    {
        Debug.Log(this.name);

        seeker = GetComponent<Seeker>();

        seeker.StartPath(transform.position, target.position, ReadyToMove);
    }


    //when the path is generated this method is called.
    void ReadyToMove(Path p)
    {
        Debug.Log("Path complete " + p.error);
        pathToFollow = p;
        StartCoroutine(moveTowardsPath());
    }


    IEnumerator moveTowardsPath()
    {
        foreach(Vector3 position in pathToFollow.vectorPath)
        {
            while(Vector3.Distance(this.transform.position,position)>0.5f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, position, 1f);
                Debug.Log(position);/**/
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return null;
    }
}


