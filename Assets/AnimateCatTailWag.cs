using UnityEngine;

public class AnimateCatTailWag : MonoBehaviour
{
    public Sprite[ ] frames;
    public float framesPerSecond = 10.0f;

    void Update( )
    {
        int index = (int)(Time.time * framesPerSecond) % frames.Length;
        GetComponent<SpriteRenderer>().sprite = frames[index];
    }
}