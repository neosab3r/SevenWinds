using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class NameTeam : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _nextScene;
    [SerializeField] private SaveManager _saveManager;

    [SerializeField] private Color32 _wrongNameColor = new Color32();
    [SerializeField] private Color32 _defaultColor = new Color32();
    [SerializeField] private int _lenghtCorrectName = 10;

    
    void Start()
    {
        _nextScene.interactable = false;
    }

    public void SetSaveName(string name)
    {
        if (name != null && name.Length > 0)
        {
            _inputField.text = name;
            Debug.Log(_text.text.Length);
            _nextScene.interactable = true;
            _text.color = _defaultColor;
        }
    }

    private void FixedUpdate()
    {
        if (_text.text.Length <= _lenghtCorrectName && _text.text.Length >= 3)
        {
            _nextScene.interactable = true;
            _text.color = _defaultColor;
        }
        else
        {
            _nextScene.interactable = false;
            _text.color = _wrongNameColor;
        }          

    }

    public void SaveName()
    {
        _saveManager.SaveName(_text.text);
        SceneManager.LoadScene("Uniform");
    }
}
