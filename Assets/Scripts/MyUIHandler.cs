using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class MyUIHandler : MonoBehaviour
{
    public static MyUIHandler instance { get; private set; }
    public float displayTime = 4f;

    private VisualElement m_Healthbar;
    private VisualElement m_NonPlayerDialogue;
    private float m_TimerDisplay;

    private void Awake()
    {
        instance = this;

        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");

        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_TimerDisplay = -1f;
    }

    private void Start()
    {
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    public void SetHealthValue(float amount)
    {
        m_Healthbar.style.width = Length.Percent(amount * 100f);
    }

    public void TurnOnNPCDialogue()
    {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }
}
