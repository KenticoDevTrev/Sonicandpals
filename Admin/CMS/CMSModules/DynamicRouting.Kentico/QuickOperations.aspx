<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSApp.CMSModules.DynamicRouting.QuickOperations" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">


    <h1 style="margin-bottom:10px; margin-top: 0; line-height:initial;">Url Slug Quick Operations</h1>
        <p>Here are some quick operations you can perform.</p>
    <hr />
    <p>
        <strong>Rebuild the Site</strong><br />
        This will rebuild all UrlSlugs on the current site, inserting/updating/deleting any that are incorrect.<br />
                <asp:Button runat="server" CssClass="btn btn-primary" ID="btnRebuildSite" Text="Rebuild Site" OnClick="btnRebuildSite_Click" />

    </p>
    <hr />
    <p>
        <strong>Rebuild the Tree (Node Alias Path)</strong><br />
        This will rebuild the Url Slugs for the given page and any descendents.
        <cms:FormControl runat="server" ID="tbxPath" FormControlName="selectpath" /> <asp:Button runat="server" ID="btnRebuildSubTree" CssClass="btn btn-primary" Text="Rebuild Sub Tree" OnClick="btnRebuildSubTree_Click" />
    </p>
    <hr />
    <p><strong>URL Checker</strong><br />
        Use this to check to see if the system can find a Page based on the given Url Slug.  <br />
        <em>Note that this just checks the URL Slug, it does not contain any special logic applied in the DynamicRoutingEvents.GetCulture nor DynamicRoutingEvents.GetPage.</em><br />
        <asp:TextBox runat="server" CssClass="form-control" ID="tbxRouteToTest" Width="400" />
        <asp:Button runat="server" ID="btnCheckUrl" CssClass="btn btn-primary" Text="Find Page" OnClick="btnCheckUrl_Click" />
        <asp:Literal runat="server" ID="ltrPageFound" /></p>
    <p>
        <strong>Total Clear</strong><br />
        Running this will erase all UrlSlugs, all queues and rebuild.  This should only be done on first install if your site had conflicts and caused errors in building the initial set of Url Slugs.<br />
                <asp:Button runat="server" CssClass="btn btn-primary" ID="btnCleanWipe" Text="Clear and Rebuild" OnClick="btnCleanWipe_Click" OnClientClick="return confirm('Are you sure you want to do this? All custom Urls will be lost.')" />

    </p>
    <hr />
</asp:Content>
