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

    private void Awake()
    {
        _ollamaClient = GetComponent<OllamaClient>();
        LoadPersona();
    }

    private void OnEnable()
    {
        _sendButton.onClick.AddListener(OnSendButtonClick);
        // 응답이 수신되었을 때 호출되는 이벤트에 연결
        OllamaClient.OnResponseReceived += ResponseHandler;
    }

    private void OnDisable()
    {
        _sendButton.onClick.RemoveListener(OnSendButtonClick);
        OllamaClient.OnResponseReceived -= ResponseHandler;
    }
    
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

    private void OnSendButtonClick()
    {
        string userText = _userInputField.text.Trim();
        if (string.IsNullOrEmpty(userText)) return;
        
        // 사용자가 입력한 메시지를 히스토리에 저장 -> 전체 메시지(_history)를 LLM 전송 -> 응답 -> UI갱신
        _history.Add(new OllamaMessage
        {
            role = "user",
            content = userText
        });

        // UI 정리
        _userInputField.text = string.Empty;
        _npcDialogText.text = "NPC 생각 중 ...";
        // 전체 메세지 전송 => LLM
        _ollamaClient.SendChat(_history);
    }

    private void ResponseHandler(string response)
    {
        // 대화 맥락 유지용 히스토리에 추가
        _history.Add(new OllamaMessage
        {
            role = "assistant",
            content = response
        });

        _npcDialogText.text = response;
    }
}
