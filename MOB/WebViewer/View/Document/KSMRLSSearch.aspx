<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KSMRLSSearch.aspx.cs" Inherits="WebViewer.View.Document.KSMRLSSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge; chrome=1" />
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

        .card-body {
            padding: .75rem;
        }

        .form-group-row {
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            margin-bottom: .2rem;
        }

        .col-form-label {
            font-size: 12px;
            padding-left: 0px;
            padding-right: 0px;
        }

        .btn {
            line-height: 1;
        }

        .col-form-label {
            line-height: 1;
        }

        .dropdown-menu {
            font-size: 12px;
            left: -38px !important;
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
            if (typeof docGrid != 'undefined')
                docGrid.SetHeight(document.documentElement.clientHeight - 52);
        }

        function docGrid_EndCallback(s, e) {
            if (s.cpMsg) {
                alertify.alert(s.cpMsg);
                delete s.cpMsg;
            }
        }

        function docGrid_Init(s, e) {
            callResize();
        }

        function onToolbarItemClick(s, e) {
            if (e.item.name == "CustomItemInit") {
                txtDocumentKeyWord.SetText("");
                txtDocumentNo.SetText("");
                txtSheetNumber.SetText("");
                cboActualSize.SetValue("");
                cboUOM.SetValue("");
                txtOwner.SetText("");
                cboDocument_Type.SetValue("");
                txtDocumentRev.SetText("");
                chkCurrentRevision.SetValue("Y");
                startDate_Stored.SetText("");
                endDate_Stored.SetText("");
                startDate_Last_Chgd.SetText("");
                endDate_Last_Chgd.SetText("");   

                txtWhere.SetText("");
            }
            else if (e.item.name == "CustomItemSearch") {
                var str = "";
                if (txtDocumentKeyWord.GetText() != "") {
                    if (txtDocumentKeyWord.GetText().indexOf("*") == -1) {
                        str += " and documentkey IN (select documentkeyword.documentkey from documentkeyword Where documentkeyword.ProjectID = 7 And (KeyWordData = '" + txtDocumentKeyWord.GetText() + "'))";
                    }
                    else {
                        str += " and documentkey IN (select documentkeyword.documentkey from documentkeyword Where documentkeyword.ProjectID = 7 And (KeyWordData like '" + txtDocumentKeyWord.GetText() + "'))";
                    }
                }

                if (txtDocumentNo.GetText() != "") {
                    if (txtDocumentNo.GetText().indexOf("*") == -1) {
                        str += " and DocumentNo = '" + txtDocumentNo.GetText() + "'";
                    }
                    else {
                        str += " and DocumentNo like '" + txtDocumentNo.GetText() + "'";
                    }
                }

                if (txtSheetNumber.GetText() != "") {
                    if (txtSheetNumber.GetText().indexOf("*") == -1) {
                        str += " and SheetNumber = '" + txtSheetNumber.GetText() + "'";
                    }
                    else {
                        str += " and SheetNumber like '" + txtSheetNumber.GetText() + "'";
                    }
                }

                if (cboActualSize.GetValue() != null && cboActualSize.GetValue() != "") {
                    str += " and ActualSize = '" + cboActualSize.GetValue() + "'";
                }

                if (cboUOM.GetValue() != null && cboUOM.GetValue() != "") {
                    str += " and UOM = '" + cboUOM.GetValue() + "'";
                }

                if (txtOwner.GetText() != "") {
                    if (txtOwner.GetText().indexOf("*") == -1) {
                        str += " and Owner = '" + txtOwner.GetText() + "'";
                    }
                    else {
                        str += " and Owner like '" + txtOwner.GetText() + "'";
                    }
                }

                if (cboDocument_Type.GetValue() != null && cboDocument_Type.GetValue() != "") {
                    str += " and Document_Type = '" + cboDocument_Type.GetValue() + "'";
                }

                if (txtDocumentRev.GetText() != "") {
                    if (txtDocumentRev.GetText().indexOf("*") == -1) {
                        str += " and DocumentRev = '" + txtDocumentRev.GetText() + "'";
                    }
                    else {
                        str += " and DocumentRev like '" + txtDocumentRev.GetText() + "'";
                    }
                }

                if (startDate_Stored.GetValue() != null) {
                    str += " and Date_Stored >= '" + getDateString(startDate_Stored.GetValue()) + "'"
                }
                if (endDate_Stored.GetValue() != null) {
                    str += " and Date_Stored <= '" + getDateString(endDate_Stored.GetValue()) + "'"
                }

                if (startDate_Last_Chgd.GetValue() != null) {
                    str += " and Date_Last_Chgd >= '" + getDateString(startDate_Last_Chgd.GetValue()) + "'"
                }
                if (endDate_Last_Chgd.GetValue() != null) {
                    str += " and Date_Last_Chgd <= '" + getDateString(endDate_Last_Chgd.GetValue()) + "'"
                }

                if (str == "") {
                    alertify.alert("검색 조건을 입력하세요.");
                    return;
                }

                if (chkCurrentRevision.GetValue() == "Y") {
                    str += " and CurrentRevision = 'Y'";
                }

                txtWhere.SetText(str);
                docGrid.Refresh();
            }
            else if (e.item.name == "CustomItemImageView") {
                if (docGrid.IsDataRow(docGrid.GetFocusedRowIndex())) {
                    parent.frames["ifraMain"].location.href = "DocumentViewer.aspx?id=" + menuId
                        + "&pid=" + prjId
                        + "&table=" + txtTable.GetText()
                        + "&key=" + docGrid.GetRowKey(docGrid.GetFocusedRowIndex());
                    parent.$('#tablinkMain').click();
                }
            }
            else if (e.item.name == "CustomItemImageDownload") {
                if (docGrid.IsDataRow(docGrid.GetFocusedRowIndex())) {
                    docGrid.PerformCallback("download");
                }
            }
            else if (e.item.name == "CustomExportToXLSX") {
                e.processOnServer = true;
                e.usePostBack = true;
            }
        }

        function docGrid_RowDblClick(s, e) {

            if (docGrid.IsDataRow(docGrid.GetFocusedRowIndex())) {
                parent.frames["ifraMain"].location.href = "DocumentViewer.aspx?id=" + menuId
                    + "&pid=" + prjId
                    + "&table=" + txtTable.GetText()
                    + "&key=" + docGrid.GetRowKey(docGrid.GetFocusedRowIndex());
                parent.$('#tablinkMain').click();
            }
        }

        function getDateString(d) {
            var year = d.getFullYear();
            var month = (d.getMonth() + 1).toString().padStart(2, '0');
            var day = d.getDate().toString().padStart(2, '0');

            return `${year}${month}${day}`;
        }

    </script>
