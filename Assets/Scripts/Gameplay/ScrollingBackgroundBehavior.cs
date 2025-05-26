using UnityEngine;
using UnityEngine.Tilemaps;

public class ScrollingBackgroundBehavior : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _scrollSpeed;


    void Update()
    {
        _renderer.material.mainTextureOffset += new Vector2(0, _scrollSpeed * Time.deltaTime);
    }
}
