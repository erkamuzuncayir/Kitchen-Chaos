using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: This code will be refactored and replaced with new Input System.
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    
    private bool _isWalking;

    private void Update()
    
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        Vector3 playerMovement = moveDir * (moveSpeed * Time.deltaTime);
        transform.position += playerMovement;

        _isWalking = moveDir != Vector3.zero;
        
        const float rotate_speed = 10f;
        Vector3 playerRotate = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotate_speed);
        transform.forward = playerRotate;
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
}
