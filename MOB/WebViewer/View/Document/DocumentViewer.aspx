<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentViewer.aspx.cs" Inherits="WebViewer.View.Document.DocumentViewer" %>

<%@ Register assembly="DevExpress.XtraReports.v23.2.Web.WebForms, Version=23.2.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

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
<%--    <script type="text/html" id="filedownload">
        <svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"
	         viewBox="0 0 32 32" style="enable-background:new 0 0 32 32;" xml:space="preserve">
            <style type="text/css">
	            .Black{fill:#727272;}
	            .Green{fill:#039C23;}
            </style>
            <g id="Export">
	            <path class="Black" d="M10,12H6V6h4V12z M22,12v6v9c0,0.6-0.4,1-1,1H1c-0.6,0-1-0.4-1-1V7c0-0.6,0.4-1,1-1h3v8h14v-2H22z M18,18H4
		            v6h14V18z"/>
	            <polygon class="Green" points="16,10 24,10 24,14 32,8 24,2 24,6 16,6 	"/>
            </g>
        </svg>
    </script>--%>
    <script type="text/javascript">

        function NotReload() {
            if ((event.ctrlKey == true && (event.keyCode == 78 || event.keyCode == 82)) || (event.keyCode == 116)) {
                event.keyCode = 0;
                event.cancelBubble = true;
                event.returnValue = false;
            }
        }
        document.onkeydown = NotReload;

        function webDodumentViewer_DocumentReady(s, e) {
            if (webDodumentViewer.GetCurrentPageIndex() >= 0) {
                $('#pageInfo').text("이미지 정보 (Page " + (webDodumentViewer.GetCurrentPageIndex() + 1) + ")");
            }
        }

        function customizeMenuAction(s, e) {
            //if (downAuthorYn == "Y") {
            //    var selected = ko.observable(false);
            //    e.Actions.push({
            //        text: "파일다운로드",
            //        imageTemplateName: "filedownload",
            //        visible: true,
            //        disabled: false,
            //        selected: selected,
            //        clickAction: function () {
            //            alertify.alert("Click!");
            //        }
            //    });
            //}
            
            // ExportTo 숨기기
            //var exportTo = e.GetById(DevExpress.Reporting.Viewer.ActionId.ExportTo);
            //if (exportTo) exportTo.visible = false;
            // PrintPage 숨기기
            var printPage = e.GetById(DevExpress.Reporting.Viewer.ActionId.PrintPage);
            if (printPage) printPage.visible = false;

            if (printAuthorYn == "N") {
                // Print 숨기기
                var print = e.GetById(DevExpress.Reporting.Viewer.ActionId.Print);
                if (print) print.visible = false;

                var exportTo = e.GetById(DevExpress.Reporting.Viewer.ActionId.ExportTo);
                if (exportTo) exportTo.visible = false;
            }
            else {
                var print = e.GetById(DevExpress.Reporting.Viewer.ActionId.Print);
                var defaultPrintFunction1 = print.clickAction;  
                print.clickAction = function () {
                    docCallback.PerformCallback("Print");
                    defaultPrintFunction1();  
                };

                var exportToAction = e.Actions.filter(function (x) { return x.templateName === 'dxrd-preview-export-to' })[0];
                var exportToDefaultClickAction = exportToAction.clickAction;
                exportToAction.clickAction = function (arg) {
                    if (typeof arg.itemData.format != 'undefined') {
                        docCallback.PerformCallback("Print");
                    }
                    exportToDefaultClickAction(arg);
                } 
            }
        }

        function previewClick(s, e) {
            if (webDodumentViewer.GetCurrentPageIndex() >= 0) {
                $('#pageInfo').text("이미지 정보 (Page " + (webDodumentViewer.GetCurrentPageIndex() + 1) + ")");
                if (nowPageKey != arrPageKey[webDodumentViewer.GetCurrentPageIndex()]) {
                    nowPageKey = arrPageKey[webDodumentViewer.GetCurrentPageIndex()];
                    calllbackPnl.PerformCallback(nowPageKey);
                }
            }    
        }
    </script>
</head>
<body style="padding-top: 4px;" >
    <form id="form1" runat="server">
        <div style="position: absolute; left: 0px; right: 0px; top: 4px; bottom: 0px;">
            <div class="col-12">
                <!-- Left sidebar -->
                <div class="email-leftbar card">
                    <h4 id="pageInfo" class="header-title font-16 mt-2">이미지 정보</h4>
                    <div class="overflow-auto" style="max-width: 310px; height: calc(100vh - 52px)">
                        <dx:ASPxCallbackPanel ID="calllbackPnl" runat="server" Width="100%" ClientInstanceName="calllbackPnl" OnCallback="calllbackPnl_Callback">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <asp:Literal ID="ltlLeftInfo" Text="" runat="server" />
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>    
                    </div>
                </div>
                <!-- End Left sidebar -->
                <!-- Right Sidebar -->
                <div class="email-rightbar mb-3 card" style="height: calc(100vh - 7px)">
					<dx:ASPxWebDocumentViewer ID="WebDodumentViewer" runat="server" ClientInstanceName="webDodumentViewer" Height="100%">
						<SettingsExport ShowPrintNotificationDialog="False" />
						<ClientSideEvents CustomizeMenuActions ="customizeMenuAction" PreviewClick="previewClick" DocumentReady="webDodumentViewer_DocumentReady"/>
					</dx:ASPxWebDocumentViewer>
					<dx:ASPxCallback ID="docCallback" runat="server" ClientInstanceName="docCallback" OnCallback="docCallback_Callback"></dx:ASPxCallback>
					<div id="divHidden" style="left: 200px; position: absolute; top: 100px; visibility: hidden;">
						<dx:BootstrapTextBox ID="txtFileInfo" runat="server" ClientInstanceName="txtFileInfo">
						</dx:BootstrapTextBox>
					</div>
                </div>

            </div>

        </div>
    </form>

    <!-- jQuery  -->
    <script src="../assets/js/bootstrap.bundle.min.js"></script>
    <script src="../assets/js/jquery.slimscroll.js"></script>
    <script src="../assets/js/waves.min.js"></script>

    <script type="text/javascript">
        var errMsg = "<%=pstrErrMsg%>";

        var printAuthorYn = "<%=pstrPrintAuthor%>";

        var pageKey = "<%=fileInfo%>";
        var arrPageKey = [];
        var nowPageKey = "";

        $(window).on('load', onLoad);

        function onLoad() {
            isLoad = true;
            if (errMsg != "") {
                alertify.alert(errMsg);
                errMsg = "";
            }

            arrPageKey = pageKey.split('|');
            if (arrPageKey.length > 0) {
                nowPageKey = arrPageKey[0];
            }
        }

    </script>

    <!-- Alertify js -->
    <script src="../../plugins/alertify/js/alertify.js"></script>
</body>
</html>
