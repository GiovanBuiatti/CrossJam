using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;

    private Transform playerTransform;
    
    private float spawnZ = -6.0f;
    //Cette variable sert a faire spawn en avance des prefabs
    private float tileLength = 12.0f;
    //Cette variable est le nombre de tile en jeu 
    private int amnTilesOnScreen = 7;
    //Cette variable sert a ce que les tiles ne se suppriment pas trop tot
    private float safeZone = 15.0f;

    private int lastPrefabIndex = 0;
    
    private List<GameObject> activeTiles;

    // Start is called before the first frame update
    private void Start()
    {

        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

       for(int i = 0; i < amnTilesOnScreen; i++)
        {
            if( i < 2)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile();
            }
            
        }

    }

    // Update is called once per frame
    private void Update()
    {
        //Ce if veut dire : quand on passe un certain point, une nouvelle Tile apparait (principe du niveau infini) / Il sert aussi a supprimer les tiles une fois dépassés
        if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }


    //Ici on a la fonction qui fait spawn des PREFABS ALEATOIREMENT
    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if(prefabIndex == -1)
        {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        }
            go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    //Ici on supprime les prefabs qui sont derriere le joueur 
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    //Cette fonction est celle qui permet le spawn de tile d'être aléatoire
    private int RandomPrefabIndex()
    {
        if(tilePrefabs.Length <= 1)
        
        return 0;
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
        
    }
}
