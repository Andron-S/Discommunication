using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    protected PlayerInput Input;
    private Player _player;

    public virtual void Awake()
    {
        _player = GetComponent<Player>();

        Input = new PlayerInput();
        Input.Player.Enable();

        Input.Player.Attack.performed += rangeContext => _player.AttackWeapon();
        Input.Player.EatAbility.performed += eatContext => _player.EatAbility();
    }

    private void OnEnable()
    {
        Input.Enable();
    }

    private void OnDisable()
    {
        Input.Disable();
    }
}
