using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadApp : MonoBehaviour
{
    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private NameScene _nameScene;
    [SerializeField] private NameTeam _nameTeamText;
    [SerializeField] private SelectedColors _selectedColors;
    
    [SerializeField] private List<GameObject> _objects;
    
    [SerializeField] private string _nameUniform;
    [SerializeField] private string _nameLogo;
    [SerializeField] private string _nameTeam;
    
    enum NameScene
    {
        uniform,
        logo, 
        nameTeam
    };
    
    void Start()
    {
        switch (_nameScene)
        {
            case NameScene.uniform:
            {
                AppData dateUniform = _saveManager.LoadData(_nameUniform);
                if(!FindNameInObjects(dateUniform)) _objects[0].SetActive(true);
                
                _selectedColors.SetSaveUniform(dateUniform);
                
                break;
            }
            case NameScene.logo:
            {
                AppData dataLogo = _saveManager.LoadData(_nameLogo);
                if (!FindNameInObjects(dataLogo))
                {
                    _objects[2].SetActive(true);
                    Debug.Log("adsfasd");
                }
                
                _selectedColors.SetSaveUniform(dataLogo);
                
                AppData dateUniform = _saveManager.LoadData(_nameUniform);
                FindNameInObjects(dateUniform);
                
                break;
            }
            case NameScene.nameTeam:
            {
                AppData dateUniform = _saveManager.LoadData(_nameUniform);
                FindNameInObjects(dateUniform);
            
                AppData dataLogo = _saveManager.LoadData(_nameLogo);
                FindNameInObjects(dataLogo);
                
                AppData dataNameTeam = _saveManager.LoadData(_nameTeam);
                
                _nameTeamText.SetSaveName(dataNameTeam.appData.nameTeam);
                Debug.Log("SAVE NAME: " + dataNameTeam.appData.nameTeam);
                
                break;
            }
        }
    }

    private bool FindNameInObjects(AppData data)
    {
        foreach (var obj in _objects)
        {
            if (obj.name == data.appData.name)
            {
                UniformColor uniformColor = obj.GetComponent<UniformColor>();

                for (int i = 0; i < uniformColor._sprites.Count; i++)
                {
                    Color32 dataColor = data.appData.colors[i].Color;
                    uniformColor._sprites[i].color = dataColor;
                }
                    
                obj.SetActive(true);
                return true;
            }
        }

        return false;
    }
}
