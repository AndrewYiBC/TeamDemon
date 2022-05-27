using UnityEngine;

public class DialogActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerControls player))
        {
            player.Interactable = this;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerControls player))
        {
            player.Interactable = null;
        }
    }
    public void Interact(PlayerControls player)
    {
        player.Dialogue_UI.ShowDialog(dialogueObject);
    }
}
