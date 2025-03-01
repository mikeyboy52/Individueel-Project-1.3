using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldEditor : MonoBehaviour
{
    public void CreateWorld(string worldname)
    {
        string name = worldname;
        APIClient.Instance.CreateWorld(name);

    }

    public void EditWorld(string worldname)
    {
        APIClient.Instance.EditWorld(name);
    }
}
