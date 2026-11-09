<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessingInspectionDetail.aspx.cs" Inherits="MobileViewer.View.ProcessingInspectionDetail" %>
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
    <title>공정검사 등록</title>
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
    </style>
    <script type="text/javascript">

		//function btnSearch_onClick() {

		//	if (cboCust.GetValue() != null && cboCust.GetValue() != "") {
		//		txtCustomer.SetText(cboDocument_Type.GetValue());
  //          } else {
		//		txtCustomer.SetText("All");
  //          }

		//	txtStartDate.SetText(getDateString(startDate_Stored.GetValue()));
		//	txtEndDate.SetText(getDateString(endDate_Stored.GetValue()));
		//	//txtWhere.SetText(str);
		//	docGrid.Refresh();
		//}

        function btnsearch_onClick() {
            //Detail부 조회
			if (cboInspMaster.GetValue() == null || cboInspMaster.GetValue() == "") {
                alert("검사구분을 선택해주세요.");
                return;
            }

			txtInspCode.SetText(cboInspMaster.GetValue().toString());
            docGrid.Refresh();
        }

<%--        function btnClientDeleteCliCk(s, e) {
            //var r = confirm(cboInspMaster.GetText() + " 검사내역을 삭제하시겠습니까?");
            var r = confirm("검사내역을 삭제하시겠습니까?");

            if (r == true) {
                txtDeleteYN.SetText("Y");
            } else {
                txtDeleteYN.SetText("N");
            }

            if (r == true) {
                var result = '*/<%# this.DeleteInsp()%>/*';

                if (result) {
                    alert("삭제가 완료되었습니다");
                }
                //sqlDataSource1.Delete();
                //this.DeleteInsp();
            }
        }
        --%>

        function btnSave_onClick() {

            //DocGrid.UpdateEdit();

        }


		

		function getDateString(d) {
			var year = d.getFullYear();
			var month = (d.getMonth() + 1).toString().padStart(2, '0');
			var day = d.getDate().toString().padStart(2, '0');

			return `${year}${month}${day}`;
		}

        function OnRowClick(s, e) {

            alert(e.visibleIndex);
            //DocGrid.StartEdit(e.visibleIndex);
			//var prevClickTime = s.lastClickTime;
			//s.lastClickTime = new Date();
			//if (prevClickTime && (s.lastClickTime - prevClickTime < 300)) {
			//	if (txtViewAuthor.GetText() != "Y") return;
			//	s.GetRowValues(e.visibleIndex, "DocumentKey;DocumentNo;SheetNumber;DocumentRev", onCallbackRowValue);
			//}
		}


        function OnCustomButtonClick(s, e) {
            var aa = e.buttonID;

            var rowVisibleIndex = e.visibleIndex;
            alert(aa, " + ", rowVisibleIndex);
			//s.GetRowValues(e.visibleIndex, 'ID', ShowPopup);
		}


		function onCallbackRowValue(values) {
			$("#dvModalTitle").text(values[1] + "/" + values[2] + "/" + values[3]);

			frames["ifraViewer"].location.href = "DocumentViewer.aspx?id=" + menuId
				+ "&pid=" + prjId
				+ "&table=" + txtTable.GetText()
				+ "&key=" + values[0];

			btnModalOpen.click();
        }

   //     function confirmDelete() {

			//if (cboInspMaster.GetValue() == null || cboInspMaster.GetValue() == "") {
			//	alert("검사구분을 선택해주세요.");
			//	return;
   //         }

			//return confirm("검사내역을 삭제하시겠습니까?");
   //     }

	</script>
</head>
<body>

    <!-- loader -->

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
        <div class="pageTitle">공정검사 등록</div>
        <div class="right"></div>
    </div>
    <!-- * App Header -->

        <!-- App Capsule -->
        <div id="appCapsule">
            <div class="section full mt-1">
                <div class="section-title">조회조건</div>
                <div class="wide-block pt-05 pb-05">

