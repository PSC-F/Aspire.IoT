namespace ZhangPengFei.IoT.RuleEngine.Core;

public interface IRuleEngine
{
    /// <summary>
    /// 添加规则
    /// </summary>
    /// <param name="rule"></param>
    void AddRule(Rule rule);

    /// <summary>
    ///  移除规则
    /// </summary>
    /// <param name="rule"></param>
    void RemoveRule(Rule rule);

    /// <summary>
    /// 执行规则
    /// </summary>
    /// <param name="facts"></param>
    void ExecuteRules(Facts facts);

    /// <summary>
    /// 初始化引擎
    /// </summary>
    /// <param name="callback"></param>
    bool InitEngine(string message);

    // 监听topic是否变化
    bool On(string topic);

    /// <summary>
    /// 重新加载
    /// </summary>
    /// <returns></returns>
    bool ReloadRuleEngine();
}