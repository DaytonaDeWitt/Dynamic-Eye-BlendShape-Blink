using UnityEngine;

public class DynamicEyeBlink : MonoBehaviour
{

    //blink based on blednershape value using strings for blendshape name//
    public SkinnedMeshRenderer skinnedMeshRenderer;
    //public string blinkShapeName = "Blink"; - make this a serilized field for multiple shape key controlls//

    
    public string blinkShapeName = "Blink"; // Name of the blend shape for blinking
    public float blinkSpeed = 1.0f; // Speed of the blink animation
    private int blinkShapeIndex;
    private float blinkValue = 0.0f; // Current blink value
    public bool isBlinking = false; // Flag to check if blinking is active
    void Start()
    {
        // Get the index of the blink shape
        blinkShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blinkShapeName);
        if (blinkShapeIndex < 0)
        {
            Debug.LogError("Blend shape not found: " + blinkShapeName);
        }
    }

    void Update()
    {
        // Check if the blink shape index is valid
        if (blinkShapeIndex < 0)
            return;

        // If blinking is active, update the blink value
        if (isBlinking)
        {
            blinkValue += Time.deltaTime * blinkSpeed;
            if (blinkValue >= 1.0f)
            {
                blinkValue = 1.0f;
                isBlinking = false; // Stop blinking when fully closed
            }
        }
        else
        {
            // Gradually open the eyes when not blinking
            blinkValue -= Time.deltaTime * blinkSpeed;
            if (blinkValue <= 0.0f)
            {
                blinkValue = 0.0f;
            }
        }

        // Set the blend shape value for blinking
        skinnedMeshRenderer.SetBlendShapeWeight(blinkShapeIndex, blinkValue * 100.0f);
        //blink every 5 seconds//
        if (!isBlinking && Time.time % 5 < 0.1f)
        {
            TriggerBlink(); // Automatically trigger a blink every 5 seconds
        }
    }

    // Call this method to trigger a blink
    public void TriggerBlink()
    {
        isBlinking = true; // Start blinking
        blinkValue = 0.0f; // Reset blink value to start from closed position
    }
}
