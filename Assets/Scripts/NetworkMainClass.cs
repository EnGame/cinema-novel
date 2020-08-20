using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Web
{
    public class NetworkMainClass : MonoBehaviour
    {
        private const string Url = "https://picsum.photos";

        public static IEnumerator GetJsonFromServer(string uri, Action<string> successCallback = null,
            Action<string> errorCallback = null)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(Url + uri))
            {
                var token = PlayerPrefs.GetString("token");

                print($"<color=gray><b>LOG::GET_DATA_FROM_SERVER::</b> {Url}{uri}</color>");
                if (token != "") www.SetRequestHeader("Authorization", "Bearer " + token);

                yield return www.SendWebRequest();

                if (www.isNetworkError)
                {
                    print($"<color=red>NETWORK ERROR::{www.error}</color>");
                }
                else
                {
                    if (www.responseCode == 200 && successCallback != null) successCallback(www.downloadHandler.text);
                    else
                    {
                        errorCallback?.Invoke("<color=red>Error:</color>" + Url + uri + "  " +
                                              www.downloadHandler.text);
                    }
                }
            }
        }

        public static IEnumerator SendJsonToServer(string uri, WWWForm form, Action<string> successCallback = null,
            Action<string> errorCallback = null)
        {
            var token = PlayerPrefs.GetString("token");

            print($"<color=gray><b>LOG::SEND_DATA_TO_SERVER::</b> {Url}{uri}</color>");

            UnityWebRequest www = UnityWebRequest.Post(Url + uri, form);

            if (token != "") www.SetRequestHeader("Authorization", "Bearer " + token);

            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                print($"<color=red>NETWORK ERROR::{www.error}</color>");
            }
            else
            {
                if (www.responseCode == 200 && successCallback != null) successCallback(www.downloadHandler.text);
                else
                {
                    errorCallback?.Invoke(www.downloadHandler.text);
                }
            }
        }

        public static IEnumerator GetImageFromServer(string uri, Action<Texture2D> action)
        {
            print($"<color=gray><b>LOG::GET_IMAGEDATA_FROM_SERVER::</b> {Url}{uri}</color>");

            UnityWebRequest request = UnityWebRequestTexture.GetTexture(Url + uri);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
                action(((DownloadHandlerTexture) request.downloadHandler).texture);
        }
    }
}