using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject winText;
    public TextMeshProUGUI countText;

    private float speed = 10f;
    private int count;
    private Rigidbody rb;
    private Vector2 movementVector;
    private Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;
        SetCountText();
    }

    private void FixedUpdate()
    {
        direction = new Vector3(movementVector.x, 0f, movementVector.y);
        rb.AddForce(direction * speed);
    }

    //Using PlayerInput Component "SendMessage" Behavior
    private void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector);
    }

    //Using PlayerInput Component "Invoke Unity Events" Behavior
    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementVector = ctx.ReadValue<Vector2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count >= 16)
        {
            winText.SetActive(true);
        }
    }
}
