using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        // 전송 코루틴 호출
        StartCoroutine(PostRequest(messages));
    }

    public IEnumerator PostRequest(List<OllamaMessage> messages)
    {
        // 대화창 UI ( 생각중 ... )
        OnLoadingChanged?.Invoke(true);
        
        Debug.Log("요청 전송중 ...");
        
        // 직렬화 : 객체(클래스) => 문자열(JSON)
        string json = JsonUtility.ToJson(new OllamaRequest
        {
            model = MODEL,
            messages = messages,
            stream = false
        });
        
        // UnityWebRequest 를 using 구문 사용 (자동 메모리 해제)
        using UnityWebRequest request = new UnityWebRequest(API_URL, "POST");

        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.timeout = 60;
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
            
            // JSON 파싱 메시지만 추출 (JSON 문자열 => OllamaResponse 객체로 역직렬화)
            var response = JsonUtility.FromJson<OllamaResponse>(request.downloadHandler.text);
            
            OnResponseReceived?.Invoke(response.message.content);
        }
        else
        {
            Debug.Log(request.error);
        }
        
        OnLoadingChanged?.Invoke(false);
    }
}
