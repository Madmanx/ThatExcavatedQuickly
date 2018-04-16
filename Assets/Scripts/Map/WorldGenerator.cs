using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public delegate void MapReady();
    public static event MapReady OnMapReady;


    [SerializeField]
    GameObject WormTile;

    [SerializeField]
    GameObject GrassTile;

    [SerializeField]
    GameObject GroundTile;
   
    [SerializeField]
    GameObject EndTile;

    int[,,] map = new int[10, 10, 10] {
        {
            { 0, 0, 0, 0, 0, 0 ,0, 0,0 ,0 },
            { 0, 0, 0, 0, 0, 0 ,0, 0,0 ,0 },
            { 0, 0, 0, 0, 0, 0 ,0, 0,0 ,0 },
            { 0, 0, 0, 0, 0, 0 ,0, 0,0 ,0 },
            { 0, 0, 0, 0, 0, 0 ,0, 0,0 ,0 },
            { 0, 0, 0, 0, 0, 0 ,0, 0,0 ,0 },
            { 0, 0, 0, 0, 0, 0 ,0, 0,0 ,0 },
            { 0, 0, 0, 0, 0, 0 ,0, 0,0 ,0 },
            { 0, 0, 0, 0, 0, 0 ,0, 0,0 ,0 },
            { 0, 0, 0, 0, 0, 0 ,0, 0,0 ,0 }
        },
         {
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
             { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 }
        },
          {
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
             { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 }
        },
           {
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
             { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 }
        },
            {
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
             { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,3 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 }
        },
             {
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
             { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,2,2 },
            { 1, 1, 3, 1, 1 ,1, 1, 1 ,2,2 }
        },
              {
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
             { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 1 ,1,1 },
            { 1, 1, 1, 1, 1 ,1, 1, 2 ,2,2 },
            { 1, 1, 1, 1, 1 ,1, 1, 2 ,-1,-1 },
            { 1, 1, 1, 1, 1 ,1, 1, 2 ,-1,-1 }
        },
               {
            { 2, 2, 2, 2, 2 ,2, 2, 2 ,2,2 },
            { 2, 2, 2, 2, 2 ,2, 2, 2 ,2,2 },
            { 2, 2, 2, 2, 2 ,2, 2, 2 ,2,2 },
            { 2, 2, 2, 2, 2 ,2, 2, 2 ,2,2 },
            { 2, 2, 2, 2, 2 ,2, 2, 2 ,2,2 },
            { 2, 2, 2, 2, 2 ,2, 2, 2 ,2,2 },
            { 2, 2, 2, 2, 2 ,2, 2, 2 ,2,2 },
            { 2, 2, 2, 2, 2 ,2, 2, -1 ,-1,-1 },
            { 2, 2, 2, 2, 2 ,2, 2, -1 ,-1,-1 },
            { 2, 2, 2, 2, 2 ,2, 2, -1 ,-1,-1 }
        },
             {
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 }
        },
         {
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 },
            { -1, -1, -1, -1, -1 ,-1, -1, -1 ,-1,-1 }
        }
    };

    // Update is called once per frame
    float elaspsedTime = 0.0f;
    float maxElapsedTime = 0.05f;

    int currentLayer = 0;

    bool endReach = false;

    // Use this for initialization
    void Start () {
        //Mathf.PerlinNoise(10, 10);
	}

    int currentStreamingLine = 0;
    public void StreamWorld()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 position = transform.position;
            position.x = transform.position.x + (i);
            position.z = transform.position.z + (currentStreamingLine);
            position.y = transform.position.y + (currentLayer);

            switch (map[currentLayer, currentStreamingLine, i])
            {
                case 0:
                    Instantiate(EndTile, position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(GroundTile, position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(GrassTile, position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(WormTile, position, Quaternion.identity);
                    break;
                default:
                    break;
            }
       
        }
        currentStreamingLine++; // je passe à la ligne suivante

        if(currentStreamingLine >= 10)
        {
            currentStreamingLine = 0;
            currentLayer++;
            if(currentLayer >= 10)
            {
                endReach = true;
            }
        }
    }

    void Update()
    {
        if (!endReach)
        {
            elaspsedTime += Time.deltaTime;
            if (elaspsedTime >= maxElapsedTime)
            {
                StreamWorld();
                elaspsedTime = 0.0f;
            }
        }
  
    }

}
