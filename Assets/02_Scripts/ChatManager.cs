using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// NPC 페르소나 로딩 -> 대화 히스토리 관리 -> UI 갱신
public class ChatManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _npcNameText;
    [SerializeField] private TextMeshProUGUI _npcDialogText;
    [SerializeField] private TMP_InputField _userInputField;
    [SerializeField] private Button _sendButton;
    
    private OllamaClient _ollamaClient;
    private NpcPersona _npcPersona;
    
    // 대화 히스토리 관리를 위한 컬렉션
    public List<OllamaMessage> _history = new();
}
