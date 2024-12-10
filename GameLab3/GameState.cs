using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GameState
{
    public Battlefield Battlefield { get; set; }

    public void Save(string filePath)
    {
        using (var fs = new FileStream(filePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(fs, this);
        }
    }

    public static GameState Load(string filePath)
    {
        using (var fs = new FileStream(filePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (GameState)formatter.Deserialize(fs);
        }
    }
}
