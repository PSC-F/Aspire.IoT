using ZhangPengFei.IoT.MQ.Common;

namespace ZhangPengFei.IoT.RuleEngine.RulePaser;

public class ConfigDefaultRuleLoader : IConfigRuleLoader
{
    private string _topic;

    public ConfigDefaultRuleLoader(string topic)
    {
        _topic = topic;
    }

    public string LoadRules()
    {
        throw new NotImplementedException();
    }
}