using System;
using UnityEngine;

public class Objects
{
    public Guid Id;
    public Guid EnviromentId;
    public int PrefabId;
    public float PositionX;
    public float PositionY;
    public float ScaleX;
    public float ScaleY;
    public float RotationZ;
    public string SortingLayer;

    public Objects(string id, string enviromentId, int prefabId, float positionX, float positionY, float scaleX, float scaleY, float rotationZ, string sortingLayer)
    {
        Id = Guid.Parse(id);
        EnviromentId = Guid.Parse(enviromentId);
        PrefabId = prefabId;
        PositionX = positionX;
        PositionY = positionY;
        ScaleX = scaleX;
        ScaleY = scaleY;
        RotationZ = rotationZ;
        SortingLayer = sortingLayer;
    }
}
