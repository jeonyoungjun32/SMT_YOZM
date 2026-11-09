<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessOrder2.aspx.cs" Inherits="MobileViewer.View.Document.BusinessOrder2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport"
        content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, viewport-fit=cover" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="theme-color" content="#000000" />
    <title>영업 수주 등록</title>
    <meta name="description" content="Bogam Mobile" />
    <meta name="keywords" content="bootstrap 5, mobile template, cordova, phonegap, mobile, html" />
    <link rel="icon" type="image/png" href="../assets/img/hosung_logo_32.png" sizes="32x32" />
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
  //          txtCust.SetText(cboCust.GetValue());
		//	txtStartDate.SetText(getDateString(startDate_Stored.GetValue()));
		//	txtEndDate.SetText(getDateString(endDate_Stored.GetValue()));
		//	docGrid.Refresh();
  //      }
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
        <div class="pageTitle">영업 수주 등록</div>
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
                                <dx:BootstrapComboBox ID="cboCategory" runat="server" ClientInstanceName="cboCategory" Width="100%" NullText="수주일자"></dx:BootstrapComboBox>
                            </div>
                            <div class="col-6" style="flex: 1; padding-left: 10px;">
                                <dx:BootstrapComboBox ID="cboCust" runat="server" ClientInstanceName="cboCust" Width="100%" NullText="거래처"></dx:BootstrapComboBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group boxed">
                        <div class="row input-wrapper" style="display: flex; justify-content: space-between;">
                            <div class="col-6" style="flex: 1; padding-right: 10px;">
                                <dx:BootstrapDateEdit ID="startDate_Stored" runat="server" ClientInstanceName="startDate_Stored" Width="100%" NullText="시작일자"></dx:BootstrapDateEdit>
                            </div>
                            <div class="col-6" style="flex: 1; padding-left: 10px;">
                                <dx:BootstrapDateEdit ID="endDate_Stored" runat="server" ClientInstanceName="endDate_Stored" Width="100%" NullText="종료일자"></dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <%--<div class="input-wrapper">--%>
                          <div class="col-6">
                                <dx:ASPxButton ID="btnSearch" runat="server" Text="조 회" 
                                            OnClick="btnSearch_Click" 
                                            CssClass="btn btn-primary btn-lg btn-block"></dx:ASPxButton>
                        </div>
                          <div class="col-6">

                           <dx:ASPxButton ID="btnInsert" runat="server" Text="등 록" CssClass="btn btn-primary btn-lg btn-block"
                                    OnClick="btnInsert_Click"></dx:ASPxButton>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
            <dx:BootstrapGridView ID="DocGrid" ClientInstanceName="docGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                KeyFieldName="SdOrderNo;SdOrderSeq" DataSourceID="SqlDataSource1" 
                OnCustomColumnDisplayText="DocGrid_CustomColumnDisplayText"
                OnRowUpdating ="Grid_RowUpdating" 
                OnStartRowEditing="DocGrid_StartRowEditing"
                OnCellEditorInitialize="grid_CellEditorInitialize"
                OnRowDeleting="DocGrid_RowDeleting">
                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                <SettingsBehavior AllowFocusedRow="True" />
                <SettingsPager Mode="ShowAllRecords" PageSize="1000" />
                <CssClasses Row="grid-nowrap-row; gridCol" Control="grid-borderless" HeaderRow="gridCol" />
                <SettingsEditing Mode="InLine">
				</SettingsEditing>
                <SettingsDataSecurity AllowEdit="True" AllowDelete="True"/>  
                  <Columns>
                    <dx:BootstrapGridViewCommandColumn ShowEditButton="true" ShowNewButton="true" ShowDeleteButton="true" Width="1" MaxWidth="1" />
                    </Columns>
            </dx:BootstrapGridView> 

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HSConnectionString %>"            
            SelectCommand="exec Mobile_GetSdOrderList @pCorpId, @pStartDate, @pEndDate, @pCust, 'ALL'" ProviderName="<%$ ConnectionStrings:HSConnectionString.ProviderName %>" 
            UpdateCommand="

IF @NewYn = 'N'
BEGIN

UPDATE SdOrderDetail 
SET MaterialCode = @MaterialCode, 
       Price = @Price,
       VatPrice = @VatPrice,
       TotalPrice = @TotalPrice,
       Qty = @Qty, 
       Remark = @Remark
WHERE CorpId = @CorpId
AND SdOrderNo = @SdOrderNo
AND SdOrderSeq = @SdOrderSeq

