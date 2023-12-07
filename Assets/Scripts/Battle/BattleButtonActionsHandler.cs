using UnityEngine;

public class BattleButtonActionsHandler : MonoBehaviour
{
    [SerializeField] 
    private GameObject canvas;

    [SerializeField]
    private GameObject actionsGroup;
    
    [SerializeField]
    private GameObject enemySelectionGroup;

    [SerializeField]
    private GameObject specialAttacksGroup;

    [SerializeField] 
    private GameObject specialAttackInfo;

    private BattleManager _battleManager;

    private GameObject[] _specialAttackButtons;
    
    void Start()
    {
        canvas = GameObject.Find("Canvas");

        actionsGroup = canvas.transform.Find("ActionsGroup").gameObject;
        enemySelectionGroup = canvas.transform.Find("EnemySelectionGroup").gameObject;
        specialAttacksGroup = canvas.transform.Find("SpecialAttacksGroup").gameObject;
        specialAttackInfo = canvas.transform.Find("SpecialAttackInfo").gameObject;
        
        _battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

        _specialAttackButtons = new GameObject[4];
        for (var i = 0; i < _specialAttackButtons.Length; i++)
            _specialAttackButtons[i] = specialAttacksGroup.transform.Find($"Slot {i + 1}").gameObject;
    }

    public void SetAvailableEnemyButtons(int numberOfEnemies)
    {
        if(enemySelectionGroup == null) enemySelectionGroup = GameObject.Find("Canvas").transform.Find("EnemySelectionGroup").gameObject;
        
        for (var i = 0; i < numberOfEnemies; i++)
        {
            enemySelectionGroup.transform.Find($"Slot {i + 1}").gameObject.SetActive(true);
        }
    }

    public void DeactivateEnemyButton(int index)
    {
        enemySelectionGroup.transform.Find($"Slot {index + 1}").gameObject.SetActive(true);
    }

    public void OnAttackButtonPressed()
    {
        HideAllPanels();
        enemySelectionGroup.SetActive(true);
    }

    public void OnAttackButtonPressed(int index)
    {
        _battleManager.ExecutePlayerAttack(index);
    }

    public void OnSpecialAttackButtonPressed()
    {
        HideAllPanels();
        specialAttacksGroup.SetActive(true);

        var specials = _battleManager.GetAttackerSpecialAttacks();

        HideAllSpecialButtons();

        for (var i = 0; i < specials.Count; i++)
        {
            _specialAttackButtons[i].SetActive(true);
            _specialAttackButtons[i].GetComponent<SpecialAttackButtonHandler>().SetSpecialAttack(specials[i]);
        }
    }

    private void HideAllSpecialButtons()
    {
        for (var i = 0; i < 4; i++) specialAttacksGroup.transform.Find($"Slot {i + 1}").gameObject.SetActive(false);
    }

    public void OnSpecialAttackButtonPressed(int index)
    {
        HideAllPanels();
        specialAttackInfo.SetActive(false);
        actionsGroup.SetActive(true);
    }
    
    public void SetActionsPanel()
    {
        HideAllPanels();
        actionsGroup.SetActive(true);
    }

    public void HideAllPanels()
    {
        actionsGroup.SetActive(false);
        enemySelectionGroup.SetActive(false);
        specialAttacksGroup.SetActive(false);
    }

}
