using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class SubscriptionPolicies
    {
        public string locationPlacementId { get; set; }
        public string quotaId { get; set; }
        public string spendingLimit { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string authorizationSource { get; set; }
        public List<object> managedByTenants { get; set; }
        public string subscriptionId { get; set; }
        public string tenantId { get; set; }
        public string displayName { get; set; }
        public string state { get; set; }
        public SubscriptionPolicies subscriptionPolicies { get; set; }
    }

    public class Count
    {
        public string type { get; set; }
        public int value { get; set; }
    }

    public class APIResponse
    {
        public List<Value> value { get; set; }
        public Count count { get; set; }
    }
}