UPDATE SdOrderMaster
SET 
    ObtainDate  = @ObtainDate, 
    DeliveryDate= @DeliveryDate,
    Price       = (SELECT SUM(Price)      FROM SdOrderDetail 
                    WHERE CorpId = @CorpId 
                      AND SdOrderNo = @SdOrderNo),
    VatPrice    = (SELECT SUM(VatPrice)   FROM SdOrderDetail 
                    WHERE CorpId = @CorpId 
                      AND SdOrderNo = @SdOrderNo),
    TotalPrice  = (SELECT SUM(TotalPrice) FROM SdOrderDetail 
                    WHERE CorpId = @CorpId 
                      AND SdOrderNo = @SdOrderNo)
WHERE 
    CorpId    = @CorpId
  AND SdOrderNo= @SdOrderNo;
END


ELSE
BEGIN

DECLARE @UnitPrice int
DECLARE @UnitCode varchar(5)
SET @UnitPrice = dbo.FN_SdPrice(@CorpId, @MaterialCode, @SoldToCode)
SET @UnitCode = (SELECT UnitCode FROM CmMaterial WHERE MaterialCode = @MaterialCode);

INSERT INTO SdOrderMaster(CorpId, SdOrderNo, SoldToCode, ObtainDate, DeliveryDate, Price, 
VatPrice, TotalPrice, VatType, VatRate, OutState, Remark, HouseID, State, CreDt)
						
VALUES(
@CorpId,
@SdOrderNo,
@SoldToCode,
@ObtainDate,
@DeliveryDate,
@Qty * @UnitPrice,
(@Qty * @UnitPrice)/10,
CAST((@Qty * @UnitPrice) * 1.1 AS INT),
'10',
'10',
'대기',
@Remark,
@HouseID,
'대기',
GETDATE());

INSERT INTO SdOrderDetail(CorpId,SdOrderNo,SdOrderSeq,MaterialCode,UnitCode,Qty,StockQty,ReqQty,ProQty,OutQty,
UnitPrice,Price,VatPrice,TotalPrice,DeliveryDate,OrderStatus,PlacingStatus,PlacingOutStatus,
GoodsStatus,Remark,VatType,VatRate,State,GoodsYn,CreDt)

VALUES(
@CorpId,
@SdOrderNo,
1,
@MaterialCode,
@UnitCode,
@Qty, 
0,
0,
0,
0,
@UnitPrice,
@Qty * @UnitPrice,
(@Qty * @UnitPrice)/10,
CAST((@Qty * @UnitPrice) * 1.1 AS INT),
@DeliveryDate,
'대기',
'대기',
'대기',
'대기',
@Remark,
'10',
'10',
'대기',
'N',
GETDATE());

EXEC Manufacture_MakeSdOrderRoute @CorpId, @SdOrderNo, 1, 'admin';

END
"
            
    DeleteCommand="
BEGIN
    DELETE FROM SdOrderDetail 
    WHERE CorpId = @CorpId 
      AND SdOrderNo = @SdOrderNo 
      AND SdOrderSeq = @SdOrderSeq;

    IF NOT EXISTS (
        SELECT 1 
        FROM SdOrderDetail 
        WHERE CorpId = @CorpId 
          AND SdOrderNo = @SdOrderNo
    )
    BEGIN
        DELETE FROM SdOrderMaster 
        WHERE CorpId = @CorpId 
          AND SdOrderNo = @SdOrderNo;
    END
