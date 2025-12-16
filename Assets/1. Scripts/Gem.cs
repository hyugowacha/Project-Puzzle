using UnityEngine;

public class Gem : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log($"Clicked: {transform.position.x}, {transform.position.y}");
    }

}
