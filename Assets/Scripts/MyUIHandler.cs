using UnityEngine;
using UnityEngine.UIElements;

public class MyUIHandler : MonoBehaviour
{
    public static MyUIHandler instance { get; private set; }

    private VisualElement m_Healthbar;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1f);
    }

    public void SetHealthValue(float amount)
    {
        m_Healthbar.style.width = Length.Percent(amount * 100f);
    }
}
