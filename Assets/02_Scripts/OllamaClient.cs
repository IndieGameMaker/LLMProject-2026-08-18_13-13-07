using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Ollama REST API 사용해서 통신
// 엔드포인트 => POST http://localhost:11434/api/chat
public class OllamaClient : MonoBehaviour
{
    // 엔드포인트
    private const string API_URL = "http://localhost:11434/api/chat";
    // 모델
    private const string MODEL = "gemma4:e2b";
    
    // 이벤트 : LLM 응답 도착 / 로딩 상태 변경
    public static event Action<string> OnResponseReceived;
    public static event Action<bool> OnLoadingChanged;
    
    // 요청 코루틴을 가동하는 래퍼 메서드 (Send Button이 호출할 메서드)
    public void SendChat(List<OllamaMessage> messages)
    {
        // TODO: 전송 코루틴 호출
    }

    public IEnumerator PostRequest(List<OllamaMessage> messages)
    {
        // 대화창 UI ( 생각중 ... )
        OnLoadingChanged?.Invoke(true);

        // 직렬화 : 객체(클래스) => 문자열(JSON)
        string json = JsonUtility.ToJson(new OllamaRequest
        {
            model = MODEL,
            messages = messages,
            stream = false
        });
        
        // UnityWebRequest 를 using 구문 사용 (자동 메모리 해제)
        using UnityWebRequest request = new UnityWebRequest(API_URL, "POST");

        
        
        OnLoadingChanged?.Invoke(false);
    }
}
