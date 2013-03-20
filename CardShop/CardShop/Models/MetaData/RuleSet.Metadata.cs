using System;
using System.ComponentModel.DataAnnotations;

namespace CardShop.Models
{
    [MetadataType(typeof(RuleSetMetaData))]
    public partial class RuleSet
    {

    }

    public class RuleSetMetaData
    {
        public int RuleSetId { get; set; }
        public string Name { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public string RuleSet1 { get; set; }
        public Nullable<short> Status { get; set; }
        public string AssemblyPath { get; set; }
        public string ActivityName { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
