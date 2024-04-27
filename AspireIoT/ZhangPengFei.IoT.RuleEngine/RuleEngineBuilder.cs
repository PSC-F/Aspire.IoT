using ZhangPengFei.IoT.MQ.Common;
using ZhangPengFei.IoT.RuleEngine.RulePaser;

namespace ZhangPengFei.IoT.RuleEngine;

public class RuleEngineBuilder
{
    private NormalMessage _message;
    private Core.RuleEngine _engine;
    private IConfigRuleLoader _configRuleLoader;

    public RuleEngineBuilder ConfigRulesLoader(IConfigRuleLoader loader)
    {
        _configRuleLoader = loader;
        return this;
    }

    public RuleEngineBuilder Build()
    {
        _engine = new Core.RuleEngine();
        if (_configRuleLoader != null)
        {
            var rules = _configRuleLoader.LoadRules();
            _engine.ConfigureRules(rules);
        }

        return this;
    }

    public RuleEngineBuilder ConfigData(string messageValue)
    {
        // 在这里配置数据
        if (_engine != null)
        {
            _engine.ConfigureData(messageValue);
        }

        return this;
    }

    public Core.RuleEngine GetRuleEngine()
    {
        return _engine;
    }

    public RuleEngineBuilder ConfigRulesParse(IRuleParser ruleParser)
    {
        _engine.ConfigRulesParse(ruleParser);
        return this;
    }
}