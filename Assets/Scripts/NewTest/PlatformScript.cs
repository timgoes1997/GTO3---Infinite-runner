﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    public enum SpawnType
    {
        Platform,
        LineX,
        LineZ,
        Random,
        RandomLineX,
        RandomLineZ,
        Single,
        Sectioned
    }

    [Serializable]
    public struct SpawnChance
    {
        public SpawnObject spawnObject;
        [Range(minSpawnObjectChance, maxSpawnObjectChance)]
        public float spawnChance;
    }

    [Serializable]
    public struct SpawnObjectTypeChance
    {
        public SpawnedObjectType spawnedObjectType;
        [Range(minSpawnedObjectTypeChance, maxSpawnedObjectTypeChance)]
        public float spawnChance;
        public SpawnChance[] spawnChances;
    }

    public SpawnableObject[,] platformObjects;
    public Vector3 gridSize = Vector3.one;
    public Vector3 sectionSize = new Vector3(2, 0, 1);
    public const float minSpawnObjectChance = 0f;
    public const float maxSpawnObjectChance = 100f;
    public const float minSpawnedObjectTypeChance = 0f;
    public const float maxSpawnedObjectTypeChance = 100f;
    public int maxObstaclesPlatform = 3;
    public int itemPlatformDistance = 6;
    private int curLastItemPlatform = 0;
    public bool oneItemPlatform = true;
    public bool center = true; //Centers the object if possible (When the object next to it is non existent for example)
    public bool placeNextToItem = true;
    public SpawnType spawnType;
    public SpawnObjectTypeChance[] spawnObjectTypeChances;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Returns an array with the grids and the item to spawn on there.
    /// </summary>
    /// <returns></returns>
    public SpawnObject[,] GenerateSpawnObjects()
    {
        return null;
    }

    /// <summary>
    /// Generates an array with random objecttypes and where to spawn them.
    /// </summary>
    /// <returns>Randomized ObjectType multidimensional array.</returns>
    public SpawnedObjectType[,] GenerateSpawnObjectTypesArray()
    {
        switch (spawnType)
        {
            case SpawnType.Platform:
                return GenerateSpawnObjectTypesArrayPlatform();
            case SpawnType.LineX:
                return GenerateSpawnObjectTypesArrayLineX();
            case SpawnType.LineZ:
                return GenerateSpawnObjectTypesArrayLineZ();
            case SpawnType.Random:
                return GenerateSpawnObjectTypesArrayRandom();
            case SpawnType.RandomLineX:
                return GenerateSpawnObjectTypesArrayRandomLineX();
            case SpawnType.RandomLineZ:
                return GenerateSpawnObjectTypesArrayRandomLineZ();
            case SpawnType.Sectioned:
                return GenerateSpawnObjectTypesArraySectioned();
            case SpawnType.Single:
                return GenerateSpawnObjectTypesArraySingle();
        }

        return null;
    }

    /// <summary>
    /// Gives back a SpawnedObjectType multidimensional array full of one object.
    /// There is offcourse a limit on obstacles.
    /// </summary>
    /// <returns>Gives back a SpawnedObjectType multidimensional array full of one object.</returns>
    private SpawnedObjectType[,] GenerateSpawnObjectTypesArrayPlatform()
    {
        SpawnedObjectType[,] spawns = new SpawnedObjectType[platformObjects.GetLength(0), platformObjects.GetLength(1)];

        SpawnedObjectType sot = GetRandomSpawnObjectType();
        if (curLastItemPlatform <= itemPlatformDistance)
        {
            while (sot != SpawnedObjectType.Item) sot = GetRandomSpawnObjectType();
        }

        switch (sot)
        {
            case SpawnedObjectType.Item:
                PlaceOneSpawnedObjectTypeRandomly(ref spawns, sot);
                break;
            case SpawnedObjectType.Coin:
                FillSpawnedObjectTypeArray(ref spawns, sot);
                break;
            case SpawnedObjectType.None:
                FillSpawnedObjectTypeArray(ref spawns, sot);
                break;
            case SpawnedObjectType.Obstacle:

                break;
        }

        return spawns;
    }

    /// <summary>
    /// Fills the given SpawnedObjectType array fully with one objectType.
    /// </summary>
    /// <param name="spawns">The SpawnedObjectType array you want to add objecttypes to.</param>
    /// <param name="sot">The spawnedObjectType you want to fill the array with.</param>
    private void FillSpawnedObjectTypeArray(ref SpawnedObjectType[,] spawns, SpawnedObjectType sot)
    {
        for(int x = 0; x < spawns.GetLength(0); x++)
        {
            for(int z=0; z < spawns.GetLength(1); z++)
            {
                spawns[x, z] = sot;
            }
        }
    }


    /// <summary>
    /// Places a item in a random place in the array.
    /// </summary>
    /// <param name="spawns">The spawn array</param>
    /// <param name="sot">The spawnObjectType you want to place.</param>
    private void PlaceOneSpawnedObjectTypeRandomly(ref SpawnedObjectType[,] spawns, SpawnedObjectType sot)
    {
        int xRandom = UnityEngine.Random.Range(0, (spawns.GetLength(0) - 1));
        int zRandom = UnityEngine.Random.Range(0, (spawns.GetLength(1) - 1));
        for (int z = 0; z < spawns.GetLength(1); z++)
        {
            for (int x = 0; x < spawns.GetLength(0); x++)
            {
                if (x == xRandom && z == zRandom)
                {
                    spawns[x, z] = sot;
                }
                else
                {
                    spawns[x, z] = SpawnedObjectType.None;
                }
            }
        }
    }

    /// <summary>
    /// Gives back a SpawnedObjectType multidimensional array with one single object or multiple objects of the same type on each x-axis.
    /// </summary>
    /// <returns>A SpawnedObjectType multidimensional array with one single object or multiple objects of the same type on each x-axis.</returns>
    private SpawnedObjectType[,] GenerateSpawnObjectTypesArrayLineX()
    {
        return null;
    }

    /// <summary>
    /// Gives back a SpawnedObjectType multidimensional array with one single object or multiple objects of the same type on each z-axis.
    /// </summary>
    /// <returns>A SpawnedObjectType multidimensional array with one single object or multiple objects of the same type on each z-axis.</returns>
    private SpawnedObjectType[,] GenerateSpawnObjectTypesArrayLineZ()
    {
        return null;
    }

    /// <summary>
    /// Gives back a SpawnedObjectType multidimensional array based on sections.
    /// </summary>
    /// <returns>A SpawnedObjectType multidimensional array based on sections.</returns>
    private SpawnedObjectType[,] GenerateSpawnObjectTypesArrayRandom()
    {
        return null;
    }

    /// <summary>
    /// Gives back a SpawnedObjectType multidimensional array with randomObjects sorted line based on the x-axis.
    /// </summary>
    /// <returns>A SpawnedObjectType multidimensional array with randomObjects sorted line based on the x-axis.</returns>
    private SpawnedObjectType[,] GenerateSpawnObjectTypesArrayRandomLineX()
    {
        return null;
    }

    /// <summary>
    /// Gives back a SpawnedObjectType multidimensional array with randomObjects sorted line based on the z-axis.
    /// </summary>
    /// <returns>A SpawnedObjectType multidimensional array with randomObjects sorted line based on the z-axis.</returns>
    private SpawnedObjectType[,] GenerateSpawnObjectTypesArrayRandomLineZ()
    {
        return null;
    }

    /// <summary>
    /// Gives back a SpawnedObjectType multidimensional array with one object that isn't none.
    /// </summary>
    /// <returns>A SpawnedObjectType multidimensional array with one object that isn't none.</returns>
    private SpawnedObjectType[,] GenerateSpawnObjectTypesArraySingle()
    {
        return null;
    }

    /// <summary>
    /// Gives back a SpawnedObjectType multidimensional array based on sections.
    /// </summary>
    /// <returns>A SpawnedObjectType multidimensional array based on sections.</returns>
    private SpawnedObjectType[,] GenerateSpawnObjectTypesArraySectioned()
    {
        return null;
    }

    /// <summary>
    /// Returns a spawnedObjectType based upon the spawnchance you have given.
    /// It works the same as <see cref="GetSpawnObject(SpawnedObjectType)"/> to determine the value.
    /// </summary>
    /// <returns>A spawnedObjectType</returns>
    private SpawnedObjectType GetRandomSpawnObjectType()
    {
        foreach (SpawnObjectTypeChance sotc in spawnObjectTypeChances)
        {
            float spawnFloat = UnityEngine.Random.Range(minSpawnObjectChance, maxSpawnObjectChance);
            if (sotc.spawnChance < spawnFloat)
            {
                return sotc.spawnedObjectType;
            }
        }
        return SpawnedObjectType.None;
    }

    /// <summary>
    /// Returns an object based on the spawnchance you have given.
    /// The spawn chance is by default between 0 and 100.
    /// This method will check all SpawnObjects in spawnchance.
    /// In spawnChance you specify a value zone, which is a zone between the given value of the spawnobject and another objects lower value.
    /// For example you have 2 objects one with value 0.2f and one with 0.4f. The last objects zone will be between 0.2f and 0.4f.
    /// Note: A value of 0f has a 0% spawnrate.
    /// </summary>
    /// <param name="objectType">Of which objecttype you want to spawn a object, think of items, obstacles or coins.</param>
    /// <returns>The object that you will spawn.</returns>
    private SpawnObject GetSpawnObject(SpawnedObjectType objectType)
    {
        if (objectType == SpawnedObjectType.None) return SpawnObject.None;
        foreach (SpawnObjectTypeChance sotc in spawnObjectTypeChances)
        {
            if (sotc.spawnedObjectType == objectType)
            {
                float spawnFloat = UnityEngine.Random.Range(minSpawnObjectChance, maxSpawnObjectChance);
                foreach (SpawnChance sc in sotc.spawnChances)
                {
                    if (sc.spawnChance < spawnFloat)
                    {
                        return sc.spawnObject;
                    }
                }
            }
        }

#if UNITY_EDITOR
        Debug.Log("SpawnObject was not found for objecttype" + objectType);
#endif

        return SpawnObject.None;
    }


    /// <summary>
    /// Moves and resizes the platform.
    /// </summary>
    /// <param name="minX">Minimum x scale.</param>
    /// <param name="maxX">Maximum x scale.</param>
    /// <param name="minY">Minimum y scale.</param>
    /// <param name="maxY">Maximum y scale.</param>
    /// <param name="minZ">Minimum z scale.</param>
    /// <param name="maxZ">Maximum z scale.</param>
    /// <param name="postion">The position you want to move this platform to.</param>
    public void MoveResize(float minX, float maxX, float minY, float maxY, float minZ, float maxZ, Vector3 postion)
    {
        Resize(minX, maxX, minY, maxY, minZ, maxZ);
        transform.position = postion;
    }

    /// <summary>
    /// Resizes the object to a random size between the min an max value for the x,y and z scale.
    /// </summary>
    /// <param name="minX">Minimum x scale.</param>
    /// <param name="maxX">Maximum x scale.</param>
    /// <param name="minY">Minimum y scale.</param>
    /// <param name="maxY">Maximum y scale.</param>
    /// <param name="minZ">Minimum z scale.</param>
    /// <param name="maxZ">Maximum z scale.</param>
    public void Resize(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        Resize(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY), UnityEngine.Random.Range(minZ, maxZ));
    }

    /// <summary>
    /// Resizes the object to the size specified by the x,y and z.
    /// </summary>
    /// <param name="x">The x scale</param>
    /// <param name="y">The y scale</param>
    /// <param name="z">the z scale</param>
    public void Resize(float x, float y, float z)
    {
        Vector3 scale = new Vector3(x, y, z);
        Resize(scale);
    }

    /// <summary>
    /// Resizes this object with the given scale.
    /// </summary>
    /// <param name="scale">The scale this object needs to be.</param>
    public void Resize(Vector3 scale)
    {
        transform.localScale = scale;
        platformObjects = new SpawnableObject[GetGridsX(), GetGridsZ()];
    }

    /// <summary>
    /// Gets the amount of grids on the x-axis.
    /// </summary>
    /// <returns>The amount of grids on the x-axis</returns>
    private int GetGridsX()
    {
        int grids = 0;
        float xSize = transform.localScale.x;
        while (xSize > gridSize.x)
        {
            xSize -= gridSize.x;
            grids++;
        }
        return grids;
    }

    /// <summary>
    /// Gets the amount of grids on the z-axis.
    /// </summary>
    /// <returns>The amount of grids on the z-axis</returns>
    private int GetGridsZ()
    {
        int grids = 0;
        float zSize = transform.localScale.z;
        while (zSize > gridSize.z)
        {
            zSize -= gridSize.z;
            grids++;
        }
        return grids;
    }

}
