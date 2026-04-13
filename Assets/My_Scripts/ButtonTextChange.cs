using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextChange : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttontext;
    [SerializeField] private Image button;

    private bool isToggled = false;
    public void OnButtonClick()
    {
        isToggled = !isToggled;
        
        if(isToggled)
        {
            buttontext.text = "Button has Been Pressed";
            button.color = Color.red;
        } else
        {
            buttontext.text = "";
            button.color = Color.green;
        }
    }
}
