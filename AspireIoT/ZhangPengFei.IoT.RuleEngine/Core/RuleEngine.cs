using ZhangPengFei.IoT.RuleEngine.RulePaser;

namespace ZhangPengFei.IoT.RuleEngine.Core;

public class RuleEngine : IRuleEngine
{
    private string _message;


    public void AddRule(Rule rule)
    {
        throw new NotImplementedException();
    }

    public void RemoveRule(Rule rule)
    {
        throw new NotImplementedException();
    }

    public void ExecuteRules(Facts facts)
    {
        throw new NotImplementedException();
    }

    public bool InitEngine(string message)
    {
        throw new NotImplementedException();
    }


    public bool On(string topic)
    {
        throw new NotImplementedException();
    }

    public bool ReloadRuleEngine()
    {
        throw new NotImplementedException();
    }

    public void ConfigureRules(object rules)
    {
        throw new NotImplementedException();
    }

    public void ConfigureData(string messageValue)
    {
        throw new NotImplementedException();
    }

    public void ConfigRulesParse(IRuleParser rules)
    {
        throw new NotImplementedException();
    }
}