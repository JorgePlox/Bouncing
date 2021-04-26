using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //El level Generator es unico
    public static LevelGenerator sharedInstance;

    //Primero: Lista con todos los bloques creados
    //Segundo: los bloques que se encuentran ahora en la escena
    public List<LevelBlock> allLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    //Punto o lugar donde queremos que se creen los niveles
    public Transform levelStartPoint;






    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        GenerateInitialBLocks();
    }


    public void AddLevelBlock()
    {

        //Se toma un número entre 0 y la cantidad de bloques existente y se crea (random toma el segundo valor como menor <b)
        //Se instancia el bloque
        int randomIndex = Random.Range(0, allLevelBlocks.Count);
        LevelBlock currentBlock = (LevelBlock)Instantiate(allLevelBlocks[randomIndex]);

        //Se toma el bloque y se deja como hijo del LevelGenerator
        currentBlock.transform.SetParent(this.transform, false);



        Vector3 spawnPosition = Vector3.zero;

        if (currentLevelBlocks.Count == 0)
        {
            spawnPosition = levelStartPoint.position;
        }

        else
        {
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].endPoint.position;

        }

        Vector3 correction = new Vector3(spawnPosition.x-currentBlock.startPoint.position.x,
                                         spawnPosition.y-currentBlock.startPoint.position.y,
                                         0);

        currentBlock.transform.position = correction;
        currentLevelBlocks.Add(currentBlock);
    }

    public void RemoveLevelBlock()
    {
        LevelBlock oldestBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldestBlock);
        Destroy(oldestBlock.gameObject);
    }


    public void RemoveAllBlocks()
    {
        while (currentLevelBlocks.Count >0)
        {
            RemoveLevelBlock();
        }
    }


    public void GenerateInitialBLocks()
    { 
        for (int i = 0; i < 3; i++)
        {
            AddLevelBlock();

        }
    }

}
