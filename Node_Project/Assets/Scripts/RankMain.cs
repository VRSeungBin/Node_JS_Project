using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           //유니티 UI 접근
using UnityEngine.Networking;   //유니티 Networking 사용
using System.Text;

public class RankMain : MonoBehaviour
{
    public string host;
    public int port;
    public string top3Uri;
    public string idUri;
    public string postUri;
    public string id;
    public int score;

    public Button BtnGetTop3;
    public Button BtnGetId;
    public Button BtnPost;

    Dictionary<string, int> scoreDic = new Dictionary<string, int>();

    void Start()
    {
        this.BtnGetId.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, idUri + "/" + id);
            Debug.Log(url);

            StartCoroutine(this.GetId(url, (raw) =>
            {
                //var res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores_id>(raw);
                var res = JsonUtility.FromJson<Protocols.Packets.res_scores_id>(raw);
                Debug.LogFormat("{0}, {1}", res.result.id, res.result.score);

            }));

        });

        this.BtnGetTop3.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, top3Uri);
            Debug.Log(url);

            StartCoroutine(this.GetId(url, (raw) =>
            {
                //var res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores_top3>(raw);
                Protocols.Packets.res_scores_top3 res = JsonUtility.FromJson<Protocols.Packets.res_scores_top3>(raw);

                Debug.LogFormat("{0}, {1}", res.cmd, res.result.Length);
                foreach (var user in res.result)
                {
                    Debug.LogFormat("{0} : {1}", user.id, user.score);
                }

            }));
        });

        this.BtnPost.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, postUri);
            Debug.Log(url);

            var req = new Protocols.Packets.req_scores();
            req.cmd = 1000;
            req.id = id;
            req.score = score;
            //var json = JsonConvert.SerializeObject(req);
            var json = JsonUtility.ToJson(req);
            Debug.Log(json);

            StartCoroutine(this.PostScore(url, json, (raw) =>
            {
                //Protocols.Packets.res_scores res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores>(raw);
                Protocols.Packets.res_scores res = JsonUtility.FromJson<Protocols.Packets.res_scores_id>(raw);
                Debug.LogFormat("{0}, {1}", res.cmd, res.message);
            }));

        });
    }


    private IEnumerator PostScore(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json);                         //직렬화 (문자열 -> 바이트 배열)


        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();                           //Node.js 로 보냄

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||                  //각종 네트워크 에러 사항 채킹
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 좋지 않음");
        }
        else
        {
            Debug.LogFormat("{0}/{1}/{2}/", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }

        webRequest.uploadHandler.Dispose();
    }

    private IEnumerator GetId(string url, System.Action<string> callback)
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        Debug.Log("----->" + webRequest.downloadHandler.text);

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||                  //각종 네트워크 에러 사항 채킹
           webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 좋지 않음");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator GetTop3(string url, System.Action<string> callback)
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||                  //각종 네트워크 에러 사항 채킹
           webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 좋지 않음");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }

}