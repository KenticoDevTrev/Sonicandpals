using System;
using CMS;
using CMS.Core;
using CMS.Helpers;
using CMS.PortalEngine;
using CMS.UIControls;


[UIElement(ModuleName.DESIGN, "PageBuilderContainer.General")]
public partial class CMSModules_PageBuilderContainers_UI_PageBuilderContainers_Container_Edit_General : CMSModalDesignPage
{
    #region "Variables"

    private bool dialogMode;
    private bool tabMode;

    private PageBuilderContainerInfo PageBuilderContainer;

    #endregion


    #region "Methods"

    protected override void OnPreInit(EventArgs e)
    {
        RequireSite = false;

        dialogMode = (QueryHelper.GetBoolean("dialog", false) || QueryHelper.GetBoolean("isindialog", false));
        tabMode = QueryHelper.GetBoolean("tabmode", false);

        if (dialogMode)
        {
            if (!QueryHelper.ValidateHash("hash", "objectid"))
            {
                URLHelper.Redirect(AdministrationUrlHelper.GetErrorPageUrl("dialogs.badhashtitle", "dialogs.badhashtext"));
            }
        }
        else
        {
            CheckGlobalAdministrator();
        }

        var containerId = QueryHelper.GetInteger("containerid", 0);
        PageBuilderContainer = PageBuilderContainerInfoProvider.GetPageBuilderContainerInfo(containerId);

        if (PageBuilderContainer == null)
        {
            string containerName = QueryHelper.GetString("name", null);
            PageBuilderContainer = PageBuilderContainerInfoProvider.GetPageBuilderContainerInfo(containerName);
        }

        SetEditedObject(PageBuilderContainer, null);

        base.OnPreInit(e);
    }


    protected override void CreateChildControls()
    {
        if (PageBuilderContainer != null)
        {
            Guid instanceGuid = QueryHelper.GetGuid("instanceguid", Guid.Empty);

            UIContext.EditedObject = PageBuilderContainer;
            ucHierarchy.PreviewObjectName = PageBuilderContainer.ContainerName;
            ucHierarchy.ShowPanelSeparator = !dialogMode || (dialogMode && tabMode);
            ucHierarchy.IgnoreSessionValues = dialogMode;
            ucHierarchy.DialogMode = dialogMode;

            String containerName = PageBuilderContainer != null ? PageBuilderContainer.ContainerName : "";
            String parameter = instanceGuid != Guid.Empty ? "&previewguid=" + instanceGuid : "&previewobjectidentifier=" + containerName;
            ucHierarchy.PreviewURLSuffix = parameter;
        }

        base.CreateChildControls();
    }

    #endregion
}
