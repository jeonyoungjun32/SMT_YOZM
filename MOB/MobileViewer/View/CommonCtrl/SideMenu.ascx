<%@ Control Language="C#" AutoEventWireup="true"
    CodeBehind="SideMenu.ascx.cs"
    Inherits="MobileViewer.View.CommonCtrl.SideMenu" %>

<div class="offcanvas offcanvas-start" tabindex="-1" id="sidebarPanel">
    <div class="offcanvas-body">
        <%--https://icons.getbootstrap.com/--%>
        <!-- profile box -->
        <div class="profileBox">
            <div class="image-wrapper">
                <img src="../../Images/slogo.ico" alt="image" class="imaged rounded" /> <%--업체로고 변경하는 부분--%>
            </div>

            <div class="in">
                <strong><span runat="server" id="txtEmpName"></span></strong>
                <div class="text-muted">
                    <ion-icon name="location"></ion-icon>
                    <span runat="server" id="txtCorpName"></span>
                </div>
            </div>

            <a href="#" class="close-sidebar-button" data-bs-dismiss="offcanvas">
                <ion-icon name="close"></ion-icon>
            </a>
        </div>

        <div class="listview-title mt-2 mb-1">
            <span>Menus</span>
        </div>

        <ul class="listview flush transparent no-line image-listview mt-2">
            <asp:Repeater ID="rptSideMenu" runat="server">
                <ItemTemplate>
                    <li id="li<%# Eval("ParamText") %>">
                       <a href='<%# ResolveUrl("~/View/") + Eval("Url") %>' class="item">
                            <div class="icon-box <%# Eval("CssClass") %>">
                                <i class='bi <%# Eval("WebImageKey") %> side-menu-icon'></i>
                            </div>
                            <div class="in">
                                <div><%# Eval("MenuName") %></div>
                            </div>
                        </a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>

    </div>

    <div class="sidebar-buttons">
        <a href="<%= ResolveUrl("~/View/Login.aspx") %>" class="button">
            <ion-icon name="log-out-outline" title="로그아웃"></ion-icon>
        </a>
    </div>
</div>