<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Monitoring_SiloStock.aspx.cs" Inherits="MobileViewer.View.Monitoring_SiloStock" %>
<%@ Register Src="~/View/CommonCtrl/SideMenu.ascx" TagPrefix="uc" TagName="SideMenu" %> <%--사이드메뉴 설정--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport"
        content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, viewport-fit=cover" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="theme-color" content="#000000" />
    <title>사일로모니터링</title>
    <meta name="description" content="Sejong Mobile" />
    <meta name="keywords" content="bootstrap 5, mobile template, cordova, phonegap, mobile, html" />
    <link rel="icon" type="image/png" href="../assets/img/Slogo.png" sizes="32x32" />
    <link rel="apple-touch-icon" sizes="180x180" href="../assets/img/icon/192x192.png" />
    <link href="../assets/css/inc/bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../assets/css/style.css" />

    
    <style>
        .form-group.boxed {
            margin: 0;
            padding: 4px 0;
        }

        .form-control {
            font-size: .9rem;
        }

        .dropdown-menu {
            font-size: 14px;
        }

        .form-check {
            padding-top: 4px;
        }

        .fixed {
            width: 100px;
            padding-left: 0px;
            padding-right: 0px;
        }

        .col {
            padding-left: 8px;
            padding-right: 8px;
        }

        .btn {
            padding: 3px 8px;
        }

        .gridCol {
            text-align: center; 
        }
    </style>
    <script type="text/javascript">

		function btnSearch_onClick() {

            docGrid1.Refresh();
            Literal2.Refresh();
		}

		function btnInit_onClick() {
			//txtDocumentNo.SetText("");
			//txtDocumentName.SetText("");
			//cboDocument_Type.SetValue(null);
			//startDate_Stored.SetText("");
			//endDate_Stored.SetText("");
			//chkCurrentRevision.SetValue("Y");
			//txtWhere.SetText("");
			//docGrid1.Refresh();
		}

		function getDateString(d) {
			var year = d.getFullYear();
			var month = (d.getMonth() + 1).toString().padStart(2, '0');
			var day = d.getDate().toString().padStart(2, '0');

			return `${year}${month}${day}`;
		}

		function OnRowClick(s, e) {
			//var prevClickTime = s.lastClickTime;
			//s.lastClickTime = new Date();
			//if (prevClickTime && (s.lastClickTime - prevClickTime < 300)) {
			//	if (txtViewAuthor.GetText() != "Y") return;
			//	s.GetRowValues(e.visibleIndex, "DocumentKey;DocumentNo;SheetNumber;DocumentRev", onCallbackRowValue);
			//}
		}

		function onCallbackRowValue(values) {
			$("#dvModalTitle").text(values[1] + "/" + values[2] + "/" + values[3]);

			frames["ifraViewer"].location.href = "DocumentViewer.aspx?id=" + menuId
				+ "&pid=" + prjId
				+ "&table=" + txtTable.GetText()
				+ "&key=" + values[0];

			btnModalOpen.click();
        }

    </script>
</head>
<body>

    <!-- loader -->

    <form id="form1" runat="server">
        <div class="container-fluid py-4" style="min-height: 100vh;">
    <div id="loader">
        <div class="spinner-border text-primary" role="status"></div>
    </div>
    <!-- * loader -->

    <!-- App Header -->
    <div class="appHeader bg-primary text-light">
        <div class="left">
            <a href="#" class="headerButton goBack">
                <ion-icon name="chevron-back-outline"></ion-icon>
            </a>
        </div>
        <div class="pageTitle">사일로모니터링</div>
        <div class="right"></div>
    </div>

            


<div style="height: 50px; width: 100%;"></div> <%--공백--%>

		<div class="row mb-0">
			<div class="col-12">
				<div class="col-12 text-center mb-0">
					<%--<h4 class="fw-bold text-primary">
						<asp:Literal ID="Literal1" runat="server" Text="재고"></asp:Literal>
					</h4>
					<hr style="width: 50px; border: 2px solid #007bff; margin: 0 auto;" />--%>
				</div>
			</div>
		</div>

      <div class="row">
         <button type="button" id="btnSearch" class="btn btn-primary btn-lg btn-block" onclick="btnSearch_onClick();">조 회</button>
      </div>

    <div style="height: 10px; width: 100%;"></div> <%--공백--%>

		<div class="table-responsive">
			<dx:BootstrapGridView ID="docGrid1" ClientInstanceName="docGrid1" runat="server" Width="100%" AutoGenerateColumns="False"
				KeyFieldName="DocumentKey" DataSourceID="SqlDataSource1">
				<SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
				<SettingsBehavior AllowFocusedRow="True" />
				<SettingsPager Mode="ShowAllRecords" PageSize="1000" />
				<CssClasses Row="grid-nowrap-row; gridCol" Control="grid-borderless" HeaderRow="gridCol" />
				<ClientSideEvents RowClick="OnRowClick" />
				<FormatConditions>
					<%--<dx:GridViewFormatConditionColorScale FieldName="MachienStatus" Format="BlueWhiteRed" />--%>
					<%--<dx:GridViewFormatConditionIconSet FieldName="MachienStatus"  Format="Ratings5" />--%>
				</FormatConditions>
			</dx:BootstrapGridView>
        </div>


