using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KU1 : MonoBehaviour
{
    [SerializeField]
    List<Vector3> existingAIs;

    [SerializeField]
    GameObject ai;

    void Start()
    {
        existingAIs = new List<Vector3>();
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
        SpawnAI();
        StartAI();
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
        while (existingAIs.Contains(new Vector3(x, y)));

        return pos;
    }

    public void SpawnAI()
    {
        ai.SetActive(true);
        for (int counter = 0; counter < 9; counter++)
        {
            Vector3 pos = RandomLocation();
            existingAIs.Add(pos);
            GameObject ais = Instantiate(ai, pos, Quaternion.identity);
        }
    }


    public void StartAI()
    {

        GameObject[] aim = GameObject.FindGameObjectsWithTag("AI");
        for (int i = 0; i < aim.Length; i++)
        {
            aim[i].GetComponent<customAIMoveScript>().enabled = true;
        }
    }
}

/* KU1: A* pathfinding pro implements this functionality using a specific feature => LOCAL AVOIDANCE 
        : it is based on RVO: Reciprocal Velocity Obstacles which is divided into 2 parts
            :Core simulation code and Unity interface
 */
