namespace ZhangPengFei.IoT.RuleEngine.Core;

public class Rule
{
    private string? Name { get; set; }
    private string? Condition { get; set; }
    private string? Action { get; set; }

    public Rule(string name, string condition, string action)
    {
        Name = name;
        Condition = condition;
        Action = action;
    }
}