<div style="border-bottom: 2px solid #dee2e6; padding-bottom: 10px; margin-bottom: 20px;">
    </div>
		<div class="chart">

			<div class="row mb-0">

					<div class="col-12">
						<div class="col-12 text-center mb-0">
							<h4 class="fw-bold text-primary">
								<asp:Literal ID="Literal2" runat="server" Text="현재 재고량"></asp:Literal>
							</h4>
							<hr style="width: 20px; border: 2px solid #007bff; margin: 0 auto;" />
						</div>
					</div>

					<dx:BootstrapChart ID="BootstrapChart1" runat="server" DataSourceID="SqlDataSource3" EncodeHtml="True" LoadingIndicatorText="" Palette="Office">
						<ArgumentAxis AggregationInterval="1" ArgumentType="System.String" Position="Right">
						</ArgumentAxis>

						<SeriesCollection>
							<dx:BootstrapChartSeries ArgumentField="SiloName" Name="현재고" Type="Bar" ValueField="NowStock">
								<Label Visible="True" Position="Outside">
                                    <Format Formatter="function(v) { return v.toLocaleString(); }" />
                                </Label>
							</dx:BootstrapChartSeries>
							<dx:BootstrapChartSeries ArgumentField="SiloName" Name="여유재고" ValueField="RemainStock" Type="Line">
								<Label Visible="True" Position ="Outside">
									<Format Formatter="function(v) { return v.toLocaleString() + ''; }" />
								</Label>
							</dx:BootstrapChartSeries>
						</SeriesCollection>
                        <%-- Y축 라벨을 표시하는 핵심 태그 --%>
    <ValueAxisCollection>
        <dx:BootstrapChartValueAxis Visible="true">
            <Label Visible="true">
                <%-- 천 단위 콤마(,) 표시 --%>
                <Format Type="FixedPoint" Precision="0" />
            </Label>
        </dx:BootstrapChartValueAxis>
    </ValueAxisCollection>
						<SettingsCommonAxis> 
                            <Label Visible ="true"></Label>
						</SettingsCommonAxis>
					</dx:BootstrapChart>
					<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:MESConnectionString %>" SelectCommand="Mobile_GetSiloStockList" SelectCommandType="StoredProcedure">
						<SelectParameters>
							<asp:ControlParameter ControlID="txtCorpId" Name="pCorpId" PropertyName="Text" Type="String" />
						</SelectParameters>
					</asp:SqlDataSource>

				</div>
		</div>


           		<div style="height: 70px; width: 100%;"></div> <%--공백--%>



		<!-- * App Capsule -->
        <dx:ASPxCallbackPanel ID="CbPnlReportInfo" runat="server" ClientInstanceName="cbPnlReportInfo">
            <PanelCollection>
                <dx:PanelContent runat="server">
                    <!-- Modal Form -->
                    <div class="modal fade modalbox" id="ModalForm" data-bs-backdrop="static" tabindex="-1" role="dialog">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="appHeader bg-primary text-light">
                                    <div id="dvModalTitle" class="pageTitle">이미지 뷰어</div>
                                    <div class="right">
                                        <a href="#" class="headerButton" data-bs-dismiss="modal">
                                            <ion-icon name="close-circle-outline"></ion-icon>
                                        </a>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <iframe id="ifraViewer" name="ifraViewer" class="demo-frame loading" style="z-index: 101; width: 100%; height: 100%;" marginwidth="0"
                                        marginheight="0" src="" frameborder="0" scrolling="no"></iframe>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- * Modal Form -->
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MESConnectionString %>"
            SelectCommand="exec Mobile_GetSiloStockList @pCorpId " ProviderName="<%$ ConnectionStrings:MESConnectionString.ProviderName %>">
                  <SelectParameters>
                <asp:ControlParameter ControlID="txtCorpId" Name="pCorpId" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