</head>
<body onresize="callResize();" style="padding-top: 4px;">
    <form id="form1" runat="server">
        <div style="position: absolute; left: 0px; right: 0px; top: 4px; bottom: 0px;">
            <div class="col-12">
                <!-- Left sidebar -->
                <div class="email-leftbar card">
                    <h4 class="header-title font-16 mt-2">검색 조건</h4>
                    <div class="overflow-auto" style="max-width: 310px; height: calc(100vh - 52px)">
                        <div class="form-group-row">
                            <label for="txtDocumentKeyWord" class="col-sm-4 col-form-label">Key Word Search</label>
                            <div class="col-sm-8">
                                <dx:BootstrapTextBox ID="txtDocumentKeyWord" runat="server" ClientInstanceName="txtDocumentKeyWord" Width="100%"></dx:BootstrapTextBox>
                            </div>
                        </div>
                        <div class="form-group-row">
                            <label for="txtDocumentNo" class="col-sm-4 col-form-label">문서번호</label>
                            <div class="col-sm-8">
                                <dx:BootstrapTextBox ID="txtDocumentNo" runat="server" ClientInstanceName="txtDocumentNo" Width="100%"></dx:BootstrapTextBox>
                            </div>
                        </div>
                        <div class="form-group-row">
                            <label for="txtSheetNumber" class="col-sm-4 col-form-label">쪽번호</label>
                            <div class="col-sm-8">
                                <dx:BootstrapTextBox ID="txtSheetNumber" runat="server" ClientInstanceName="txtSheetNumber" Width="100%"></dx:BootstrapTextBox>
                            </div>
                        </div>
                        <div class="form-group-row">
                            <label for="cboActualSize" class="col-sm-4 col-form-label">도면크기</label>
                            <div class="col-sm-8">
                                <dx:BootstrapComboBox ID="cboActualSize" runat="server" Width="100%" ClientInstanceName="cboActualSize"></dx:BootstrapComboBox>
                            </div>
                        </div>
                        <div class="form-group-row">
                            <label for="cboUOM" class="col-sm-4 col-form-label">단위</label>
                            <div class="col-sm-8">
                                <dx:BootstrapComboBox ID="cboUOM" runat="server" Width="100%" ClientInstanceName="cboUOM"></dx:BootstrapComboBox>
                            </div>
                        </div>
                        <div class="form-group-row">
                            <label for="txtOwner" class="col-sm-4 col-form-label">작성자</label>
                            <div class="col-sm-8">
                                <dx:BootstrapTextBox ID="txtOwner" runat="server" ClientInstanceName="txtOwner" Width="100%"></dx:BootstrapTextBox>
                            </div>
                        </div>
                        <div class="form-group-row">
                            <label for="cboDocument_Type" class="col-sm-4 col-form-label">도면종류</label>
                            <div class="col-sm-8">
                                <dx:BootstrapComboBox ID="cboDocument_Type" runat="server" Width="100%" ClientInstanceName="cboDocument_Type"></dx:BootstrapComboBox>
                            </div>
                        </div>
                        <div class="form-group-row">
                            <label for="txtDocumentRev" class="col-sm-4 col-form-label">Rev번호</label>
                            <div class="col-sm-8">
                                <dx:BootstrapTextBox ID="txtDocumentRev" runat="server" ClientInstanceName="txtDocumentRev" Width="100%"></dx:BootstrapTextBox>
                            </div>
                        </div>
                        <div class="form-group-row">
                            <label for="chkCurrentRevision" class="col-sm-4 col-form-label">최종버전</label>
                            <div class="col-sm-8">
                                <dx:BootstrapCheckBox ID="chkCurrentRevision" runat="server" ClientInstanceName="chkCurrentRevision" Width="100%" ValueChecked="Y" ValueType="System.String" ValueUnchecked="N"></dx:BootstrapCheckBox>
                            </div>
                        </div>
                        <div class="form-group-row">
                            <label for="startDate_Stored" class="col-sm-4 col-form-label">완료일</label>
                            <div class="col-sm-8">
                                <dx:BootstrapDateEdit ID="startDate_Stored" runat="server" ClientInstanceName="startDate_Stored" Width="100%"></dx:BootstrapDateEdit>
                            </div>
                        </div>
                        <div class="form-group-row">
                            <label for="endDate_Stored" class="col-sm-4 col-form-label"></label>
                            <div class="col-sm-8">
                                <dx:BootstrapDateEdit ID="endDate_Stored" runat="server" ClientInstanceName="endDate_Stored" Width="100%"></dx:BootstrapDateEdit>
                            </div>
                        </div>
                        <div class="form-group-row">
                            <label for="startDate_Last_Chgd" class="col-sm-4 col-form-label">최종보수일자</label>
                            <div class="col-sm-8">
                                <dx:BootstrapDateEdit ID="startDate_Last_Chgd" runat="server" ClientInstanceName="startDate_Last_Chgd" Width="100%"></dx:BootstrapDateEdit>
                            </div>
                        </div>
                        <div class="form-group-row">
                            <label for="endDate_Last_Chgd" class="col-sm-4 col-form-label"></label>
                            <div class="col-sm-8">
                                <dx:BootstrapDateEdit ID="endDate_Last_Chgd" runat="server" ClientInstanceName="endDate_Last_Chgd" Width="100%"></dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Left sidebar -->
                <!-- Right Sidebar -->
                <div class="email-rightbar mb-3 card">

                    <h4 class="header-title font-16 mt-2">검색 리스트</h4>
                    <dx:BootstrapGridView ID="DocGrid" ClientInstanceName="docGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                        KeyFieldName="DocumentKey" DataSourceID="SqlDataSource1" OnToolbarItemClick="DocGrid_ToolbarItemClick" OnCustomCallback="DocGrid_CustomCallback">
                        <Settings VerticalScrollableHeight="250" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" ShowGroupPanel="True" ShowFilterRow="True" />
                        <SettingsResizing ColumnResizeMode="Control" />
                        <Toolbars>
                            <dx:BootstrapGridViewToolbar>
                                <Items>
                                    <dx:BootstrapGridViewToolbarItem Text="검색조건 초기화" Name="CustomItemInit" BeginGroup="true" IconCssClass="mdi mdi-autorenew" >
                                        <SettingsBootstrap RenderOption="Secondary" />
                                    </dx:BootstrapGridViewToolbarItem>
                                    <dx:BootstrapGridViewToolbarItem Text="이미지 검색" Name="CustomItemSearch" BeginGroup="true" IconCssClass="mdi mdi-image-search-outline" >
                                        <SettingsBootstrap RenderOption="Primary" />
                                    </dx:BootstrapGridViewToolbarItem>
                                    <dx:BootstrapGridViewToolbarItem Text="이미지 보기" Name="CustomItemImageView" BeginGroup="true" Visible="False" IconCssClass="mdi mdi-image-outline" >
                                        <SettingsBootstrap RenderOption="Primary" />
                                    </dx:BootstrapGridViewToolbarItem>
                                    <dx:BootstrapGridViewToolbarItem Text="이미지 다운로드" Name="CustomItemImageDownload" BeginGroup="true" Visible="False" IconCssClass="mdi mdi-download" >
                                        <SettingsBootstrap RenderOption="Primary" />
                                    </dx:BootstrapGridViewToolbarItem>
                                    <dx:BootstrapGridViewToolbarItem Text="엑셀출력" Name="CustomExportToXLSX" BeginGroup="true" IconCssClass="far fa-file-excel" >
                                        <SettingsBootstrap RenderOption="Success" />
                                    </dx:BootstrapGridViewToolbarItem>
                                </Items>
                            </dx:BootstrapGridViewToolbar>
                        </Toolbars>
                        <SettingsText EmptyDataRow="검색된 항목이 없습니다." GroupPanel="그룹으로 표시할 컬럼을 드래그하여 넣으세요." />
                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                        <CssClasses Table="table table-striped table-bordered dt-responsive nowrap dataTable no-footer dtr-inline" />
                        <SettingsBehavior AllowClientEventsOnLoad="false" AllowFocusedRow="True" AllowSelectByRowClick="True" />
                        <SettingsPager PageSize="50" NumericButtonCount="6" Summary-Text="Page {0} of {1} ({2:#,0}건)">
                            <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                        </SettingsPager>                 
                        <GroupSummary>
                            <dx:ASPxSummaryItem FieldName="DocumentNo" SummaryType="Count" DisplayFormat="{0:#,0}건" />
                        </GroupSummary>
                        <ClientSideEvents EndCallback="docGrid_EndCallback" Init="docGrid_Init" ToolbarItemClick="onToolbarItemClick" RowDblClick="docGrid_RowDblClick" />
                    </dx:BootstrapGridView>
                </div>

            </div>

        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KSMImageConnectionString %>"
            SelectCommand="exec Proc_GetImageList @pTable, @pWhere">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtTable" Name="pTable" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtWhere" Name="pWhere" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
        <div id="divHidden" style="left: 200px; position: absolute; top: 100px; visibility: hidden;">
            <dx:BootstrapTextBox ID="txtTable" runat="server" ClientInstanceName="txtTable">
            </dx:BootstrapTextBox>
            <dx:BootstrapTextBox ID="txtWhere" runat="server" ClientInstanceName="txtWhere">
            </dx:BootstrapTextBox>
            <dx:BootstrapTextBox ID="txtViewAuthor" runat="server" ClientInstanceName="txtViewAuthor">
            </dx:BootstrapTextBox>
        </div>
    </form>

    <!-- jQuery  -->
    <script src="../assets/js/jquery.min.js"></script>
    <script src="../assets/js/bootstrap.bundle.min.js"></script>
    <script src="../assets/js/jquery.slimscroll.js"></script>
    <script src="../assets/js/waves.min.js"></script>

    <script type="text/javascript">

        var cmd = "<%=pstrCmd%>";
        var menuId = "<%=pstrMenuId%>";
        var prjId = "<%=pstrPrjId%>";
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
