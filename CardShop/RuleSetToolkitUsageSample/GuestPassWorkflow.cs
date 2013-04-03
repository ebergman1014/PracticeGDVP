using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Microsoft.Rules.Samples.RuleSetToolkitUsageSample
{
    public sealed partial class GuestPassWorkflow : SequentialWorkflowActivity
    {
        public GuestPassWorkflow()
        {
            InitializeComponent();
        }

        private int CustomerRegionId;
        private decimal Discount;
        private string Expiration;

        private string ExpirationDate()
        {
            return this.Expiration;
        }
    }
}
