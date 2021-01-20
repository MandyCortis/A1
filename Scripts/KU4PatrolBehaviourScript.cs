using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KU4PatrolBehaviourScript : MonoBehaviour
{
    public List<Transform> waypoints;

    public Transform objectToMove;

    [SerializeField]
    GameObject obstaclesPrefab;

    [SerializeField]
    Transform obsParent;

    [SerializeField]
    GameObject ai;

    [SerializeField]
    List<Vector3> existingObstacles;

    private float timer = 0.0f;
    readonly private float waitTime = 0.1f;

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

    public void SpawnAI()
    {
        Vector3 pos = RandomLocation();
        ai.SetActive(true);
        Instantiate(ai, pos, Quaternion.identity);
        objectToMove = GameObject.FindGameObjectWithTag("AI").transform;
    }


    public void AddObstacle()
    {
        for (int counter = 0; counter < 3; counter++)
        {
            Vector3 pos = RandomLocation();
            existingObstacles.Add(pos);
            GameObject obs = Instantiate(obstaclesPrefab, pos, Quaternion.identity);
            obs.transform.parent = obsParent;

            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach(GameObject obstacle in obstacles)
            {
                obstacle.GetComponent<customObstacleMove>().enabled = true;
            }
        }
    }



    IEnumerator moveMe()
    {
        //for(int counter=0;counter<waypoints.size;counter++)
        foreach(Transform mytransform in waypoints)
        {
            while(Vector3.Distance(objectToMove.position,mytransform.position)>0.1f)
            {
                //1 unit towards the first one
                objectToMove.position = Vector3.MoveTowards(objectToMove.position,
                mytransform.position,1f);

                yield return new WaitForSeconds(0.1f);        
            }
            
            yield return null;
        }
        
        yield return null;
    }



    void Start()
    {
        SpawnAI();
        StartCoroutine(moveMe());
        AddObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
            timer = waitTime;
        }
    }
}
