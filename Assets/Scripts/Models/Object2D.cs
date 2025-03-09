using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Object2D
{
    public Guid EnviromentId;
    public int PrefabId;
    public float posX;
    public float posY;
    public float scaleX;
    public float scaleY;
    public float rotZ;
    public string sortingLayer;

    public Object2D(Guid enviromentId, int prefabId, float x, float y, float sx, float sy, float rz, string layer)
    {
        EnviromentId = enviromentId;
        PrefabId = prefabId;
        posX = x;
        posY = y;
        scaleX = sx;
        scaleY = sy;
        rotZ = rz;
        sortingLayer = layer;
    }
}
