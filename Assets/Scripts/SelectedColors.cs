using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectedColors : MonoBehaviour
{ 
    [SerializeField] private Toggle _selectedToggle;
    [SerializeField] private SaveManager _saveManager;

    [SerializeField] private int _indexUniform = 0;
    [SerializeField] private int _indexToggle = 0;
    [SerializeField] private string _nameScene;

    [SerializeField] private List<SelectedButton> _buttons;
    [SerializeField] private List<UniformColor> _uniforms;
    [SerializeField] private List<ColorToggle> _colors;

    void Start()
    {
        _selectedToggle = _buttons[0]._spriteToggle;
        _selectedToggle.isOn = true;
    }

    public void SetSpriteButton(Toggle toggle)
    {
        if (toggle.isOn)
        {
            Toggle previousColorToggle = _selectedToggle;

            for (int i = 0; i < _buttons.Count; i++)
            {
                if (_buttons[i]._spriteToggle == toggle)
                {
                    _selectedToggle = toggle;
                    _indexToggle = i;
                    if (_buttons[i]._selectedColorToggle != null)
                    {
                        _buttons[i]._selectedColorToggle.isOn = true;
                    }
                    else
                    {
                        if (previousColorToggle != null &&
                            previousColorToggle.GetComponent<SelectedButton>()._selectedColorToggle != null)
                            previousColorToggle.GetComponent<SelectedButton>()._selectedColorToggle.isOn = false;
                    }

                    break;
                }

            }
        }
    }

    public void SetColor(Toggle colorToggle)
    {
        if (colorToggle.isOn)
        {
            SelectedButton spriteToggle = _selectedToggle.gameObject.GetComponent<SelectedButton>();
            
            Color32 color = colorToggle.GetComponent<ColorToggle>()._color;
            
            if (IsEqualColorSprite(color))
            {
                spriteToggle._sprite.color = color;
                spriteToggle._selectedColorToggle = colorToggle;
                SetUniformColor(color);
            }
        }
    }

    public void NextScene()
    {
        _saveManager.SaveObject(_uniforms[_indexUniform].GetComponent<UniformColor>().gameObject);
        SceneManager.LoadScene(_nameScene);
    }

    public void RandomColor()
    {
        
        List<int> randomList = new List<int>();

        while (randomList.Count <= 3)
        {
            var random = Random.Range(0, _colors.Count);
            if (!randomList.Contains(random)) {
                randomList.Add(random);
            }
        }
        
        for (int i = 0; i < _buttons.Count; i++)
        {
            _uniforms[_indexUniform]._sprites[i].color = _colors[randomList[i]]._color;
            _buttons[i]._sprite.color = _colors[randomList[i]]._color;
        }
    }

    public void ChangeIndexForm(int n)
    {

        if (_indexUniform + n < _uniforms.Count && _indexUniform + n >= 0)
        {
            _uniforms[_indexUniform].gameObject.SetActive(false);
            _indexUniform += n;
            _uniforms[_indexUniform].gameObject.SetActive(true);

            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i]._sprite.color = _uniforms[_indexUniform]._sprites[i].color;
                _buttons[i]._selectedColorToggle = null;
            }
        }
    }
    
    private void SetUniformColor(Color32 color)
    {
        _uniforms[_indexUniform]._sprites[_indexToggle].color = color;
    }

    private bool IsEqualColorSprite(Color32 color)
    {
        List<SelectedButton> toggles = _buttons.Where(x => x._spriteToggle != _selectedToggle).ToList();

        foreach (var toggle in toggles)
        {
            if (toggle._sprite.color == color)
            {
                return false;
            }
        }
        
        return true;
    }

    public void SetSaveUniform(AppData data)
    {
        if (data != null)
        {
            for (int i = 0; i < _uniforms.Count; i++)
            {
                if (data.appData.name == _uniforms[i].gameObject.name)
                {
                    _indexUniform = i;
                    
                    for (int j = 0; j < _buttons.Count; j++)
                    {
                        _buttons[j]._sprite.color = _uniforms[_indexUniform]._sprites[j].color;
                        _buttons[j]._selectedColorToggle = null;
                    }
                    
                    break;
                }
            }
        }
        else
        {
            _indexUniform = 0;
        }
    }
}
