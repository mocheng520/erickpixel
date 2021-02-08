using UnityEngine;

public class BarScaler : MonoBehaviour
{
    public void SetNewBarScale(Vector2 scale)
    {
        transform.localScale = scale;
    }
}
