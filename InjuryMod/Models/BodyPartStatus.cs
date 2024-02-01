namespace InjuryMod.Models
{
    internal class BodyPartStatus
    {
        public int TotalDamage { set; get; }

        public bool IsInjured => this.Severity != InjurySeverity.None;
        public InjurySeverity Severity { get; set; }
    }
}