<%--              <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MESConnectionString %>"
            SelectCommand="exec Mobile_GetSiloStockList @pCorpId " ProviderName="<%$ ConnectionStrings:MESConnectionString.ProviderName %>">
                  <SelectParameters>
                <asp:ControlParameter ControlID="txtCorpId" Name="pCorpId" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>--%>

        <div id="divHidden" style="left: 200px; position: absolute; top: 100px; visibility: hidden;">
          	<dx:BootstrapTextBox ID="txtCorpId" runat="server" ClientInstanceName="txtCorpId">
				</dx:BootstrapTextBox>
				<dx:BootstrapTextBox ID="txtStartDate" runat="server" ClientInstanceName="txtStartDate">
				</dx:BootstrapTextBox>
				<dx:BootstrapTextBox ID="txtEndDate" runat="server" ClientInstanceName="txtEndDate">
				</dx:BootstrapTextBox>
            		<dx:BootstrapTextBox ID="txtStartDate_2" runat="server" ClientInstanceName="txtStartDate_2">
				</dx:BootstrapTextBox>
              		<dx:BootstrapTextBox ID="txtEndDate_2" runat="server" ClientInstanceName="txtEndDate_2">
				</dx:BootstrapTextBox>
            	<dx:BootstrapTextBox ID="txtCust" runat="server" ClientInstanceName="txtCust">
				</dx:BootstrapTextBox>
				<dx:BootstrapTextBox ID="txtMaterial" runat="server" ClientInstanceName="txtMaterial">
				</dx:BootstrapTextBox>

            <button id="btnModalOpen" type="button" class="btn btn-secondary" data-bs-toggle="modal"
				<a href="ProcessingInspection.aspx">ProcessingInspection.aspx</a>
                data-bs-target="#ModalForm"></button>
        </div>
            </div>
    </form>


    <!-- App Bottom Menu -->
    <div class="appBottomMenu">
        <a href="../Main.aspx" class="item active">
            <div class="col">
                <ion-icon name="home-outline"></ion-icon>
            </div>
        </a>
        <a href="#sidebarPanel" class="item" data-bs-toggle="offcanvas">
            <div class="col">
                <ion-icon name="menu-outline"></ion-icon>
            </div>
        </a>
    </div>
    <!-- * App Bottom Menu -->
    <!-- App Sidebar -->
       <%--좌측/하단메뉴--%>
       <uc:SideMenu ID="SideMenu1" runat="server" />
    <!-- * App Sidebar -->
    <!-- DialogIconedDanger -->
    <div class="modal fade dialogbox" id="DialogIconedDanger" data-bs-backdrop="static" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-icon text-danger">
                    <ion-icon name="close-circle"></ion-icon>
                </div>
                <div class="modal-header">
                    <h5 class="modal-title">알림</h5>
                </div>
                <div id="dvDialogBody" class="modal-body">
                    
                </div>
                <div class="modal-footer">
                    <div class="btn-inline">
                        <a href="#" class="btn" data-bs-dismiss="modal">CLOSE</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- * DialogIconedDanger -->
    <!-- ============== Js Files ==============  -->
    <!-- Bootstrap -->
    <script src="../../Scripts/jquery.min.js"></script>
    <script src="../assets/js/lib/bootstrap.min.js"></script>
    <!-- Ionicons -->
    <script type="module" src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js"></script>
    <!-- Splide -->
    <script src="../assets/js/plugins/splide/splide.min.js"></script>
    <!-- ProgressBar js -->
    <script src="../assets/js/plugins/progressbar-js/progressbar.min.js"></script>
    <!-- Base Js File -->
    <script src="../assets/js/base.js"></script>

    <script type="text/javascript">
        var cmd = "<%=pstrCmd%>";
        var menuId = "<%=pstrMenuId%>";

        var gsAuthor = "<%=pstrAuthor%>";

        var prjId = "<%=pstrPrjId%>";
        var errMsg = "<%=pstrErrMsg%>";
        var isLoad = false;

		$(window).on('load', onLoad);

		function onLoad() {
			isLoad = true;
			if (errMsg != "") {
				alert(errMsg);
				errMsg = "";
			}

			if (gsAuthor != "") {
				var arr = gsAuthor.split('|');
				for (var i = 0; i < arr.length; i++) {
					$('#li' + arr[i]).css('display', 'block');
				}
			}
		}

    </script>

</body>
</html>
