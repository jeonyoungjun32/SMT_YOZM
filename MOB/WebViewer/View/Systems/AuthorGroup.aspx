<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthorGroup.aspx.cs" Inherits="WebViewer.View.Systems.AuthorGroup" %>

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

        function callResize() {
            groupGrid.SetHeight(document.documentElement.clientHeight - 68);
            detailGrid.SetHeight(document.documentElement.clientHeight - 68);
        }

        function groupGrid_EndCallback(s, e) {
            if (s.cpMsg) {
                alertify.alert(s.cpMsg);
                delete s.cpMsg;
            }
        }

        function groupGrid_Init(s, e) {
            callResize();
        }

        function groupGrid_FocusdRowChanged(s, e) {
            detailGrid.PerformCallback("FocusdRowChanged|" + s.GetRowKey(s.GetFocusedRowIndex()));
        }

        function detailGrid_EndCallback(s, e) {
            if (s.cpMsg) {
                alertify.alert(s.cpMsg);
                delete s.cpMsg;
            }
        }

        function detailGrid_Init(s, e) {
            detailGrid.PerformCallback("FocusdRowChanged|" + groupGrid.GetRowKey(groupGrid.GetFocusedRowIndex()));
            callResize();    
        }

        function onToolbarItemClick(s, e) {
            if (groupGrid.GetFocusedRowIndex() >= 0) {
                if (s.batchEditApi.HasChanges()) {
                    s.UpdateEdit();
                };
            }
        }

    </script>
