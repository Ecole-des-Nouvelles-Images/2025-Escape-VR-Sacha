using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogueEntry
    {
        public string id;
        public AudioClip clip;
        [TextArea]
        public string transcript;
    }

    public List<DialogueEntry> dialogues;
    private Dictionary<string, DialogueEntry> _dialogueMap;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _dialogueMap = new Dictionary<string, DialogueEntry>();

        foreach (var entry in dialogues)
        {
            if (!_dialogueMap.ContainsKey(entry.id))
            {
                _dialogueMap.Add(entry.id, entry);
            }
            else
            {
                Debug.LogWarning($"Dialogue ID '{entry.id}' is duplicated.");
            }
        }
    }

    public void PlayDialogue(string id)
    {
        if (_dialogueMap.ContainsKey(id))
        {
            var dialogue = _dialogueMap[id];
            if (dialogue.clip != null)
            {
                _audioSource.clip = dialogue.clip;
                _audioSource.Play();
            }
            else
            {
                Debug.LogWarning($"AudioClip missing for dialogue '{id}'.");
            }
        }
        else
        {
            Debug.LogWarning($"Dialogue ID '{id}' not found.");
        }
    }

    public bool IsDialoguePlaying()
    {
        return _audioSource != null && _audioSource.isPlaying;
    }

    public void StopDialogue()
    {
        if (_audioSource != null && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
