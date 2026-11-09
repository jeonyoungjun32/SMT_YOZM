<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateEmployee.aspx.cs" Inherits="WebViewer.View.Systems.CreateEmployee" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v23.2, Version=23.2.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <title></title>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../framestyle.css" rel="stylesheet" type="text/css" />

    <style>
        html, body {
            margin: 0;
            height: 100%;
            overflow: hidden;
        }

        .dx-data-row {
            user-select: text;
        }

        .dx-row-lines {
            user-select: text !important;
        }
    </style>
    <script type="text/javascript">

        function NotReload() {
            if ((event.ctrlKey == true && (event.keyCode == 78 || event.keyCode == 82)) || (event.keyCode == 116)) {
                event.keyCode = 0;
                event.cancelBubble = true;
                event.returnValue = false;
            }
        }
        document.onkeydown = NotReload;

        function onToolbarItemClick(s, e) {
            switch (e.item.name) {
                case "CustomExportToXLSX":
                    e.processOnServer = true;
                    e.usePostBack = true;
                    break;
            }
        }

        function chkPW(pw) {
            var num = pw.search(/[0-9]/g);
            var eng = pw.search(/[a-z]/ig);
            var spe = pw.search(/[`~!@@#$%^&*|₩₩₩'₩";:₩/?]/gi);

            if (pw.length < 8 || pw.length > 20) {
                alert("8자리 ~ 20자리 이내로 입력해주세요.");
                return false;
            } else if (pw.search(/\s/) != -1) {
                alert("비밀번호는 공백 없이 입력해주세요.");
                return false;
            } else if (num < 0 || eng < 0 || spe < 0) {
                alert("영문,숫자, 특수문자를 혼합하여 입력해주세요.");
                return false;
            } else {
                return true;
            }
        }

        function emplGrid_EndCallback(s, e) {
            if (s.cpMsg) {
                alert(s.cpMsg);
                delete s.cpMsg;
            }
        }

        function ClearSelection() {
            DeptTreeList.SetFocusedNodeKey("");
            UpdateControls(null, "");
        }
        function UpdateSelection() {
            var deptName = "";
            var focusedNodeKey = DeptTreeList.GetFocusedNodeKey();
            if (focusedNodeKey != "")
                deptName = DeptTreeList.cpDeptNames[focusedNodeKey];
            UpdateControls(focusedNodeKey, deptName);
        }
        function UpdateControls(key, text) {
            dropDownEdit.SetText(text);
            dropDownEdit.SetKeyValue(key);
            dropDownEdit.HideDropDown();
            UpdateButtons();
        }
        function UpdateButtons() {
            clearButton.SetEnabled(dropDownEdit.GetText() != "");
            selectButton.SetEnabled(DeptTreeList.GetFocusedNodeKey() != "");
        }
        function OnDropDown() {
            DeptTreeList.SetFocusedNodeKey(dropDownEdit.GetKeyValue());
            DeptTreeList.MakeNodeVisible(DeptTreeList.GetFocusedNodeKey());
        }

        function callResize() {
            emplGrid.SetHeight(document.documentElement.clientHeight - 40);
        }

        function emplGrid_Init(s, e) {
            callResize();
        }
    </script>
</head>
<body onresize="callResize();" style="padding-top: 4px;">
    <form id="form1" runat="server">
        <div style="position: absolute; left: 0px; right: 0px; top: 4px; bottom: 0px;">
            <h4 class="mt-2 header-title mb-2">사용자 리스트</h4>
            <dx:BootstrapGridView ID="EmplGrid" ClientInstanceName="emplGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                KeyFieldName="EmployeeId" DataSourceID="SqlDataSource1" OnToolbarItemClick="EmplGrid_ToolbarItemClick" OnCellEditorInitialize="EmplGrid_CellEditorInitialize" OnRowInserting="EmplGrid_RowInserting" OnRowInserted="EmplGrid_RowInserted" OnRowUpdating="EmplGrid_RowUpdating" OnCommandButtonInitialize="EmplGrid_CommandButtonInitialize">
                <SettingsPopup>
                    <EditForm Width="400" />
                </SettingsPopup>
                <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowFilterRowMenuLikeItem="true" ShowStatusBar="Visible" />
                <SettingsText CommandCancel="취소" CommandDelete="삭제" CommandEdit="수정" CommandNew="신규" CommandSelect="선택" CommandUpdate="저장" ConfirmDelete="삭제하겠습니까?" EmptyDataRow="자료가 없습니다." PopupEditFormCaption="입력 팝업" />
                <SettingsEditing EditFormColumnSpan="12" Mode="PopupEditForm"></SettingsEditing>
                <SettingsDataSecurity AllowEdit="true" AllowDelete="true" AllowInsert="true" />
                <Toolbars>
                    <dx:BootstrapGridViewToolbar>
                        <Items>
                            <dx:BootstrapGridViewToolbarItem Text="엑셀출력" Name="CustomExportToXLSX" />
                        </Items>
                    </dx:BootstrapGridViewToolbar>
                </Toolbars>
                <Columns>
                    <dx:BootstrapGridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true" ShowNewButtonInHeader="true" Width="120px" VisibleIndex="0">
                        <CssClasses HeaderCell="text-center" />
                    </dx:BootstrapGridViewCommandColumn>
                    <dx:BootstrapGridViewTextColumn FieldName="EmployeeId" Caption="사용자ID" VisibleIndex="1">
                        <PropertiesTextEdit>
                            <ValidationSettings>
                                <RequiredField ErrorText="필수 입력 항목입니다." IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <CssClasses HeaderCell="text-center" />
                        <SettingsEditForm ColumnSpan="12" />
                    </dx:BootstrapGridViewTextColumn>
                    <dx:BootstrapGridViewTextColumn FieldName="EmployeeName" Caption="사용자명" VisibleIndex="2">
                        <PropertiesTextEdit>
                            <ValidationSettings>
                                <RequiredField ErrorText="필수 입력 항목입니다." IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <CssClasses HeaderCell="text-center" />
                        <SettingsEditForm ColumnSpan="12" />
                    </dx:BootstrapGridViewTextColumn>
                    <dx:BootstrapGridViewTextColumn FieldName="Password2" Caption="비밀번호입력" VisibleIndex="3" Visible="False">
                        <CssClasses HeaderCell="text-center" />
                        <SettingsEditForm ColumnSpan="12" Visible="True" />
                    </dx:BootstrapGridViewTextColumn>
                    <dx:BootstrapGridViewComboBoxColumn Caption="부서" FieldName="DeptCode" VisibleIndex="4">
                        <PropertiesComboBox TextField="DeptName" ValueField="DeptCode" DataSourceID="deptDataSource">
                        </PropertiesComboBox>
                        <EditItemTemplate>
                            <dx:BootstrapDropDownEdit ID="dropDownEdit" runat="server" ClientInstanceName="dropDownEdit" OnInit="dropDownEdit_Init" ReadOnly="true">
                                <DropDownWindowTemplate>
                                    <div>
                                        <dx:ASPxTreeList ID="DeptTreeList" ClientInstanceName="DeptTreeList" runat="server" DataCacheMode="Disabled"
                                            Width="300px" DataSourceID="deptDataSource" OnCustomJSProperties="DeptTreeList_CustomJSProperties"
                                            KeyFieldName="DeptCode" ParentFieldName="ParentCode">
                                            <Settings VerticalScrollBarMode="Auto" ScrollableHeight="150" />
                                            <ClientSideEvents FocusedNodeChanged="function(s,e){ selectButton.SetEnabled(true); }" />
                                            <BorderBottom BorderStyle="Solid" />
                                            <SettingsBehavior AllowFocusedNode="true" AutoExpandAllNodes="true" FocusNodeOnLoad="false" />
                                            <SettingsPager Mode="ShowAllNodes">
                                            </SettingsPager>
                                            <Styles>
                                                <Node Cursor="pointer">
                                                </Node>
                                                <Indent Cursor="default">
                                                </Indent>
                                            </Styles>
                                            <Columns>
                                                <dx:TreeListTextColumn FieldName="DeptName" VisibleIndex="1">
                                                </dx:TreeListTextColumn>
                                            </Columns>
                                        </dx:ASPxTreeList>
                                    </div>
                                    <table class="dropDownEditTable">
                                        <tr>
                                            <td style="padding: 10px;">
                                                <dx:BootstrapButton ID="clearButton" ClientEnabled="false" ClientInstanceName="clearButton"
                                                    runat="server" AutoPostBack="false" Text="Clear">
                                                    <SettingsBootstrap RenderOption="Warning" />
                                                    <ClientSideEvents Click="ClearSelection" />
                                                </dx:BootstrapButton>
                                            </td>
                                            <td style="text-align: right; padding: 10px;">
                                                <dx:BootstrapButton ID="selectButton" ClientEnabled="false" ClientInstanceName="selectButton"
                                                    runat="server" AutoPostBack="false" Text="Select" ClientIDMode="Static">
                                                    <SettingsBootstrap RenderOption="Success" />
                                                    <ClientSideEvents Click="UpdateSelection" />
                                                </dx:BootstrapButton>
                                                <dx:BootstrapButton ID="closeButton" runat="server" AutoPostBack="false" Text="Close">
                                                    <SettingsBootstrap RenderOption="Dark" />
                                                    <ClientSideEvents Click="function(s,e) { dropDownEdit.HideDropDown(); }" />
                                                </dx:BootstrapButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                            </dx:BootstrapDropDownEdit>
                        </EditItemTemplate>
                    </dx:BootstrapGridViewComboBoxColumn>
                    <dx:BootstrapGridViewTextColumn FieldName="Phone" Caption="전화번호" VisibleIndex="5">
                        <CssClasses HeaderCell="text-center" />
                        <SettingsEditForm ColumnSpan="12" Visible="True" />
                    </dx:BootstrapGridViewTextColumn>
                    <dx:BootstrapGridViewTextColumn FieldName="CellPhone" Caption="휴대폰" VisibleIndex="6">
                        <CssClasses HeaderCell="text-center" />
                        <SettingsEditForm ColumnSpan="12" Visible="True" />
                    </dx:BootstrapGridViewTextColumn>
                    <dx:BootstrapGridViewTagBoxColumn FieldName="AuthorGroup" Caption="사용권한" VisibleIndex="7">
                        <PropertiesTagBox ValueField="GroupCode" TextField="GroupName">
                        </PropertiesTagBox>
                        <CssClasses HeaderCell="text-center" />
                        <SettingsEditForm ColumnSpan="12" Visible="True" />
                    </dx:BootstrapGridViewTagBoxColumn>
                    <dx:BootstrapGridViewBinaryImageColumn Caption="사진" FieldName="Photo" VisibleIndex="8">
                        <CssClasses HeaderCell="text-center" />
                        <SettingsEditForm ColumnSpan="12" Visible="True" />
                    </dx:BootstrapGridViewBinaryImageColumn>
                    <dx:BootstrapGridViewCheckColumn FieldName="UseYn" Caption="사용여부" VisibleIndex="9" HorizontalAlign="Center">
                        <PropertiesCheckEdit ValueChecked="Y" ValueType="System.String" ValueUnchecked="N">
                        </PropertiesCheckEdit>
                        <CssClasses HeaderCell="text-center" />
                        <SettingsEditForm ColumnSpan="12" />
                    </dx:BootstrapGridViewCheckColumn>
                </Columns>
                <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                <CssClasses Table="table table-striped table-bordered dt-responsive nowrap dataTable no-footer dtr-inline" />
                <SettingsBehavior AllowFocusedRow="true" AllowClientEventsOnLoad="false" ConfirmDelete="True" />
                <SettingsPager ShowEmptyDataRows="true" Mode="EndlessPaging" AlwaysShowPager="True">
                    <Summary EmptyText="" />
                </SettingsPager>
                <ClientSideEvents ToolbarItemClick="onToolbarItemClick" EndCallback="emplGrid_EndCallback" Init="emplGrid_Init" />
            </dx:BootstrapGridView>
        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KSMImageConnectionString %>"
            SelectCommand="SELECT *, '' Password2 FROM CodeUser Where CorpId = @pCorpId"
            UpdateCommand="UPDATE CodeUser SET EmployeeName = @EmployeeName
                                            , AuthorGroup = @AuthorGroup
                                            , DeptCode = @DeptCode
                                            , Phone = @Phone
                                            , CellPhone = @CellPhone
                                            , Photo = @Photo
                                            , UseYn = @UseYn
                                            , ModId = @pEmpNo
                                            , ModDt = GETDATE()
                            WHERE CorpId = @pCorpId and EmployeeId = @EmployeeId"
            DeleteCommand="DELETE FROM CodeUser WHERE CorpId = @pCorpId and EmployeeId = @EmployeeId"
            InsertCommand="INSERT INTO CodeUser(CorpId, EmployeeId, EmployeeName, Password, AuthorGroup, DeptCode, Phone, CellPhone, Photo, UseYn, CreId, CreDt) VALUES(
	                        @pCorpId, @EmployeeId, @EmployeeName, @Password, @AuthorGroup, @DeptCode, @Phone, @CellPhone, @Photo, @UseYn, @pEmpNo, GETDATE())">
            <SelectParameters>
                <asp:SessionParameter Name="pCorpId" SessionField="CorpId" />
            </SelectParameters>
            <DeleteParameters>
                <asp:SessionParameter Name="pCorpId" SessionField="CorpId" />
                <asp:Parameter Name="EmployeeId" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="EmployeeId" />
                <asp:Parameter Name="EmployeeName" />
                <asp:Parameter Name="Password" />
                <asp:Parameter Name="AuthorGroup" />
                <asp:Parameter Name="DeptCode" />
                <asp:Parameter Name="Phone" />
                <asp:Parameter Name="CellPhone" />
                <asp:Parameter Name="Photo" DbType="Binary" />
                <asp:Parameter Name="UseYn" />
                <asp:SessionParameter Name="pCorpId" SessionField="CorpId" />
                <asp:SessionParameter Name="pEmpNo" SessionField="EmpNo" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="EmployeeName" />
                <asp:Parameter Name="Password" />
                <asp:Parameter Name="AuthorGroup" />
                <asp:Parameter Name="DeptCode" />
                <asp:Parameter Name="Phone" />
                <asp:Parameter Name="CellPhone" />
                <asp:Parameter Name="Photo" DbType="Binary" />
                <asp:Parameter Name="UseYn" />
                <asp:SessionParameter Name="pCorpId" SessionField="CorpId" />
                <asp:SessionParameter Name="pEmpNo" SessionField="EmpNo" />
                <asp:Parameter Name="EmpNo" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="deptDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:KSMImageConnectionString %>"
            SelectCommand="Select DeptCode, ParentCode, DeptName From CodeDept Where CorpId = @pCorpId and UseYn = 'Y'"> 
            <SelectParameters>
                <asp:SessionParameter Name="pCorpId" SessionField="CorpId" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>

    <!-- jQuery  -->
    <script src="../assets/js/jquery.min.js"></script>
    <script src="../assets/js/bootstrap.bundle.min.js"></script>
    <script src="../assets/js/jquery.slimscroll.js"></script>
    <script src="../assets/js/waves.min.js"></script>

    <script type="text/javascript">

        var cmd = "<%=pstrCmd%>";
        var menuId = "<%=pstrMenuId%>";
        var errMsg = "<%=pstrErrMsg%>";
        var isLoad = false;

        $(window).on('load', onLoad);

        function onLoad() {
            isLoad = true;
            if (errMsg != "") {
                alertify.alert(errMsg);
                errMsg = "";
            }
        }

    </script>

    <!-- Alertify js -->
    <script src="../../plugins/alertify/js/alertify.js"></script>
</body>
</html>
