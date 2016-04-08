using SGAM.Elfec.Model.Enums;

namespace SGAM.Elfec.Model
{
    /// <summary>
    /// Describes a policy rule
    /// </summary>
    public class Rule
    {
        public string Id { get; set; }
        public RuleAction Action { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Exception { get; set; }
        public ApiStatus Status { get; set; }
    }
}
