using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using System;

namespace Cards
{
    public sealed class TextureCache : MonoBehaviour
    {
        private const int Width = 300;
        private const int Height = 200;
        
        private readonly Rect _spriteRect = new Rect(0, 0, Width, Height);
        private readonly Vector2 _spritePivot = new Vector2(0.5f, 0.5f);

        private Dictionary<int, Sprite> _cache = new Dictionary<int, Sprite>();
        private Dictionary<int, List<Action<Sprite>>> _callbacks = new Dictionary<int, List<Action<Sprite>>>();

        public void GetTexture(int id, Action<Sprite> callback)
        {
            if (_cache.ContainsKey(id))
            {
                callback?.Invoke(_cache[id]);
                return;
            }

            if (_callbacks.ContainsKey(id))
            {
                _callbacks[id].Add(callback);
                return;
            }

            _callbacks.Add(id, new List<Action<Sprite>>() { callback });

            StartCoroutine(DownloadTexture(id));
        }

        private IEnumerator DownloadTexture(int id)
        {
            var url = $"https://picsum.photos/{Width}/{Height}";
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                var texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                var sprite = Sprite.Create(texture, _spriteRect, _spritePivot);
                _cache.Add(id, sprite);
                
                var callbacks = _callbacks[id];
                foreach (var callback in callbacks)
                {
                    callback.Invoke(sprite);
                }
                
                _callbacks.Remove(id);
            }
        }
    }
}