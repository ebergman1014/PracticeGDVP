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
    public sealed partial class UserWorkflow : SequentialWorkflowActivity
    {
        public UserWorkflow()
        {
            InitializeComponent();
        }

        private string name;
        private int role;

        private void LogIn()
        {
        }

        private void SetUserCookie()
        {
        }
    }
}
