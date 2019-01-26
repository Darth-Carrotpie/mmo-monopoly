using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public static class CityDataSerializable
{
    //public List<CityData> cities = new List<CityData>();
    public static string[] Export(CityData[] cities)
    {
        List<string> c = new List<string>();
        for (int i = 0; i < cities.Length; i++)
        {
            c.Add(cities[i].name);
        }
        return c.ToArray();
    }
}
[System.Serializable]
public class CityData{
    string country;
    public string name;
    string lat;
    string lng;
}
