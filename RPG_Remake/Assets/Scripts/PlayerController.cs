using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 3; // 移動速度
    [SerializeField] private float jumpPower = 3; // ジャンプ力
    private CharacterController _characterController; // CharacterControllerのキャッシュ
    private Transform _transform; // Transformのキャッシュ
    private Vector3 _moveVelocity; // キャラの移動速度情報

    /// <summary>
    /// 接地判定処理
    /// </summary>
    private bool IsGrounded
    {
        get
        {
            var ray = new Ray(_transform.position + new Vector3(0, 0.1f), Vector3.down);
            var raycastHits = new RaycastHit[1];
            var hitCount = Physics.RaycastNonAlloc(ray, raycastHits, 0.2f);
            return hitCount >= 1;
        }
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>(); 
        _transform = transform; 
    }

    private void Update()
    {
        Debug.Log(IsGrounded ? "地上" : "空中");

        _moveVelocity.x = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;
        _moveVelocity.z = CrossPlatformInputManager.GetAxis("Vertical") * moveSpeed;

        _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));

        if (IsGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _moveVelocity.y = jumpPower; 
            }
        }
        else
        {
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        _characterController.Move(_moveVelocity * Time.deltaTime);

        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
    }
}