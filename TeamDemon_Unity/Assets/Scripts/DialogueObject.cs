using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue/DialogObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;

    public string[] Dialogue => dialogue;

}
