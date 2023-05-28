using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Animation Stuff")]
    public Animator animator;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxWalkVelocity = 0.3f;
    public float maxRunVelocity = 1.0f;

    private int velocityZHash;
    private int velocityXHash;
    private float velocityX = 0.0f;
    private float velocityZ = 0.0f;
    Terpenmek terpenmek;

    private void Start()
    {
        InitializeAnimator(); // Initialize the animator component and get the hash codes for animation parameters.
    }

    private void InitializeAnimator()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the game object.
        velocityZHash = Animator.StringToHash("vertical"); // Get the hash code for the "vertical" parameter.
        velocityXHash = Animator.StringToHash("horizontal"); // Get the hash code for the "horizontal" parameter.
    }

    private void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W); // Check if the forward key is pressed.
        bool leftPressed = Input.GetKey(KeyCode.A); // Check if the left key is pressed.
        bool rightPressed = Input.GetKey(KeyCode.D); // Check if the right key is pressed.
        bool runPressed = Input.GetKey(KeyCode.LeftShift); // Check if the run key is pressed.
        bool backPressed = Input.GetKey(KeyCode.S); // Check if the backward key is pressed.
        bool aiming = Input.GetKey(KeyCode.Mouse1) ? true:false ;

        
        float currentMaxVelocity = runPressed && !aiming ? maxRunVelocity : maxWalkVelocity; // Determine the maximum velocity based on whether the run key is pressed.
        
       

        UpdateVelocity(forwardPressed, leftPressed, rightPressed, runPressed, backPressed, currentMaxVelocity); // Update the current velocity based on input.
        LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, backPressed, currentMaxVelocity); // Lock or reset the velocity based on input.

        UpdateAnimator(); // Update the animator parameters based on the current velocity.
    }

    private void UpdateVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, bool backPressed, float currentMaxVelocity)
    {
        // Update the Z-axis velocity (forward/backward movement).
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration; // Accelerate forward.
        }
        if (backPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration; // Accelerate backward.
        }

        // Update the X-axis velocity (sideways movement).
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration; // Accelerate left.
        }
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration; // Accelerate right.
        }

        // Decelerate if the corresponding movement key is released.
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration; // Decelerate forward.
        }
        if (!backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration; // Decelerate backward.
        }
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration; // Decelerate left.
        }
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration; // Decelerate right.
        }
    }

    private void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, bool backPressed, float currentMaxVelocity)
    {
        // Lock or reset the Z-axis velocity based on input.
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration; // Decelerate forward.
        }
        else if (!backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration; // Decelerate backward.
        }
        else if (backPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration; // Decelerate backward.
            if (velocityZ < -currentMaxVelocity)
            {
                velocityZ = -currentMaxVelocity; // Limit the backward velocity to the maximum.
            }
        }

        // Reset the X-axis velocity if no sideways movement keys are pressed.
        if (!leftPressed && !rightPressed && Mathf.Abs(velocityX) < 0.05f)
        {
            velocityX = 0.0f; // Reset the X-axis velocity.
        }
        else if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration; // Decelerate left.
            if (velocityX > 0.0f)
            {
                velocityX = 0.0f; // Reset the X-axis velocity if it becomes positive.
            }
        }
        else if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration; // Decelerate right.
            if (velocityX < 0.0f)
            {
                velocityX = 0.0f; // Reset the X-axis velocity if it becomes negative.
            }
        }

        // Limit or adjust the Z-axis velocity based on input.
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity; // Limit the forward velocity to the maximum when running.
        }
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration; // Decelerate forward.
            if (velocityZ > currentMaxVelocity && velocityZ < currentMaxVelocity + 0.05f)
            {
                velocityZ = currentMaxVelocity; // Adjust the forward velocity to the maximum within a small range.
            }
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > currentMaxVelocity - 0.05f)
        {
            velocityZ = currentMaxVelocity; // Adjust the forward velocity to the maximum within a small range.
        }

        // Limit or adjust the Z-axis velocity based on input when moving backward.
        if (backPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity; // Limit the backward velocity to the maximum when running.
        }
        else if (backPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration; // Decelerate backward.
            if (velocityZ < -currentMaxVelocity && velocityZ > -currentMaxVelocity - 0.05f)
            {
                velocityZ = -currentMaxVelocity; // Adjust the backward velocity to the maximum within a small range.
            }
        }
        else if (backPressed && velocityZ > -currentMaxVelocity && velocityZ < -currentMaxVelocity + 0.05f)
        {
            velocityZ = -currentMaxVelocity; // Adjust the backward velocity to the maximum within a small range.
        }

        // Limit or adjust the X-axis velocity based on input when moving sideways.
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity; // Limit the left velocity to the maximum when running.
        }
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration; // Decelerate left.
            if (velocityX < -currentMaxVelocity && velocityX > -currentMaxVelocity - 0.05f)
            {
                velocityX = -currentMaxVelocity; // Adjust the left velocity to the maximum within a small range.
            }
        }
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < -currentMaxVelocity + 0.05f)
        {
            velocityX = -currentMaxVelocity; // Adjust the left velocity to the maximum within a small range.
        }

        // Limit or adjust the X-axis velocity based on input when moving sideways.
        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity; // Limit the right velocity to the maximum when running.
        }
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration; // Decelerate right.
            if (velocityX > currentMaxVelocity && velocityX < currentMaxVelocity + 0.05f)
            {
                velocityX = currentMaxVelocity; // Adjust the right velocity to the maximum within a small range.
            }
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > currentMaxVelocity - 0.05f)
        {
            velocityX = currentMaxVelocity; // Adjust the right velocity to the maximum within a small range.
        }
    }

    private void UpdateAnimator()
    {
        animator.SetFloat(velocityZHash, velocityZ); // Update the animator's "vertical" parameter with the Z-axis velocity.
        animator.SetFloat(velocityXHash, velocityX); // Update the animator's "horizontal" parameter with the X-axis velocity.
    }
}
