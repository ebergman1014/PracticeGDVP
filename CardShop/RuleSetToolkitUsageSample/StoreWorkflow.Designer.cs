using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Microsoft.Rules.Samples.RuleSetToolkitUsageSample
{
    partial class StoreWorkflow
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("", "")]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            this.policyFromService1 = new Microsoft.Samples.Rules.PolicyActivities.PolicyFromService();
            // 
            // policyFromService1
            // 
            this.policyFromService1.MajorVersion = 0;
            this.policyFromService1.MinorVersion = 0;
            this.policyFromService1.Name = "policyFromService1";
            this.policyFromService1.RuleSetName = null;
            // 
            // StoreWorkflow
            // 
            this.Activities.Add(this.policyFromService1);
            this.Name = "StoreWorkflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private Microsoft.Samples.Rules.PolicyActivities.PolicyFromService policyFromService1;
    }
}
