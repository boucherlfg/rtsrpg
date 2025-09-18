#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

[EditorToolbarElement(id, typeof(SceneView))]
public class RecompileToolbarButton : EditorToolbarButton
{
    public const string id = "Tools/RecompileToolbarButton";

    public RecompileToolbarButton()
    {
        text = "Recompile";
        tooltip = "Force scripts to recompile";
        clicked += UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation;
    }
}
#endif