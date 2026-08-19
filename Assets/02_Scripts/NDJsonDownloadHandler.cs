using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

// NDJSON 스트리밍 응답을 처리하는 역할
public class NDJsonDownloadHandler : DownloadHandlerScript
{
    // 토큰(청크) 단위로 호출할 이벤트 선언
    private readonly Action<string> _onToken;
    // \n 종료되지 않은 JSON 토큰을 임시 저장할 버퍼
    private readonly StringBuilder _lineBuffer = new();
    
    // 스트리밍 중에 수신한 모든 토큰을 순서대로 이어 붙이는 버퍼
    private readonly StringBuilder _fullBuffer = new();
    
    // 스트리밍 완료 후 전체 응답 텍스트 (프로퍼티)
    public string FullBuffer => _fullBuffer.ToString();
    
    // 생성자
    public NDJsonDownloadHandler(Action<string> onToken) => _onToken = onToken;
}
