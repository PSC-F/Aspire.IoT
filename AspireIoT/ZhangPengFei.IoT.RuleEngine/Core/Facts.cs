using System.Collections.Generic;

namespace ZhangPengFei.IoT.RuleEngine.Core
{
    public class Facts
    {
        private Dictionary<string, object> factsDictionary = new Dictionary<string, object>();

        public void AddFact(string name, object value)
        {
            factsDictionary[name] = value;
        }

        public object GetFact(string name)
        {
            // 使用 ContainsKey 方法检查键是否存在
            if (factsDictionary.ContainsKey(name))
            {
                return factsDictionary[name];
            }
            else
            {
                // 如果键不存在，则返回 null 或者抛出异常，具体取决于你的需求
                return null;
            }
        }
    }
}