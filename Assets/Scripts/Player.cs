using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Player : NetworkBehaviour, IKitchenObject
{
    public static Player localPlayerInstance { get; private set; }
    private KitchenObject kitchenObject;
    [SerializeField] private Transform topPoint;

    public static event EventHandler OnPickedUpSomething;

    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private LayerMask collisionLayerMask;
    [SerializeField] private List<Vector3> playerSpawnedPosition;
    [SerializeField] private PlayerVisual playerVisual;
    private float moveSpeed = 5f;
    private float rotateSpeed = 15f;
    private bool isWalking = false;
    private bool canMove = false;
    private Vector3 updatedMoveDir = Vector3.zero;
    private BaseCounter selectedCounter;
    public static event EventHandler OnAnyPlayerSpawned;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    public static void ClearStaticData()
    {
        OnAnyPlayerSpawned = null;

        OnPickedUpSomething = null;
    }
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            localPlayerInstance = this;
        }
        OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);
        transform.position = playerSpawnedPosition[(int)OwnerClientId];
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += Player_OnClientDisconnectCallback;
        }
    }
    private void Start()
    {
        GameInput.instance.onInteractionEvent += GameInput_onInteractionEvent;
        GameInput.instance.onInteractionAlternateEvent += GameInput_onInteractionAlternateEvent;
        PlayerData playerData = KitchenGameMultiplayer.Instance.GetPlayerDataFromClientId(OwnerClientId);
        playerVisual.SetPlayerColor(KitchenGameMultiplayer.Instance.GetPlayerColorByColorId(playerData.colorId));

    }
    private void Player_OnClientDisconnectCallback(ulong clientId)
    {
        if (HasKitchenObject() && clientId == OwnerClientId)
        {
            KitchenObject.DestroyKitchenObjectFromServer(GetKitchenObject());
        }
    }
    private void GameInput_onInteractionAlternateEvent(object sender, EventArgs e)
    {
        if (selectedCounter != null && GameManager.GameState.GameStart == GameManager.Instance.GetCurrentGameState())
        {
            selectedCounter.AlternateInteract();
        }
    }

    private void GameInput_onInteractionEvent(object sender, System.EventArgs e)
    {
        if (selectedCounter != null && GameManager.GameState.GameStart == GameManager.Instance.GetCurrentGameState())
        {
            selectedCounter.Interact(this);
        }
    }

    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        HandleMovementAuth();
        HandleInteraction();
    }
    private void HandleInteraction()
    {
        Vector2 inputVector = GameInput.instance.GetNormalizedInput;
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            updatedMoveDir = moveDir;
        }
        float interactDistance = 1f;
        if (Physics.Raycast(transform.position, updatedMoveDir, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                SetBaseCounter(baseCounter);
            }
            else
            {
                SetBaseCounter(null);
            }
        }
        else
        {
            SetBaseCounter(null);
        }
    }
    private void HandleMovementAuth()
    {
        Vector2 inputVector = GameInput.instance.GetNormalizedInput;
        HandleMovementServeRpc(inputVector);
    }
    // [ServerRpc(RequireOwnership = false)]
    private void HandleMovementServeRpc(Vector2 inputVector)
    {

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float playerRadius = 0.7f;
        // float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;
        isWalking = moveDir != Vector3.zero;

        canMove = !Physics.BoxCast(transform.position, Vector3.one * playerRadius, moveDir, Quaternion.identity, moveDistance, collisionLayerMask);
        if (!canMove)
        {
            //Attempt to move towards X
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDirX != Vector3.zero && !Physics.BoxCast(transform.position, Vector3.one * playerRadius, moveDirX, Quaternion.identity, moveDistance, collisionLayerMask);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDirZ != Vector3.zero && !Physics.BoxCast(transform.position, Vector3.one * playerRadius, moveDirZ, Quaternion.identity, moveDistance, collisionLayerMask);
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
    private void SetBaseCounter(BaseCounter baseCounter)
    {
        selectedCounter = baseCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs()
        {
            selectedCounter = selectedCounter
        });
    }
    public bool IsWalking => isWalking;
    public void SetKitchenObject(KitchenObject kitchenObject)
    {

        this.kitchenObject = kitchenObject;

        OnPickedUpSomething?.Invoke(this, EventArgs.Empty);
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return topPoint;
    }
    public NetworkObject GetNetworkObject()
    {
        return NetworkObject;
    }
}
