using System.Data;
using Rule = ZhangPengFei.IoT.RuleEngine.Core.Rule;

namespace ZhangPengFei.IoT.RuleEngine.RulePaser;

public interface IRuleParser
{
    public Rule ParseRule(string ruleString);
}