using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Validation;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Security;
using System.Text.RegularExpressions;

namespace AttpV2.Module;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class AttpV2Module : ModuleBase {
    public AttpV2Module() {
		// 
		// AttpV2Module
		// 
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.ModelDifference));
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.ModelDifferenceAspect));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.BaseObject));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.AuditDataItemPersistent));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.AuditedObjectWeakReference));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.FileData));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.FileAttachmentBase));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Security.SecurityModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.AuditTrail.AuditTrailModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.CloneObject.CloneObjectModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Dashboards.DashboardsModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Office.OfficeModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ReportsV2.ReportsModuleV2));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.StateMachine.StateMachineModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule));
    }
    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater };
    }
    public override void Setup(ApplicationModulesManager moduleManager)
    {
        base.Setup(moduleManager);
        ValidationRulesRegistrator.RegisterRule(moduleManager,
            typeof(PasswordStrengthCodeRule),
            typeof(IRuleBaseProperties));
    }
    public override void Setup(XafApplication application) {
        base.Setup(application);
        application.SetupComplete += application_SetupComplete;
        // Manage various aspects of the application UI and behavior at the module level.
    }
    void application_SetupComplete(object sender, EventArgs e)
    {
        ValidationModule module = ((XafApplication)sender).Modules.FindModule<ValidationModule>();
        if (module != null) 
            module.InitializeRuleSet();
    }
    public override void CustomizeTypesInfo(ITypesInfo typesInfo) {
        base.CustomizeTypesInfo(typesInfo);
        CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
    }
   
}
[CodeRule]
public class PasswordStrengthCodeRule : RuleBase<ChangePasswordOnLogonParameters>
{
    public PasswordStrengthCodeRule()
        : base("", "ChangePassword")
    {
        this.Properties.SkipNullOrEmptyValues = true;
    }
    public PasswordStrengthCodeRule(IRuleBaseProperties properties) : base(properties) { }
    protected override bool IsValidInternal(ChangePasswordOnLogonParameters target, out string errorMessageTemplate)
    {
        if (CalculatePasswordStrength(target.NewPassword) < 3)
        {
            errorMessageTemplate = "Độ mạnh của mật khẩu không đủ (phải có 8 ký tự: bao gồm chữ thường, chữ hoa và chữ số)";
            return false;
        }
        errorMessageTemplate = string.Empty;
        return true;
    }
    private int CalculatePasswordStrength(string pwd)
    {
        int weight = 0;
        if (pwd == null) 
            return weight;
        if (pwd.Length > 1 && pwd.Length < 4)
            ++weight;
        else
        {
            if (pwd.Length > 8)
                ++weight;
            Regex rxUpperCase = new Regex("[A-Z]");
            Regex rxLowerCase = new Regex("[a-z]");
            Regex rxNumerals = new Regex("[0-9]");
            Match match = rxUpperCase.Match(pwd);
            if (match.Success)
                ++weight;
            match = rxLowerCase.Match(pwd);
            if (match.Success)
                ++weight;
            match = rxNumerals.Match(pwd);
            if (match.Success)
                ++weight;
        }
        if (weight == 3 && pwd.Length < 6)
            --weight;
        if (weight == 4 && pwd.Length > 10)
            ++weight;
        return weight;
    }
}