<%--                     <div class="form-group boxed">
                        <div class="row input-wrapper">
                        <div class="row input-wrapper">
                            <div class="col-6">
                                <dx:BootstrapDateEdit ID="startDate_Stored" runat="server" ClientInstanceName="startDate_Stored" Width="100%" NullText="시작일" ></dx:BootstrapDateEdit>
                            </div>
                            <div class="col-6">
                                <dx:BootstrapDateEdit ID="endDate_Stored" runat="server" ClientInstanceName="endDate_Stored" Width="100%" NullText="완료일"></dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>--%>

					<div class="form-group boxed" style="display:none;">
						<div class="row input-wrapper">
                            <div class="col-6">
								<div class="input-wrapper">
                                    <dx:ASPxLabel  ID="ASPxLabel1" runat="server" Text="시리얼번호"></dx:ASPxLabel>
									<%--<dx: ID="BootstrapComboBox2" runat="server" Width="100%" ClientInstanceName="cboCust" NullText="지시일자"></dx:>--%>
								</div>
							</div>

							<div class="col-6">
								<div class="input-wrapper">
									<dx:BootstrapTextBox ID="txtSerialNo" runat="server" Width="100%" ClientInstanceName="txtSerialNo" NullText="시리얼번호" Enabled="False" ReadOnly="True" ></dx:BootstrapTextBox>
								</div>
							</div>
							
						</div>
					</div>

                    	<div class="form-group boxed"  style="display:none;">
						<div class="row input-wrapper">
                            <div class="col-4">
								<div class="input-wrapper">
									<dx:BootstrapTextBox ID="txtProcess" runat="server" Width="100%" ClientInstanceName="txtProcess" NullText="공정" Enabled="False" ReadOnly="True" ></dx:BootstrapTextBox>
								</div>
							</div>

							<div class="col-4">
								<div class="input-wrapper">
									<dx:BootstrapTextBox ID="txtMaterial" runat="server" Width="100%" ClientInstanceName="txtLine" NullText="라인" Enabled="False" ReadOnly="True" ></dx:BootstrapTextBox>
								</div>
							</div>

                            	<div class="col-4">
								<div class="input-wrapper">
									<dx:BootstrapTextBox ID="txtMachine" runat="server" Width="100%" ClientInstanceName="txtMachine" NullText="설비" Enabled="False" ReadOnly="True" ></dx:BootstrapTextBox>
								</div>
							</div>
							
						</div>
					</div>

					<div class="form-group boxed">
						<div class="input-wrapper">
							<dx:BootstrapComboBox  ID="cboInspMaster" runat="server" Width="100%" ClientInstanceName="cboInspMaster" NullText="검사구분" EnableMultiColumn="True" 
                                 ValueField="InspCode" DataSourceID ="SqlDataSource2" OnSelectedIndexChanged="cboInspMaster_SelectedIndexChanged" >
								<Fields>
									<dx:BootstrapListBoxField FieldName="Title" SettingsMultiColumn-Caption="명칭" />
									<dx:BootstrapListBoxField FieldName="InspType" SettingsMultiColumn-Caption="검사코드" />
									<dx:BootstrapListBoxField FieldName="InspTypeName" SettingsMultiColumn-Caption="검사구분" />
								</Fields>
							</dx:BootstrapComboBox>
                        </div>
                    </div>

                    <div class="row">
                        <%--<div class="input-wrapper">--%>
                          <div class="col-6">
                            <button type="button" id="btnSearch" class="btn btn-primary btn-lg btn-block" onclick="btnsearch_onClick();">
                                조 회</button>
                        </div>
                          <div class="col-6">

                           <dx:ASPxButton ID="btnDelete" runat="server" Text="삭 제" CssClass="btn btn-primary btn-lg btn-block"
                                   ClientSideEvents-Click="function(s,e){ if(confirm('해당 검사내역을 삭제하시겠습니까?') == true) txtDeleteYN.SetText('Y'); return ; }" OnClick="btnDelete_Click"></dx:ASPxButton>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
            <dx:BootstrapGridView ID="DocGrid" ClientInstanceName="docGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                KeyFieldName="InspNo;InspSeq" DataSourceID="SqlDataSource1"
                OnRowUpdating ="Grid_RowUpdating" OnStartRowEditing="DocGrid_StartRowEditing"  OnEditFormLayoutCreated="DocGrid_EditFormLayoutCreated"
                >
                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                <SettingsBehavior AllowFocusedRow="True" />
                <SettingsPager Mode="ShowAllRecords" PageSize="1000" />
                <CssClasses Row="grid-nowrap-row; gridCol" Control="grid-borderless" HeaderRow="gridCol" />
                <SettingsEditing Mode="PopupEditForm">
				</SettingsEditing>
                <SettingsDataSecurity AllowEdit="True" />
                <%--<ClientSideEvents RowClick="OnRowClick" />--%>
                
                  <Columns>
                    <dx:BootstrapGridViewCommandColumn ShowEditButton="true" Width="1" MaxWidth="1" />
                    </Columns>
            </dx:BootstrapGridView>

        </div>
        <!-- * App Capsule -->
        <%--<dx:ASPxCallbackPanel ID="CbPnlReportInfo" runat="server" ClientInstanceName="cbPnlReportInfo">
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
        </dx:ASPxCallbackPanel>--%>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MESConnectionString %>"
            SelectCommand="exec QC_GetPQCWorkInspection @pCorID, @pSerialNo, @pInspCode" ProviderName="<%$ ConnectionStrings:MESConnectionString.ProviderName %>" 
            UpdateCommand="
