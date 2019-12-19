using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeedSpawner : MonoBehaviour
{
    private List<GameObject> listOfSeeds;
    public GameObject seedMarkerPrefab, parentObj;
    [SerializeField]
    private int numberOfSeeds = 0;

    float xSpread, zSpread, spreadSize = 2.0f;
    Vector3 spreadPos;

    //Presets
    bool wildGrowth = false;

    void Start()
    {
        listOfSeeds = new List<GameObject>();
        MakeSeeds(numberOfSeeds);
        if (wildGrowth)
        {
            SpreadSeeds();
        }
    }

    public void SpreadSeeds()
    {
        if (listOfSeeds == null || listOfSeeds.Count == 0)
        {
            return;
        }
        if (listOfSeeds.Count > 1)
        {
            foreach (GameObject seed in listOfSeeds)
            {
                xSpread = Random.Range(-spreadSize, spreadSize);
                zSpread = Random.Range(-spreadSize, spreadSize);

                spreadPos = new Vector3(xSpread, 0.01f, zSpread);
                //print($"spreadPos is: x: {spreadPos.x} z: {spreadPos.z}");

                seed.transform.position = spreadPos;
            }
        }
        else
        {
            listOfSeeds[0].transform.position = transform.position;
        }
    }

    public void MakeSeeds(int numbOfSeeds)
    {
        for (int i = 0; i < numbOfSeeds; i++)
        {
            GameObject sMaker = Instantiate(seedMarkerPrefab, this.transform) as GameObject;
            listOfSeeds.Add(sMaker);
        }
    }
}
