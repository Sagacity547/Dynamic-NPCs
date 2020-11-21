using UnityEditor;
using UnityEngine;

public class DynamicNPCGenerator : EditorWindow
{
    //Variables that will be displayed for developer to edit
    string npcBaseName = "";
    int npcID = 1;
    SpriteRenderer npcSprite;
    float npcScale;

    //Other Variables


    // Start is called before the first frame update
    void Start()
    {
        
    }

    [MenuItem("Tools/Dynamic NPC Generator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DynamicNPCGenerator));
    }

    //These will be the values actually displayed and changable (depending) for developer use
    private void OnGUI()
    {
        GUILayout.Label("Generate New NPC", EditorStyles.boldLabel);

        npcBaseName = EditorGUILayout.TextField("NPC Name", npcBaseName);
        npcID = EditorGUILayout.IntField("NPC ID", npcID);
        npcScale = EditorGUILayout.Slider("NPC Scale", npcScale, 0.5f, 3f);
        npcSprite = EditorGUILayout.ObjectField("NPC Sprite", npcSprite, typeof(SpriteRenderer), false) as SpriteRenderer;

        if (GUILayout.Button("Generate NPC"))
        {
            //generate NPC into scene
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
