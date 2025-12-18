using UnityEngine;

public class SensitivityController : MonoBehaviour
{
    public float sensitivity = 1f;

    public void SliderChange(float value) {
        sensitivity = value;
    }


}
