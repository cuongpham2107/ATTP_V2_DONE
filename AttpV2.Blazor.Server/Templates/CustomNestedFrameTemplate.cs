using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.Blazor.Templates.Toolbar.ActionControls;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace AttpV2.Blazor.Server.Templates
{
    public class CustomNestedFrameTemplate : FrameTemplate, ISupportActionsToolbarVisibility, ISelectionDependencyToolbar
    {
        public CustomNestedFrameTemplate()
        {
            IsActionsToolbarVisible = true;
            Toolbar = new DxToolbarAdapter(new DxToolbarModel());
            Toolbar.AddActionContainer(nameof(PredefinedCategory.ObjectsCreation));
            Toolbar.AddActionContainer("Link");
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Edit));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordEdit));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.View));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Reports));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Export));
            Toolbar.AddActionContainer("Diagnostic");
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Filters));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.FullTextSearch), DevExpress.Blazor.ToolbarItemAlignment.Right);
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Unspecified), DevExpress.Blazor.ToolbarItemAlignment.Right);
        }
        protected override IEnumerable<IActionControlContainer> GetActionControlContainers() => Toolbar.ActionContainers;
        protected override RenderFragment CreateComponent() => CustomNestedFrameTemplateComponent.Create(this);
        protected override void BeginUpdate()
        {
            base.BeginUpdate();
            ((ISupportUpdate)Toolbar).BeginUpdate();
        }
        protected override void EndUpdate()
        {
            ((ISupportUpdate)Toolbar).EndUpdate();
            base.EndUpdate();
        }
        public bool IsActionsToolbarVisible { get; private set; }
        public DxToolbarAdapter Toolbar { get; }
        void ISupportActionsToolbarVisibility.SetVisible(bool isVisible) => IsActionsToolbarVisible = isVisible;
    }
}
