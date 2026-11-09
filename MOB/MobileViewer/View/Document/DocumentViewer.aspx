<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentViewer.aspx.cs" Inherits="MobileViewer.View.Document.DocumentViewer" %>

<%@ Register assembly="DevExpress.XtraReports.v23.2.Web.WebForms, Version=23.2.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport"
        content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, viewport-fit=cover" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="theme-color" content="#000000" />
    <title>KSM Eng</title>
    <meta name="description" content="Sejong Mobile" />
    <meta name="keywords" content="bootstrap 5, mobile template, cordova, phonegap, mobile, html" />
    <link rel="icon" type="image/png" href="../assets/img/favicon.png" sizes="32x32" />
    <link rel="apple-touch-icon" sizes="180x180" href="../assets/img/icon/192x192.png" />
    <link href="../assets/css/inc/bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../assets/css/style.css" />

    <style>
        html, body {
            margin: 0;
            height: 100%;
            overflow: hidden;
        }

    </style>

    <script type="text/javascript">

        function customizeMenuAction(s, e) {
            e.Actions[0].visible = false;
            var exportTo = e.Actions.filter(function (x) { return x.imageTemplateName === 'dxrd-svg-preview-export-export-to' })[0];
            if (printAuthorYn != "Y") {
                exportTo.visible = false;
            }

        }

    </script>
</head>
<body>
    <form id="form1" runat="server" style="height:100%">
        <dx:ASPxWebDocumentViewer ID="WebDodumentViewer" runat="server" ClientInstanceName="webDodumentViewer" Height="100%" MobileMode="True">
            <SettingsExport ShowPrintNotificationDialog="False" />
            <ClientSideEvents CustomizeMenuActions="customizeMenuAction" />
        </dx:ASPxWebDocumentViewer>
        <dx:ASPxCallback ID="docCallback" runat="server" ClientInstanceName="docCallback" OnCallback="docCallback_Callback"></dx:ASPxCallback>
    </form>

    <!-- ============== Js Files ==============  -->
    <!-- Bootstrap -->
    <script src="../assets/js/lib/bootstrap.min.js"></script>
    <!-- Ionicons -->
    <script type="module" src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js"></script>
    <!-- Splide -->
    <script src="../assets/js/plugins/splide/splide.min.js"></script>
    <!-- ProgressBar js -->
    <script src="../assets/js/plugins/progressbar-js/progressbar.min.js"></script>


    <script type="text/javascript">
        var errMsg = "<%=pstrErrMsg%>";

        var printAuthorYn = "<%=pstrPrintAuthor%>";

        $(window).on('load', onLoad);

        function onLoad() {
            isLoad = true;
            if (errMsg != "") {
                alert(errMsg);
                errMsg = "";
            }
        }

    </script>


</body>
</html>
