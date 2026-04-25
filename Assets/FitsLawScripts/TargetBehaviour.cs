using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TargetBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Material normalColor;   // optional — or just use the Button's colors
    private Button button;
    private ExperimentManager experimentManager;

    void Awake()
    {
        button = GetComponent<Button>();
        experimentManager = FindObjectOfType<ExperimentManager>();

        if (button != null)
            button.onClick.AddListener(OnClicked);
        else
            Debug.LogError("No Button component found on TargetBehaviour.");
    }

    void OnDestroy()
    {
        if (button != null)
            button.onClick.RemoveListener(OnClicked);
    }

    private void OnClicked()
    {
        experimentManager?.OnTargetSelected();
    }

    // Optional hover highlight via EventSystem
    public void OnPointerEnter(PointerEventData e)
    {
        // e.g. change button color on hover
    }

    public void OnPointerExit(PointerEventData e)
    {
        // restore color
    }
}