UPDATE WorkInspection
SET DisplayConfirm = @DisplayConfirm
 , SizeConfirm = @SizeConfirm
 , Sample1	   = @Sample1
 , Sample2	   = @Sample2
 , Sample3	   = @Sample3
 , Sample4	   = @Sample4
 , Sample5	   = @Sample5
, Result = @Result
WHERE CorpId = @CorpId
AND SerialNo = @SerialNo
AND InspNo = @InspNo
AND InspSeq = @InspSeq
">
              <SelectParameters>
                <asp:ControlParameter ControlID="txtCorpId" Name="pCorID" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtSerialNo" Name="pSerialNo" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtInspCode" Name="pInspCode" PropertyName="Text" />
            </SelectParameters>
        	  <UpdateParameters>
				  <asp:ControlParameter ControlID="txtCorpId" Name="CorpId" PropertyName="Text" />
				  <asp:ControlParameter ControlID="txtSerialNo" Name="SerialNo" PropertyName="Text" />
				  <asp:Parameter Name="InspNo" />
				  <asp:Parameter Name="InspSeq" />
				  <asp:Parameter Name="DisplayConfirm" />
				  <asp:Parameter Name="SizeConfirm" />
				  <asp:Parameter Name="Sample1" />
				  <asp:Parameter Name="Sample2" />
				  <asp:Parameter Name="Sample3" />
				  <asp:Parameter Name="Sample4" />
				  <asp:Parameter Name="Sample5" />
			  	<asp:Parameter Name="Result" />
			  </UpdateParameters>
        </asp:SqlDataSource>

           <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MESConnectionString %>"
            SelectCommand="SELECT CM.InspCode
                                , CM.Title
                                , CM.InspType
                                , cci.Title InspTypeName
                                , CM.PublicPart
                                , CM.PublicMachine
                                , CM.PublicProcess
                        FROM CodeInspectionMaster CM
                        LEFT JOIN CodeInspectionProcess CIP on CM.InspCode = CIP.InspCode AND CIP.ProcessCode = @processCode
                        LEFT JOIN CodeInspectionMachine CIM on CM.InspCode = CIM.InspCode AND CIM.MachineCode = @machineCode
                        LEFT JOIN CodeInspectionPart CIP2 on CM.InspCode = CIP2.InspCode AND CIP2.PartCode = @materialCode
               LEFT JOIN CmCateItem cci ON CM.CorpID = cci.CorpID AND CM.InspType = cci.ItemCode AND cci.CLASS = 'Q010'
                        WHERE
                        (CM.PublicProcess = 'Y' or CIP.ProcessCode IS NOT NULL)
                        AND
                        (CM.PublicMachine = 'Y' or CIM.MachineCode IS NOT NULL)
                        AND
                        (CM.PublicPart = 'Y' or CIP2.PartCode IS NOT NULL)" ProviderName="<%$ ConnectionStrings:MESConnectionString.ProviderName %>">
              <SelectParameters>
            	  <asp:ControlParameter ControlID="txtProcess" Name="processCode" PropertyName="Text" />
				  <asp:ControlParameter ControlID="txtMachine" Name="machineCode" PropertyName="Text" />
				  <asp:ControlParameter ControlID="txtMaterial" Name="materialCode" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>

        <div id="divHidden" style="left: 200px; position: absolute; top: 100px; visibility: hidden;">
			<%--		<dx:BootstrapListBoxField FieldName="IsoAlpha3Code">
										<SettingsMultiColumn Caption="Code" ToolTip="IsoAlpha3Code" Width="50px" />
									</dx:BootstrapListBoxField>--%>

               <dx:BootstrapTextBox ID="txtCorpId" runat="server" ClientInstanceName="txtCorpId">
            </dx:BootstrapTextBox>

            
               <dx:BootstrapTextBox ID="txtInspCode" runat="server" ClientInstanceName="txtInspCode">
            </dx:BootstrapTextBox>
              <dx:BootstrapTextBox ID="txtOrderNo" runat="server" ClientInstanceName="txtOrderNo">
            </dx:BootstrapTextBox>
              <dx:BootstrapTextBox ID="txtOrderSeq" runat="server" ClientInstanceName="txtOrderSeq">
            </dx:BootstrapTextBox>
                <dx:BootstrapTextBox ID="txtSubSeq" runat="server" ClientInstanceName="txtSubSeq">
            </dx:BootstrapTextBox>
                     <dx:BootstrapTextBox ID="txtDeleteYN" runat="server" ClientInstanceName="txtDeleteYN">
            </dx:BootstrapTextBox>

            <button id="btnModalOpen" type="button" class="btn btn-secondary" data-bs-toggle="modal"
                data-bs-target="#ModalForm"></button>
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
