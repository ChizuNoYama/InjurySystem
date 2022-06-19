using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;

namespace ExampleMod.Components
{
    internal class WoundedHeroDeveloper : PropertyOwnerF<PropertyObject>, IHeroDeveloper
    {
        internal WoundedHeroDeveloper(IHeroDeveloper wrappedDeveloper)
        {
            _originalDeveloper = wrappedDeveloper;
        }

        #region Properties
        private IHeroDeveloper _originalDeveloper { get; set; }
        internal bool FromInjuriesHealed { get; set; }

        public int UnspentFocusPoints 
        {
            get { return _originalDeveloper.UnspentFocusPoints; }
            set { _originalDeveloper.UnspentFocusPoints = value; }
            
        }
        public int UnspentAttributePoints 
        {
            get { return _originalDeveloper.UnspentAttributePoints; }
            set { _originalDeveloper.UnspentAttributePoints = value; }
        }

        public int TotalXp => _originalDeveloper.TotalXp;

        public Hero Hero => _originalDeveloper.Hero;


        #endregion Properties

        #region Methods

        public void AddSkillXp(SkillObject skill, float rawXp, bool isAffectedByFocusFactor = true, bool shouldNotify = true)
        {
            _originalDeveloper.AddSkillXp(skill, rawXp, isAffectedByFocusFactor, shouldNotify);
        }

        public void CheckInitialLevel()
        {
            _originalDeveloper.CheckInitialLevel();
        }

        public void ClearUnspentPoints()
        {
            _originalDeveloper.ClearUnspentPoints();
        }

        public void AddFocus(SkillObject skill, int changeAmount, bool checkUnspentFocusPoints = true)
        {
            _originalDeveloper.AddFocus(skill, changeAmount, checkUnspentFocusPoints);
        }

        public void RemoveFocus(SkillObject skill, int changeAmount)
        {
            _originalDeveloper.RemoveFocus(skill, changeAmount);
        }

        public void DeriveSkillsFromTraits(bool isByNaturalGrowth = false, CharacterObject? template = null)
        {
            _originalDeveloper.DeriveSkillsFromTraits(isByNaturalGrowth, template);
        }

        public void SetInitialSkillLevel(SkillObject skill, int newSkillValue)
        {
            _originalDeveloper.SetInitialSkillLevel(skill, newSkillValue);
        }

        public void InitializeSkillXp(SkillObject skill)
        {
            _originalDeveloper.InitializeSkillXp(skill);
        }

        public void ClearHero()
        {
            _originalDeveloper.ClearHero();
        }

        public void AddPerk(PerkObject perk)
        {
            _originalDeveloper.AddPerk(perk);
        }

        public float GetFocusFactor(SkillObject skill)
        {
            return _originalDeveloper.GetFocus(skill);
        }

        public void AddAttribute(CharacterAttribute attribute, int changeAmount, bool checkUnspentPoints = true)
        {
            if (FromInjuriesHealed)
            {
                // TODO: Figure out a way to track how when and where these penaties are being restored. Choose who wants be in charge of doing this.
                // 
            }
            else
            {

            }
            _originalDeveloper.AddAttribute(attribute, changeAmount, checkUnspentPoints);
        }

        public void RemoveAttribute(CharacterAttribute attrib, int changeAmount)
        {

            _originalDeveloper.RemoveAttribute(attrib, changeAmount);
        }

        public void ChangeSkillLevel(SkillObject skill, int changeAmount, bool shouldNotify = true)
        {
            if (changeAmount < 0)
            {
                this.AddSkillPenaly(skill, changeAmount);
            }
            else
            {
                _originalDeveloper.ChangeSkillLevel(skill, changeAmount, shouldNotify);
            }
        }

        private void AddSkillPenaly(SkillObject skill, int changeAmount)
        {
            int skillValue = this.Hero.GetSkillValue(skill);
            int num = skillValue + changeAmount;
        }

        public int GetFocus(SkillObject skill)
        {
            return _originalDeveloper.GetFocus(skill);
        }

        public bool CanAddFocusToSkill(SkillObject skill)
        {
            return _originalDeveloper.CanAddFocusToSkill(skill);
        }

        void IHeroDeveloper.AfterLoad()
        {
            _originalDeveloper.AfterLoad();
        }

        public int GetTotalSkillPoints()
        {
            return _originalDeveloper.GetTotalSkillPoints();
        }

        public int GetXpRequiredForLevel(int level)
        {
            return _originalDeveloper.GetXpRequiredForLevel(level);
        }

        public IReadOnlyList<PerkObject> GetOneAvailablePerkForEachPerkPair()
        {
            return _originalDeveloper.GetOneAvailablePerkForEachPerkPair();
        }

        public int GetRequiredFocusPointsToAddFocus(SkillObject skill)
        {
            return _originalDeveloper.GetRequiredFocusPointsToAddFocus(skill);
        }

        public int GetSkillXpProgress(SkillObject skill)
        {
            return _originalDeveloper.GetSkillXpProgress(skill);
        }

        public bool GetPerkValue(PerkObject perk)
        {
            return _originalDeveloper.GetPerkValue(perk);
        }

        public void SetInitialLevel(int i)
        {
            _originalDeveloper.SetInitialLevel(i);
        }

        #endregion Methods
    }
}
