using System.Text;
using Models;

public static class UiTextFormatter
{
    private static StringBuilder _sb = new StringBuilder();
    
    public static string GetPirateInfoAsText(Pirate pirate)
    {
        _sb.Clear();

        _sb.Append($"HP: {pirate.Hp}/{pirate.MaxHp}\n");
        _sb.Append($"Energy: {pirate.Energy}/{pirate.MaxEnergy}\n");
        _sb.Append($"Attack: {pirate.AttackingPoints}\n");
        _sb.Append($"Mining: {pirate.MiningPoints}\n");
        _sb.Append($"Cooking: {pirate.CookingPoints}\n");

        return _sb.ToString();
    }

    public static string GetSpecialFormatted(Special special)
    {
        _sb.Clear();

        _sb.Append($"Damage: {special.Damage}\n");
        _sb.Append($"Heal: {special.Heal}\n");
        _sb.Append($"Range: {special.Range}\n");
        _sb.Append($"Energy: {special.Energy}\n");
        
        return _sb.ToString();
    }
}