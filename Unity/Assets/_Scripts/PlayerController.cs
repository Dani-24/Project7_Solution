using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject ImpactPrefab;
 
    // Start is called before the first frame update
    private bool _isAttacking;
    private bool _isBlocking;

    private UpDown UpOrDown;

    private bool CanAttack => !_isAttacking && !_isBlocking;
    private bool CanBlock => !_isAttacking && !_isBlocking;

    public bool Dead => _dead;




    #region AnimationParamNames
    const string SPEED = "Speed";
    const string ATTACK_HIGH_QUICK = "AttackHighQuick";
    const string ATTACK_HIGH_SLOW = "AttackHighSlow";
    const string ATTACK_LOW_QUICK = "AttackLowQuick";
    const string ATTACK_LOW_SLOW = "AttackLowSlow";

    const string BLOCK_HIGH = "BlockHigh";
    const string BLOCK_LOW = "BlockLow";


    const string DIE = "Die";
    const string WIN = "Win";

    

    #endregion

    private Animator _animator;
    private Transform _otherPlayer;

    static int _playercount;
    int _id;
    private bool _dead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _id = _playercount++;
    }

    public void SetOtherPlayer(Transform other)
    {
        _otherPlayer = other;
    }

    internal void SetAtacking(bool value, UpDown upDown)
    {
        _isAttacking = value;
        UpOrDown = upDown;
    }

   

    internal void SetBlocking(bool value, UpDown upDown)
    {
        _isBlocking = value;
        UpOrDown = upDown;
    }

    public void TryHighQuickAttack()
    {
        if (CanAttack)
            _animator.SetTrigger(ATTACK_HIGH_QUICK);
    }
    public void TryHighSlowAttack()
    {
        if (CanAttack)
            _animator.SetTrigger(ATTACK_HIGH_SLOW);
    }
    public void TryLowQuickAttack()
    {
        if (CanAttack)
            _animator.SetTrigger(ATTACK_LOW_QUICK);
    }
    public void TryLowSlowAttack()
    {
        if (CanAttack)
            _animator.SetTrigger(ATTACK_LOW_SLOW);
    }

    internal void TryHighBlock()
    {
        if (CanBlock)
            _animator.SetTrigger(BLOCK_HIGH);
    }

    internal void TryLowBlock()
    {
        if (CanBlock)
            _animator.SetTrigger(BLOCK_LOW);
    }


    public void OnHit(Transform hit)
    {
        var hitBy = hit.root.GetComponent<PlayerController>();
        if (hitBy.transform == _otherPlayer && hitBy._isAttacking)
        {
            if (!_isBlocking || hitBy.UpOrDown!=this.UpOrDown || hitBy.Dead)
            {
                Die();
                hitBy.Win();
                Instantiate(ImpactPrefab, hit.position, Quaternion.identity);
            }
            
        }
        

        
    }

    private void Die()
    {
        _animator.SetTrigger(DIE);

      //  GetComponent<AudioSource>().Play();
        StartCoroutine(DieLater());


    }

    IEnumerator DieLater()
    {
        yield return new WaitForSeconds(0.1f);
        _dead = true;
        yield return new WaitForSeconds(5);
        PlayerStart.nPLayers = 0;
        _playercount = 0;
        MovementController._playercount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Win()
    {
        _animator.SetTrigger(WIN);
    }
}

public enum UpDown
{
    Up,
    Down
}
