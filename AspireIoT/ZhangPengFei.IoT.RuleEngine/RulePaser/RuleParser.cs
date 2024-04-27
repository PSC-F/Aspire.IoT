using ZhangPengFei.IoT.RuleEngine.Core;

namespace ZhangPengFei.IoT.RuleEngine.RulePaser;

public class RuleParser : IRuleParser
{
    public Rule ParseRule(string ruleString)
    {
        // 解析规则字符串，生成 Rule 对象
        //格式：name:condition=>action
        string[] parts = ruleString.Split("=>");
        string[] nameAndCondition = parts[0].Split(":");
        string name = nameAndCondition[0].Trim();
        string condition = nameAndCondition[1].Trim();
        string action = parts[1].Trim();

        return new Rule(name, condition, action);
    }
}