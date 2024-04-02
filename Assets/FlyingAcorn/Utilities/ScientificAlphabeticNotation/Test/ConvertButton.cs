using System.Collections;
using System.Collections.Generic;
using FlyingAcorn.Utilities.ScientificAlphabeticNotation;
using TMPro;
using UnityEngine;

public class ConvertButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    public void Convert()
    {
        var result = NumberFormatting.ConvertToScientificAlphabeticFormat(inputField.text);
        Debug.Log(result);
    }
}
