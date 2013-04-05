//----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//----------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Rules.Samples.ExternalRuleSetToolkit;
using System.Workflow.Activities.Rules;

namespace Microsoft.Samples.Rules.ExternalRuleSetToolkit
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WorkflowHelper wfh = new WorkflowHelper();
            RuleSet rules = wfh.GetRules("GuestPassRuleSet");

            GuestPass gp1 = new GuestPass(1, 20, "2013-05-04");
            GuestPass gp2 = new GuestPass(2, 30, "2013-05-05");
            GuestPass gp3 = new GuestPass(3, 40, "2013-05-06");
            GuestPass gp4 = new GuestPass(4, 1, "");

            wfh.RunRules<GuestPass>(gp1, "GuestPassRuleSet");
            wfh.RunRules<GuestPass>(gp2, "GuestPassRuleSet");
            wfh.RunRules<GuestPass>(gp3, "GuestPassRuleSet");

            Console.WriteLine(gp1.ToString());
            Console.WriteLine(gp2.ToString());
            Console.WriteLine(gp3.ToString());


            Application.EnableVisualStyles();
            Application.Run(new RuleSetToolkitEditor());
        }
    }
}
