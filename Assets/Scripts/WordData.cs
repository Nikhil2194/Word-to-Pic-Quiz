using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordData : MonoBehaviour
{
    [SerializeField] TMP_Text charText;

    [HideInInspector]
    public char charValue;

    private Button buttonObj;


    private void Awake()
    {
        buttonObj = GetComponent<Button>();

        if(buttonObj != null)
        {
            buttonObj.onClick.AddListener(() => CharSelected());
        }
    }

    public void SetChar(char _value)
    {
        charText.text = _value.ToString();
        charValue = _value;
    }

    private void CharSelected()
    {
        QuizManager.instance.SelectedOption(this);
    }

}
