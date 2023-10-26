using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{

    public GameObject prefab;
    public float offset = 2f;
    public Vector3 lastPosition;

    private int roadCount = 0;
    public void StartBuilding()
    {
        InvokeRepeating("CreateNewRoadPart", 1f, 0.5f);
    }

    public void CreateNewRoadPart()
    {
        Debug.Log("Create new road part");

        Vector3 spawnPosition = new Vector3(1.414213f, 0, 7.071068f);

        float chance = Random.Range(0, 100);

        if(chance < 50)
        {
            spawnPosition = new Vector3(lastPosition.x + offset, -lastPosition.y, lastPosition.z + offset);
        }
        else
        {
            spawnPosition = new Vector3(lastPosition.x - offset, lastPosition.y, lastPosition.z + offset) ;
        }

        GameObject g = Instantiate(prefab, spawnPosition, Quaternion.Euler(0, 45, 0));

        lastPosition = g.transform.position;

        roadCount++;

        if(roadCount % 5 == 0)
        {
            g.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
