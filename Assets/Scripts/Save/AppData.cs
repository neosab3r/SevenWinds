using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class AppData
{
    public Data appData = new Data();

    [Serializable]
    public class Data
    {
        public List<SerializableColor> colors = new List<SerializableColor>();
        public string name;
        public string nameTeam;
    }

    public void Save(GameObject obj)
    {
        appData = new Data();
        
        UniformColor color = obj.GetComponent<UniformColor>();
        
        Data data = new Data();

        for (int i = 0; i < color._sprites.Count; i++)
        {
            SerializableColor serializableColor = new SerializableColor();
            serializableColor.Color = color._sprites[i].color;
            data.colors.Add(serializableColor);
        }

        data.name = color.gameObject.name;

        appData = data;
    }
}

[Serializable]
public class SerializableColor
{
    public float[] colorStore = new float[4] {1F, 1F, 1F, 1F};

    public Color Color
    {
        get { return new Color(colorStore[0], colorStore[1], colorStore[2], colorStore[3]); }
        set { colorStore = new float[4] {value.r, value.g, value.b, value.a}; }
    }


    public static implicit operator Color(SerializableColor instance)
    {
        return instance.Color;
    }

    public static implicit operator SerializableColor(Color color)
    {
        return new SerializableColor {Color = color};
    }
}
