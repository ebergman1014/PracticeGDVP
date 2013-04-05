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
using CardShop.Models;

namespace Microsoft.Rules.Samples.RuleSetToolkitUsageSample
{
    public sealed partial class BaseballCardWorkflow : SequentialWorkflowActivity
    {
        public BaseballCardWorkflow()
        {
            InitializeComponent();
        }

        //private BaseballCard baseballcard { get; set; }
        public int BaseballCardId { get; set; }
        public string Player { get; set; }
        public string Team { get; set; }
        public decimal Cost { get; set; }
    }

}
