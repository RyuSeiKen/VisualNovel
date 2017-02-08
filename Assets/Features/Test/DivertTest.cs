using UnityEngine;
using Ink.Runtime;

public class DivertTest : MonoBehaviour
{

    public TextAsset inkJSONAsset;
    private Story story;

    public void Start()
    {
        story = new Story(inkJSONAsset.text);

        Path newDivert = new Path("success");
        story.variablesState["DIVERT"] = newDivert;

        if (story.canContinue)
        {
            Debug.Log(story.ContinueMaximally());
        }
    }
    
}
