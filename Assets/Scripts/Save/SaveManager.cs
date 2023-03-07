using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public AppData _data;

    private string _pathSave;
    public string _name;
    
    private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();
    private FileStream _fileStream;

    private void OnEnable()
    {
        _pathSave = Application.persistentDataPath;
    }

    public void SaveName(string teamName)
    {
        _fileStream = new FileStream(_pathSave + _name, FileMode.Create);
        
        _data = new AppData
        {
            appData =
            {
                nameTeam = teamName
            }
        };

        _binaryFormatter.Serialize(_fileStream, _data);
        
        _fileStream.Close();
    }
    
    public void SaveObject(GameObject obj)
    {
        _fileStream = new FileStream(_pathSave + _name, FileMode.Create);
        
        _data = new AppData();
        
        _data.Save(obj);
        
        _binaryFormatter.Serialize(_fileStream, _data);
        
        _fileStream.Close();
    }

    public AppData LoadData(string nameObj)
    {
        if (!File.Exists(_pathSave + nameObj))
            return new AppData();
        
        _fileStream = new FileStream(_pathSave + nameObj, FileMode.Open);

        AppData data = (AppData) _binaryFormatter.Deserialize(_fileStream);
        
        _fileStream.Close();

        return data;
    }
}