END
">
            
            
            
              <SelectParameters>
                <asp:ControlParameter ControlID="txtCorpId" Name="pCorpID" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtCust" Name="pCust" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtStartDate" Name="pStartDate" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtEndDate" Name="pEndDate" PropertyName="Text" />
            </SelectParameters>
        	  <UpdateParameters>
				  <asp:ControlParameter ControlID="txtCorpId" Name="CorpId" PropertyName="Text" />
				  <asp:ControlParameter ControlID="txtSdOrderNo" Name="SdOrderNo" PropertyName="Text" />
				  <asp:ControlParameter ControlID="txtSdOrderSeq" Name="SdOrderSeq" PropertyName="Text" />
				  <asp:Parameter Name="NewYn" />
				  <asp:Parameter Name="SoldToCode" />
				  <asp:Parameter Name="MaterialCode" />
				  <asp:Parameter Name="Price" />
				  <asp:Parameter Name="VatPrice" />
				  <asp:Parameter Name="TotalPrice" />
				  <asp:Parameter Name="Qty" />
				  <asp:Parameter Name="ObtainDate" />
				  <asp:Parameter Name="DeliveryDate" />
				  <asp:Parameter Name="HouseID" />
				  <asp:Parameter Name="Remark" />
			  </UpdateParameters>
           <DeleteParameters>
                <asp:ControlParameter ControlID="txtCorpId" Name="CorpId" PropertyName="Text" />
                <asp:Parameter Name="SdOrderNo" />
                <asp:Parameter Name="SdOrderSeq" />
          </DeleteParameters>
        </asp:SqlDataSource>
                
        <div id="divHidden" style="left: 200px; position: absolute; top: 100px; visibility: hidden;">

               <dx:BootstrapTextBox ID="txtCorpId" runat="server" ClientInstanceName="txtCorpId">
            </dx:BootstrapTextBox>            
               <dx:BootstrapTextBox ID="txtCust" runat="server" ClientInstanceName="txtCust">
            </dx:BootstrapTextBox>
              <dx:BootstrapTextBox ID="txtStartDate" runat="server" ClientInstanceName="txtStartDate">
            </dx:BootstrapTextBox>
              <dx:BootstrapTextBox ID="txtEndDate" runat="server" ClientInstanceName="txtEndDate">
            </dx:BootstrapTextBox>
                <dx:BootstrapTextBox ID="txtSdOrderNo" runat="server" ClientInstanceName="txtSdOrderNo">
            </dx:BootstrapTextBox>
                <dx:BootstrapTextBox ID="txtSdOrderSeq" runat="server" ClientInstanceName="txtSdOrderSeq">
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
    <div class="offcanvas offcanvas-start" tabindex="-1" id="sidebarPanel">
        <div class="offcanvas-body">
            <!-- profile box -->
            <div class="profileBox">
                <div class="image-wrapper">
                   <img src="../../Images/SHINPOONG_logo.png" alt="image" class="imaged rounded" />
                </div>
                <div class="in">
                    <strong><span runat ="server" id="txtEmpName"></span></strong>
                    <div class="text-muted">
                        <ion-icon name="location"></ion-icon>
                        <span runat ="server" id="txtCorpName"></span>
                    </div>
                </div>
                <a href="#" class="close-sidebar-button" data-bs-dismiss="offcanvas">
                    <ion-icon name="close"></ion-icon>
                </a>
            </div>
            <!-- * profile box -->
            <div class="listview-title mt-2 mb-1">
                <span>Menus</span>
            </div>
            <ul class="listview flush transparent no-line image-listview mt-2">
                    <li id="li11706" style="display:none">
                    <a href="OrderProgressStatus.aspx?id=11706&pid=3&table=fieldd0300" class="item">
                        <div class="icon-box bg-primary">
                            <ion-icon name="receipt-outline"></ion-icon>
                        </div>
                        <div class="in">
                            <div>수주생산진행현황</div>
                        </div>
                    </a>
                </li>
                <li id="li11707" style="display:none">
                    <a href="FaultRateList.aspx?id=11707&pid=4&table=fieldd0400" class="item">
                        <div class="icon-box bg-primary">
                            <ion-icon name="receipt-outline"></ion-icon>
                        </div>
                        <div class="in">
                            <div>불량률현황</div>
                        </div>
                    </a>
                </li>
                <li id="li11708" style="display:none">
                    <a href="MachineOperationList.aspx?id=11708&pid=5&table=fieldd0500" class="item">
                        <div class="icon-box bg-primary">
                            <ion-icon name="receipt-outline"></ion-icon>
                        </div>
                        <div class="in">
                            <div>설비가동률현황</div>
                        </div>
                    </a>
                </li>
                <li id="li11709" style="display:none">
                    <a href="MachineOperationMonitoring.aspx?id=11709&pid=6&table=fieldd0600" class="item">
                        <div class="icon-box bg-primary">
                            <ion-icon name="receipt-outline"></ion-icon>
                        </div>
                        <div class="in">
                            <div>설비가동상태모니터링</div>
                        </div>s
                    </a>
                </li>
                <li id="li11710" style="display:none">
                    <a href="BusinessOrder.aspx?id=11710&pid=7&table=fieldd0700" class="item">
                        <div class="icon-box bg-primary">
                            <ion-icon name="receipt-outline"></ion-icon>
                        </div>
                        <div class="in">
                            <div>영업수주등록</div>
                        </div>
                    </a>
                </li>
                <li id="li11711" style="display:none">
                    <a href="ProcessingInspection.aspx?id=11711&pid=7&table=fieldd0700" class="item">
                        <div class="icon-box bg-primary">
                            <ion-icon name="receipt-outline"></ion-icon>
                        </div>
                        <div class="in">
                            <div>영업수주별진행현황</div>
                        </div>
                    </a>
                </li>
            </ul>
        </div>
        <!-- sidebar buttons -->
        <div class="sidebar-buttons">
            <a href="../Login.aspx" class="button">
                <ion-icon name="log-out-outline" title="로그아웃"></ion-icon>
            </a>
        </div>
        <!-- * sidebar buttons -->
    </div>
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
