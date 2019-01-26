using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorGenerator : MonoBehaviour
{
    public List<ColorName> colNames = new List<ColorName>();

    public Color GetColorValue (string colName){
        Color c = Color.black;
        foreach(ColorName n in colNames){
            if (n.strName == colName){
                return n.color;
            }
        }
        return c;
    }
    [System.Serializable]
    public class ColorName {
        public string strName;
        public Color color;
    }
}
