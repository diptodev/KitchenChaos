using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IKitchenObject
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform topPoint;
    private float moveSpeed=5f;
    private float rotateSpeed = 15f;
    private bool isWalking = false;
    private bool canMove=false;
    private Vector3 updatedMoveDir=Vector3.zero;
    private ClearCounter selectedCounter;
    private KitchenObject kitchenObject;

    public static Player playerInstance {  get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
      public  ClearCounter selectedCounter;
    }



    private void Awake()
    {
        gameInput.onInteractionEvent += GameInput_onInteractionEvent;
        playerInstance = this;
    }

    private void GameInput_onInteractionEvent(object sender, System.EventArgs e)
    {
        if (selectedCounter !=null)
        {
            selectedCounter.Interact(this);
        }
    }

    void Update()
    {

      HandleMovement();
      HandleInteraction();
    }
    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetNormalizedInput;
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            updatedMoveDir = moveDir;
        }
        float interactDistance = 1f;
        if (Physics.Raycast(transform.position, updatedMoveDir, out RaycastHit raycastHit, interactDistance,counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                SetClearCounter(clearCounter);
            }
            else
            {
                SetClearCounter(null);
            }
        }
        else
        {
            SetClearCounter(null);
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetNormalizedInput;
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed *Time.deltaTime;
        isWalking = moveDir != Vector3.zero;

        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            //Attempt to move towards X
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }

            }
        }
        if (canMove)
        {
            transform.position += moveDir * Time.deltaTime * moveSpeed;

        }
        transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    private void SetClearCounter(ClearCounter _clearCounter)
    {
        selectedCounter = _clearCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs()
        {
            selectedCounter = selectedCounter
        });
    }
    public bool IsWalking => isWalking;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return topPoint;
    }
}
