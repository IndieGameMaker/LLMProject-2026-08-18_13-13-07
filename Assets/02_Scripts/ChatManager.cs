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
    
    // 페르소나 로딩
    private void LoadPersona()
    {
        TextAsset json = Resources.Load<TextAsset>("WizardPersona");
        // 역직렬화 (json => 객체)
        _npcPersona = JsonUtility.FromJson<NpcPersona>(json.text);
        
        _history.Add(new OllamaMessage
        {
            role = "system",
            content = $"당신의 이름은 {_npcPersona.name} 입니다. " +
                      $"성격 : {_npcPersona.personality} " +
                      $"배경 : {_npcPersona.background} " +
                      $"말투 : {_npcPersona.speechStyle} " +
                      "반드시 캐릭터 성격을 유지하고, 한글로 답하세요."
        });

        _npcNameText.text = _npcPersona.name;
        _npcDialogText.text = _npcPersona.greeting;

    }
    
}
