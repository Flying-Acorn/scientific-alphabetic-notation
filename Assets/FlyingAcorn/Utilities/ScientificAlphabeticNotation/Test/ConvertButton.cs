using FlyingAcorn.Utilities.ScientificAlphabeticNotation;
using TMPro;
using UnityEngine;
using Toggle = UnityEngine.UI.Toggle;

public class ConvertButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField numberInputField;
    [SerializeField] private TMP_InputField decimalInputField;
    [SerializeField] private Toggle precisionModeToggle;

    public void Convert()
    {
        int.TryParse(decimalInputField.text, out var decimalCount);
        var result = NumberFormatting.ConvertToScientificAlphabeticFormat(numberInputField.text,decimalCount,precisionModeToggle.isOn);
        Debug.Log(result);
    }
}
