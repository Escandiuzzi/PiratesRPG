using System.Text;

public static class UiTextFormatter 
{
    public static string GetPirateInfoAsText(Pirate pirate)
    {
        var statsText = new StringBuilder();

        statsText.Append($"HP: {pirate.Hp}/{pirate.MaxHp}\n");
        statsText.Append($"Energy: {pirate.Energy}/{pirate.MaxEnergy}\n");
        statsText.Append($"Attack: {pirate.AttackingPoints}\n");
        statsText.Append($"Mining: {pirate.MiningPoints}\n");
        statsText.Append($"Cooking: {pirate.CookingPoints}\n");

        return statsText.ToString();
    }
}
