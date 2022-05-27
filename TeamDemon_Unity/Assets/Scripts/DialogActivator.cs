using UnityEngine;

public class DialogActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private GameObject keyboardInstruction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerControls player))
        {
            keyboardInstruction.SetActive(true);
            player.Interactable = this;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerControls player))
        {
            keyboardInstruction.SetActive(false);
            if (player.Interactable is DialogActivator dialogActivator && dialogActivator == this)
                player.Interactable = null;
        }
    }
    public void Interact(PlayerControls player)
    {
        player.Dialogue_UI.ShowDialog(dialogueObject);
    }
}