</head>
<body onresize="callResize();" style="padding-top: 4px;">
    <form id="form1" runat="server">
        <div style="position: absolute; left: 0px; right: 0px; top: 4px; bottom: 0px;">
            <div class="row">
                <div class="col-lg-5">
                    <div class="card m-b-30">
                        <div class="card-body">
                            <h4 class="header-title font-16 mt-2">권한그룹</h4>
                            <dx:BootstrapGridView ID="GroupGrid" ClientInstanceName="groupGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                                KeyFieldName="GroupCode" DataSourceID="SqlDataSource1" OnRowInserting="GroupGrid_RowInserting">
                                <SettingsPopup>
                                    <EditForm Width="400" />
                                </SettingsPopup>
                                <Settings ShowFilterRowMenuLikeItem="true" ShowStatusBar="Visible" />
                                <SettingsText CommandCancel="취소" CommandDelete="삭제" CommandEdit="수정" CommandNew="신규" CommandSelect="선택" CommandUpdate="저장" ConfirmDelete="삭제하겠습니까?" EmptyDataRow="자료가 없습니다." PopupEditFormCaption="입력 팝업" />
                                <SettingsEditing EditFormColumnSpan="12" Mode="PopupEditForm"></SettingsEditing>
                                <SettingsDataSecurity AllowEdit="true" AllowDelete="true" AllowInsert="true" />
                                <Columns>
                                    <dx:BootstrapGridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true" ShowNewButtonInHeader="true" Width="120px" VisibleIndex="0">
                                        <CssClasses HeaderCell="text-center" />
                                    </dx:BootstrapGridViewCommandColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="GroupCode" Caption="그룹코드" VisibleIndex="1" ReadOnly="True">
                                        <CssClasses HeaderCell="text-center" />
                                        <SettingsEditForm ColumnSpan="12" />
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="GroupName" Caption="그룹명" VisibleIndex="2">
                                        <PropertiesTextEdit>
                                            <ValidationSettings>
                                                <RequiredField ErrorText="필수 입력 항목입니다." IsRequired="True" />
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <CssClasses HeaderCell="text-center" />
                                        <SettingsEditForm ColumnSpan="12" />
                                    </dx:BootstrapGridViewTextColumn>
                                </Columns>
                                <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                                <CssClasses Table="table table-striped table-bordered dt-responsive nowrap dataTable no-footer dtr-inline" />
                                <SettingsBehavior AllowFocusedRow="true" AllowClientEventsOnLoad="false" ConfirmDelete="True" />
                                <SettingsPager ShowEmptyDataRows="true" Mode="EndlessPaging" AlwaysShowPager="True">
                                    <Summary EmptyText="" />
                                </SettingsPager>
                                <ClientSideEvents EndCallback="groupGrid_EndCallback" Init="groupGrid_Init" FocusedRowChanged="groupGrid_FocusdRowChanged" />
                            </dx:BootstrapGridView>
                        </div>
                    </div>
                </div>
                <!-- end col -->
                <div class="col-lg-7">
                    <div class="card m-b-30">
                        <div class="card-body">
                            <h4 class="header-title font-16 mt-2">권한 목록</h4>
                            <dx:BootstrapGridView ID="DetailGrid" ClientInstanceName="detailGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                                KeyFieldName="GroupCode;MenuID;AuthorCode;" OnCustomCallback="DetailGrid_CustomCallback" OnBatchUpdate="DetailGrid_BatchUpdate">
                                <Settings ShowStatusBar="Hidden" />
                                <SettingsEditing Mode="Batch" >
                                    <BatchEditSettings ShowConfirmOnLosingChanges="False" AlwaysShowCheckboxesInCheckColumns="False" />
                                </SettingsEditing>
                                <SettingsDataSecurity AllowEdit="true" />
                                <Toolbars>
                                    <dx:BootstrapGridViewToolbar>
                                        <Items>
                                            <dx:BootstrapGridViewToolbarItem Text="저 장" Name="CustomItemUpdate" />
                                        </Items>
                                    </dx:BootstrapGridViewToolbar>
                                </Toolbars>
                                <SettingsText CommandCancel="취소" CommandDelete="삭제" CommandEdit="수정" CommandNew="신규" CommandSelect="선택" CommandUpdate="저장" ConfirmDelete="삭제하겠습니까?" EmptyDataRow="자료가 없습니다." PopupEditFormCaption="입력 팝업" />
                                <Columns>
                                    <dx:BootstrapGridViewTextColumn FieldName="GroupCode" Caption="그룹코드" VisibleIndex="0" Visible="False">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="MenuID" Caption="메뉴ID" VisibleIndex="1" Visible="False">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="AuthorCode" Caption="권한코드" VisibleIndex="2" Visible="False">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="MenuName" Caption="메뉴명" VisibleIndex="3">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="AuthorName" Caption="권한명" VisibleIndex="4" ReadOnly="True">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewCheckColumn FieldName="AuthorYn" Caption="권한여부" VisibleIndex="9" HorizontalAlign="Center">
                                        <PropertiesCheckEdit ValueChecked="Y" ValueType="System.String" ValueUnchecked="N">
                                        </PropertiesCheckEdit>
                                        <CssClasses HeaderCell="text-center" />
                                        <SettingsEditForm ColumnSpan="12" />
                                    </dx:BootstrapGridViewCheckColumn>
                                </Columns>
                                <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                                <CssClasses Table="table table-striped table-bordered dt-responsive nowrap dataTable no-footer dtr-inline" />
                                <SettingsBehavior AllowClientEventsOnLoad="false" ConfirmDelete="True" />
                                <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit"></SettingsAdaptivity>
                                <SettingsPager ShowEmptyDataRows="true" Mode="EndlessPaging" AlwaysShowPager="True" PageSize="1000">
                                    <Summary EmptyText="" />
                                </SettingsPager>
                                <ClientSideEvents EndCallback="detailGrid_EndCallback" Init="detailGrid_Init" ToolbarItemClick="onToolbarItemClick" />
                            </dx:BootstrapGridView>
                        </div>
                    </div>
                </div>
                <!-- end col -->
            </div>

        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KSMImageConnectionString %>"
            SelectCommand="SELECT * FROM CodeAuthorGroup Where CorpId = @pCorpId"
            UpdateCommand="UPDATE CodeAuthorGroup SET GroupName = @GroupName, ModId = @pEmpNo, ModDt = GETDATE()
                            WHERE CorpId = @pCorpId and GroupCode = @GroupCode"
            DeleteCommand="DELETE FROM CodeAuthorGroup WHERE CorpId = @pCorpId and GroupCode = @GroupCode"
            InsertCommand="INSERT INTO CodeAuthorGroup(CorpId, GroupCode, GroupName, CreId, CreDt) VALUES(
	                        @pCorpId, @GroupCode, @GroupName, @pEmpNo, GETDATE())">
            <SelectParameters>
                <asp:SessionParameter Name="pCorpId" SessionField="CorpId" />
            </SelectParameters>
            <DeleteParameters>
                <asp:SessionParameter Name="pCorpId" SessionField="CorpId" />
                <asp:Parameter Name="GroupCode" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="GroupCode" />
                <asp:Parameter Name="GroupName" />
                <asp:SessionParameter Name="pCorpId" SessionField="CorpId" />
                <asp:SessionParameter Name="pEmpNo" SessionField="EmpNo" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="GroupName" />
                <asp:SessionParameter Name="pCorpId" SessionField="CorpId" />
                <asp:SessionParameter Name="pEmpNo" SessionField="EmpNo" />
            </UpdateParameters>
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
