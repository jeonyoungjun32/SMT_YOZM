<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessingInspection.aspx.cs" Inherits="MobileViewer.View.ProcessingInspection" %>
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
    <title>가공공정검사</title>
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

			var str = "";
			//if (txtDocumentNo.GetText() != "") {
			//	if (txtDocumentNo.GetText().indexOf("*") == -1) {
			//		str += " and DocumentNo = '" + txtDocumentNo.GetText() + "'";
			//	}
			//	else {
			//		str += " and DocumentNo like '" + txtDocumentNo.GetText() + "'";
			//	}
			//}

			//if (txtDocumentName.GetText() != "") {
			//	if (txtDocumentName.GetText().indexOf("*") == -1) {
			//		str += " and DocumentName = '" + txtDocumentName.GetText() + "'";
			//	}
			//	else {
			//		str += " and DocumentName like '" + txtDocumentName.GetText() + "'";
			//	}
			//}

			//if (cboDocument_Type.GetValue() != null && cboDocument_Type.GetValue() != "") {
			//	str += " and Document_Type = '" + cboDocument_Type.GetValue() + "'";
			//}

			//if (startDate_Stored.GetValue() != null) {
			//	str += " and Date_Stored >= '" + getDateString(startDate_Stored.GetValue()) + "'"
			//}
			//if (endDate_Stored.GetValue() != null) {
			//	str += " and Date_Stored <= '" + getDateString(endDate_Stored.GetValue()) + "'"
			//}

			//if (str == "") {
			//	$("#dvDialogBody").text("검색 조건을 입력하세요.");
			//	$("#DialogIconedDanger").modal('show');
			//	return;
			//}

			//if (chkCurrentRevision.GetValue() == "Y") {
			//	str += " and CurrentRevision = 'Y'";
			//}


			if (cboLine.GetValue() != null && cboLine.GetValue() != "") {
				txtLine.SetText(cboLine.GetValue());
			} else {
				txtLine.SetText("All");
            }


			if (cboProcess.GetValue() != null && cboProcess.GetValue() != "") {
				txtProcess.SetText(cboProcess.GetValue());
			} else {
				txtProcess.SetText("All");
			}

			txtStartDate.SetText(getDateString(startDate_Stored.GetValue()));
            txtEndDate.SetText(getDateString(endDate_Stored.GetValue()));
			docGrid.Refresh();
		}

		function btnInit_onClick() {
			cboLine.SetValue(null);
			cboProcess.SetValue(null);
			startDate_Stored.SetText("");
			endDate_Stored.SetText("");
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

        function OnCustomButtonClick(s, e) {
			if (e.buttonID != "btnAdd") return;
            var rowIndex = e.visibleIndex;
			s.GetRowValues(e.visibleIndex, "OrderNo;OrderSeq;SerialNo;SubSeq", OnCallbackRowValues);
            //alert("클릭된 행의 ID: " + Id[0]);

               // window.location.href = "DetailsPage.aspx?ID=" + id;  // 페이지 이동
        }
        function OnCallbackRowValues(values) {
            //alert("클릭된 행의 ID: " + values[0] + "/" + values[1] + "/" + values[2]);

            window.location.href = "ProcessingInspectionDetail.aspx?id=10711&pid=15"
                + "&orderno=" + values[0]
                + "&orderseq=" + values[1]
                + "&serialno=" + values[2]
                + "&subseq=" + values[3]
                ;  // 페이지 이동
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
    <form id="form1" runat="server">
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
			<div class="pageTitle">가공공정검사</div>
			<div class="right"></div>
		</div>
	   <!-- * App Header -->

		   <!-- App Capsule -->
        <div id="appCapsule">
            <div class="section full mt-1">
                <div class="section-title">조회조건</div>
                <div class="wide-block pt-05 pb-05">

                    <div class="form-group boxed">
                        <div class="row input-wrapper" style="display: flex; justify-content: space-between;">
                            <div class="col-6" style="flex: 1; padding-right: 10px;">
                                <dx:BootstrapComboBox ID="cboProcess" runat="server" ClientInstanceName="cboProcess" Width="100%" NullText="공정"></dx:BootstrapComboBox>
                            </div>
                            <div class="col-6" style="flex: 1; padding-left: 10px;">
                                <dx:BootstrapComboBox ID="cboLine" runat="server" ClientInstanceName="cboLine" Width="100%" NullText="라인"></dx:BootstrapComboBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group boxed">
                        <div class="row input-wrapper" style="display: flex; justify-content: space-between;">
                            <div class="col-6" style="flex: 1; padding-right: 10px;">
                                <dx:BootstrapDateEdit ID="startDate_Stored" runat="server" ClientInstanceName="startDate_Stored" Width="100%" NullText="생산일자"></dx:BootstrapDateEdit>
                            </div>
                            <div class="col-6" style="flex: 1; padding-left: 10px;">
                                <dx:BootstrapDateEdit ID="endDate_Stored" runat="server" ClientInstanceName="endDate_Stored" Width="100%" NullText="종료일자"></dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>

                    <div class="row">
      
                        <div class="col-6">
                            <button type="button" id="btnInit" class="btn btn-secondary btn-lg btn-block" onclick="btnInit_onClick();">
                                초기화</button>
                        </div>
                        <div class="col-6">
                            <button type="button" id="btnSearch" class="btn btn-primary btn-lg btn-block" onclick="btnSearch_onClick();">
                                조 회</button>
						</div>
					</div>



				</div>
            </div>
            <dx:BootstrapGridView ID="DocGrid" ClientInstanceName="docGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                KeyFieldName="DocumentKey" DataSourceID="SqlDataSource1">
                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                <SettingsBehavior AllowFocusedRow="True" />
                <SettingsPager Mode="ShowAllRecords" PageSize="1000" />
                <CssClasses Row="grid-nowrap-row; gridCol" Control="grid-borderless" HeaderRow="gridCol" />

                <ClientSideEvents RowClick="OnRowClick" />
                <ClientSideEvents CustomButtonClick ="OnCustomButtonClick" />
           
            </dx:BootstrapGridView> 
        </div>
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
                  SelectCommand="exec QC_GetPQCOrderProcessList @pCorID, @pStartDate, @pEndDate, @pProcCode, @pLineCode " ProviderName="<%$ ConnectionStrings:MESConnectionString.ProviderName %>">
              <SelectParameters>
                <asp:ControlParameter ControlID="txtCorpId" Name="pCorID" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtStartDate" Name="pStartDate" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtEndDate" Name="pEndDate" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtProcess" Name="pProcCode" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtLine" Name="pLineCode" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>

			<div id="divHidden" style="left: 200px; position: absolute; top: 100px; visibility: hidden;">
				<dx:BootstrapTextBox ID="txtCorpId" runat="server" ClientInstanceName="txtCorpId">
				</dx:BootstrapTextBox>

				<dx:BootstrapTextBox ID="txtProcess" runat="server" ClientInstanceName="txtProcess">
				</dx:BootstrapTextBox>
				<dx:BootstrapTextBox ID="txtLine" runat="server" ClientInstanceName="txtLine">
				</dx:BootstrapTextBox>
				<dx:BootstrapTextBox ID="txtStartDate" runat="server" ClientInstanceName="txtStartDate">
				</dx:BootstrapTextBox>

				<dx:BootstrapTextBox ID="txtEndDate" runat="server" ClientInstanceName="txtEndDate">
				</dx:BootstrapTextBox>

				<button id="btnModalOpen" type="button" class="btn btn-secondary" data-bs-toggle="modal"
					data-bs-target="#ModalForm">
				</button>
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
