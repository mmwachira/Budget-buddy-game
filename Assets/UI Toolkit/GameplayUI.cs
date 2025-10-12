using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUI : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/GameplayUI")]
    public static void ShowExample()
    {
        GameplayUI wnd = GetWindow<GameplayUI>();
        wnd.titleContent = new GUIContent("GameplayUI");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}
