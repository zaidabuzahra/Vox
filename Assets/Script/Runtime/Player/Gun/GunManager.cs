using DG.Tweening;
using Runtime.Player.Gun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    //Utility interface reference
    public IUtility[] utilities = new IUtility[3];

    private IUtility _currentUtility;
    private Animator _animator;

    private int _currentUtilityIndex = 0;
    private int _maxUtilityIndex = -1;
    public float _utilitySwitchCooldown = 0.5f;
    public float _utilityEquipCooldown = 0.5f;

    private const int UTILITY_CHARGE_MAX = 100;
    private float _utilityCharge = UTILITY_CHARGE_MAX;
    public int _utilityChargeRate = 1;
    public int _utilityUseRate = 2;

    private bool _utilityShut = false;
    private bool _canUseUtility = false;

    public Animator Animator => _animator;
    public Slider utilitySlider;

    private void OnEnable()
    {
        InputSignals.Instance.OnSwitchUtility += SwitchUtility;
        InputSignals.Instance.OnUseUtilityPressed += UseUtility;
        InputSignals.Instance.OnUseUtilityCancelled += StopUtility;
    }

    private void OnDisable()
    {
        if (InputSignals.Instance != null)
        {
            InputSignals.Instance.OnSwitchUtility -= SwitchUtility;
            InputSignals.Instance.OnUseUtilityPressed -= UseUtility;
        }
    }

    private void Update()
    {
        if (_currentUtility != null && _canUseUtility)
        {
            if (_currentUtility.isActive)
            {
                _utilityCharge -= Time.deltaTime * _utilityUseRate;
            }
            else
            {
                _utilityCharge += Time.deltaTime * _utilityChargeRate;
            }

            _utilityCharge = Mathf.Clamp(_utilityCharge, 0, UTILITY_CHARGE_MAX);

            if (_utilityCharge <= 0)
            {
                _currentUtility.StopUtility();
                _canUseUtility = false;
                _utilityShut = true;
            }
        }
        if (!_canUseUtility && _utilityShut)
        {
            _utilityCharge += Time.deltaTime * _utilityChargeRate;
            _utilityCharge = Mathf.Clamp(_utilityCharge, 0, UTILITY_CHARGE_MAX);
            if (_utilityCharge == UTILITY_CHARGE_MAX)
            {
                _canUseUtility = true;
                _utilityShut = false;
            }
        }
        if (_currentUtility != null)
        {
            _currentUtility.utilityCharge = utilitySlider.value;
        }
        utilitySlider.value = _utilityCharge / 100;
    }
    private void SwitchUtility(int i)
    {
        if (!_canUseUtility) return;
        _canUseUtility = false;
        int tempIndex = _currentUtilityIndex + i;
        if (tempIndex > _maxUtilityIndex)
        {
            tempIndex = 0;
        }
        else if (tempIndex < 0)
        {
            tempIndex = _maxUtilityIndex;
        }

        if (tempIndex == _currentUtilityIndex || !utilities[tempIndex])
        {
            Debug.LogWarning("No utility to switch to");
            _canUseUtility = true;
            return;
        }

        _currentUtilityIndex = tempIndex;

        if (utilities[_currentUtilityIndex] != null)
        {
            if (_currentUtility != null)
            {
                _currentUtility.Unequip();
                //transform.DOLocalRotate(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 60 * i), 0.5f);
            }
            StartCoroutine(AssignUtility());
        }
    }

    IEnumerator AssignUtility()
    {
        yield return new WaitForSeconds(_utilitySwitchCooldown);

        _currentUtility = utilities[_currentUtilityIndex];
        _currentUtility.Equip();
        yield return new WaitForSeconds(_utilityEquipCooldown);
        _canUseUtility = true;
    }

    private void UseUtility()
    {
        if (_currentUtility == null || !_canUseUtility)
        {
            Debug.Log("Can't use utility");
            return;
        }
        _currentUtility.UseUtility();
    }

    private void StopUtility()
    {
        if (_currentUtility == null || !_canUseUtility || !_currentUtility.stopUtility) return;
        _currentUtility.StopUtility();
    }

    public void SetUtilityState(bool state)
    {
        _canUseUtility = state;
    }
    public void AddUtility(GameObject newUtility)
     {
        //_animator.SetTrigger("Pick Up Utility");
        GameObject utility = Instantiate(newUtility, transform);
        _maxUtilityIndex++;
        _currentUtilityIndex = _maxUtilityIndex;

        if (utility.GetComponent<UVLightUtility>())
        {
            InputSignals.Instance.PickUp?.Invoke(utility);
        }
        utilities[_maxUtilityIndex] = utility.GetComponent<IUtility>();
        if (_currentUtility != null)
        {
            _currentUtility.Unequip();
            //transform.DOLocalRotate(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 60), 0.5f);
        }
        StartCoroutine(AssignUtility());
    }
}