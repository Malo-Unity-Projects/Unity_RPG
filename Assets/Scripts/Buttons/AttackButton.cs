using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public Text damageText;
    public Text attackText;
    public bool wait;
    ColorBlock colors;

    // Start is called before the first frame update
    void Start()
    {
        colors = this.colors;
        wait = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetButton(MutantAttack attack, int damage)
    {
        damageText.text = damage.ToString();
        attackText.text = attack.attackName;
        colors = GetComponent<Button>().colors;

        switch (attack.color) {
            case (MutantAttack.AttackColor.BLUE):
                colors.normalColor = Color.blue;
                colors.highlightedColor = Color.blue;
                colors.pressedColor = Color.blue;
                colors.selectedColor = Color.blue;
                break;
            case (MutantAttack.AttackColor.GREEN):
                colors.normalColor = Color.green;
                colors.highlightedColor = Color.green;
                colors.pressedColor = Color.green;
                colors.selectedColor = Color.green;
                break;
            case (MutantAttack.AttackColor.RED):
                colors.normalColor = Color.red;
                colors.highlightedColor = Color.red;
                colors.pressedColor = Color.red;
                colors.selectedColor = Color.red;
                break;
        }
        GetComponent<Button>().colors = colors;
    }

    public void OnClick()
    {
        wait = false;
    }
}
