using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customObstacleMove : MonoBehaviour
{
    public List<Transform> obsWaypoints;
    public Transform objectToMove;

    [SerializeField]
    GameObject obstacles;

    [SerializeField]
    Transform obsParent;

    [SerializeField]
    GameObject wpParent;

    [SerializeField]
    GameObject obsWP;

    [SerializeField]
    List<Vector3> existingObstacles;


    public int wayPointCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        wpParent = new GameObject("ObsWaypoints");
        wpParent.transform.position = new Vector3(0, 0);
        SpawnWP();
        
    }

    private Vector3 RandomLocation()
    {
        Vector3 pos;
        int x, y;

        do
        {
            x = Random.Range(-49, 49);
            y = Random.Range(-49, 49);

            pos = new Vector3(x + 1.5f, y + 1.5f);
        }
        while (existingObstacles.Contains(new Vector3(x, y)));

        return pos;
    }

    public void SpawnWP()
    {
        for (int counter = 0; counter < 2; counter++)
        {
            Vector3 pos = RandomLocation();
            existingObstacles.Add(pos);
            GameObject wp = Instantiate(obsWP, pos, Quaternion.identity);
            
            obsWaypoints.Add(wp.transform);
            wp.transform.parent = wpParent.transform;

        }
        StartCoroutine(moveMe());
    }

    IEnumerator moveMe()
    {
        //for(int counter=0;counter<waypoints.size;counter++)
        foreach (Transform mytransform in obsWaypoints)
        {
            while (Vector3.Distance(objectToMove.position, mytransform.position) > 0.1f)
            {
                //1 unit towards the first one
                objectToMove.position = Vector3.MoveTowards(objectToMove.position,
                mytransform.position, 1f);

                yield return new WaitForSeconds(0.1f);
            }
            wayPointCounter++;
            if(wayPointCounter >= obsWaypoints.Count)
            {
                StopCoroutine(moveMe());
                wayPointCounter = 0;
                StartCoroutine(moveMe());
            }
            yield return null;
        }

        yield return null;
    }
}
