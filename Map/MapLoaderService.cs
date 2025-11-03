using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class MapLoaderService : IService
{
    private MapConfig _mapConfig;

    public MapConfig MapConfig => _mapConfig;

    public bool IsInitialized { get; set; }
    public void DeInitialize()
    {
        IsInitialized = false;
    }

    public void Initialize()
    {
        LoadMapConfig();
        IsInitialized = true;
    }

    private void LoadMapConfig()
    {
        _mapConfig = null;
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MapConfig));
            using (FileStream stream = new FileStream(Path.Combine(Application.dataPath, "Config/map.xml"), FileMode.Open))
            {
                _mapConfig = serializer.Deserialize(stream) as MapConfig;
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Exception loading config file: " + e);
        }

        foreach (var item in _mapConfig.Entities)
        {
            Debug.Log("*** entity " + item.Id + " x/y " + item.X + "/" + item.Y);
        }
    }

    public void WriteMap()
    {
        _mapConfig.Entities.Add(new MapConfig.Entity
        {
            Id = "test_1",
            Height = 2,
            Width = 2,
            X = 3,
            Y = 0
        });
         
        XmlSerializer mySerializer = new XmlSerializer(typeof(MapConfig));
        StreamWriter myWriter = new StreamWriter(Path.Combine(Application.dataPath, "Config/map2.xml"));
        mySerializer.Serialize(myWriter, _mapConfig);
        myWriter.Close();

        Debug.Log("*** save map");
    }
}
