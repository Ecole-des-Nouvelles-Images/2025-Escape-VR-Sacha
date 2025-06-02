using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
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
        private Coroutine _pendingDialogue;

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

        private void Start()
        {
       
            StartCoroutine(PlaySequence());
        }
    
        private IEnumerator PlaySequence()
        {
            PlayDialogue("1",5f);
            yield return new WaitWhile(IsDialoguePlaying);

            yield return new WaitForSeconds(1f);

            PlayDialogue("2");
        }

        public void PlayDialogue(string id, float delay = 0f)
        {
            if (_dialogueMap.TryGetValue(id, out DialogueEntry dialogue) && dialogue.clip != null)
            {
                if (_pendingDialogue != null)
                    StopCoroutine(_pendingDialogue);

                _pendingDialogue = StartCoroutine(PlayWithDelay(dialogue.clip, delay));
            }
            else
            {
                Debug.LogWarning($"Dialogue ID '{id}' not found or missing AudioClip.");
            }
        }

        private IEnumerator PlayWithDelay(AudioClip clip, float delay)
        {
            if (delay > 0f)
                yield return new WaitForSeconds(delay);

            _audioSource.clip = clip;
            _audioSource.Play();
            _pendingDialogue = null;
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
}
