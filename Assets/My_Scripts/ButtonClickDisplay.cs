using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ButtonClickDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displaytext;
    [SerializeField] private Image button;

    private int counter = 0;

    void Start()
    {
        displaytext.text = "";
    }
    public void onclickbutton() {
        counter++;
        displaytext.text = counter.ToString();
    }


}
