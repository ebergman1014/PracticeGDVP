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
    public sealed partial class StoreWorkflow : SequentialWorkflowActivity
    {
        public StoreWorkflow()
        {
            InitializeComponent();
        }

        public int StoreId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal DiscountRate { get; set; }
    }

}
