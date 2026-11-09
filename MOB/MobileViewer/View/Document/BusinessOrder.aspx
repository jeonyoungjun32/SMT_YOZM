<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessOrder.aspx.cs" Inherits="MobileViewer.View.BusinessOrder" %>
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
    <title>수주등록</title>
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

		function btnSearch_onClick() {
            txtCust.SetText(cboCust.GetValue());
			txtStartDate.SetText(getDateString(startDate_Stored.GetValue()));
			txtEndDate.SetText(getDateString(endDate_Stored.GetValue()));
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
        <div class="pageTitle">수주등록</div>
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

                        <%--</div>--%>
                    </div>
                </div>
            </div>
            <dx:BootstrapGridView ID="DocGrid" ClientInstanceName="docGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                KeyFieldName="SdOrderNo;SdOrderSeq" DataSourceID="SqlDataSource1"
                OnRowUpdating ="Grid_RowUpdating" 
                OnRowDeleting ="DocGrid_RowDeleting"
                OnRowInserting="DocGrid_RowInserting" 
                OnInitNewRow="DocGrid_InitNewRow"
                OnCellEditorInitialize="grid_CellEditorInitialize"
                >
                <SettingsText ConfirmDelete="선택한 항목을 삭제하시겠습니까?" />

                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                <SettingsBehavior AllowFocusedRow="True" />
                <SettingsPager Mode="ShowAllRecords" PageSize="1000" />
                <CssClasses Row="grid-nowrap-row; gridCol" Control="grid-borderless" HeaderRow="gridCol" />
                <SettingsEditing Mode="PopupEditForm">
				</SettingsEditing>
            </dx:BootstrapGridView>

        </div>       

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MESConnectionString %>"
            SelectCommand="exec MB_GetSdOrderList @pCorpId, @pStartDate, @pEndDate, @pCust, 'ALL'" ProviderName="<%$ ConnectionStrings:MESConnectionString.ProviderName %>" 
            
            InsertCommand="

            DECLARE @UnitPrice float = 0

            IF @UnitPrice = 0
            BEGIN
                SET @UnitPrice = dbo.[FN_Get_ItemPrice](@CorpId, '20', @ItemCode, @SoldToCode, @ObtainDate)
            END

            INSERT INTO BM_SdOrderM(CorpId, SdOrderNo, SoldToCode, ObtainDate, DeliveryDate, Price, 
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

            INSERT INTO BM_SdOrderD(CorpId,SdOrderNo,SdOrderSeq,ItemCode,Qty,StockQty,
            UnitPrice,Price,VatPrice,TotalPrice,DeliveryDate,OrderStatus,PlacingStatus,PlacingOutStatus,Remark,VatType,VatRate,State,CreDt)

            VALUES(
            @CorpId,
            @SdOrderNo,
            1,
            @ItemCode,
            @Qty, 
            0,
            @UnitPrice,
            @Qty * @UnitPrice,
            (@Qty * @UnitPrice)/10,
            CAST((@Qty * @UnitPrice) * 1.1 AS INT),
            @DeliveryDate,
            '대기',
            '대기',
            '대기',
            @Remark,
            '10',
            '10',
            'N',
            GETDATE());
          " 

        UpdateCommand="
                    DECLARE @UnitPrice float = 0

                    IF @UnitPrice = 0
                    BEGIN
                        SET @UnitPrice = dbo.[FN_Get_ItemPrice](@CorpId, '20', @ItemCode, @SoldToCode, @ObtainDate)
                    END

                    UPDATE BM_SdOrderD 
                    SET ItemCode = @ItemCode, 
                            Price = @Price,
                            VatPrice = @VatPrice,
                            TotalPrice = @TotalPrice,
                            Qty = @Qty, 
                            Remark = @Remark
                    WHERE CorpId = @CorpId
                    AND SdOrderNo = @SdOrderNo
                    AND SdOrderSeq = @SdOrderSeq

                    UPDATE BM_SdOrderM
                    SET 
                        ObtainDate  = @ObtainDate, 
                        DeliveryDate= @DeliveryDate,
                        Price       = (SELECT SUM(Price)      FROM BM_SdOrderD 
                                        WHERE CorpId = @CorpId 
                                            AND SdOrderNo = @SdOrderNo),
                        VatPrice    = (SELECT SUM(VatPrice)   FROM BM_SdOrderD 
                                        WHERE CorpId = @CorpId 
                                            AND SdOrderNo = @SdOrderNo),
                        TotalPrice  = (SELECT SUM(TotalPrice) FROM BM_SdOrderD 
                                        WHERE CorpId = @CorpId 
                                            AND SdOrderNo = @SdOrderNo)
                    WHERE 
                        CorpId    = @CorpId
                        AND SdOrderNo= @SdOrderNo;
                    "
            
        DeleteCommand="
                    BEGIN
                        DELETE FROM BM_SdOrderD 
                        WHERE CorpId = @CorpId 
                          AND SdOrderNo = @SdOrderNo 
                          AND SdOrderSeq = @SdOrderSeq;

                        IF NOT EXISTS (
                            SELECT 1 
                            FROM BM_SdOrderD 
                            WHERE CorpId = @CorpId 
                              AND SdOrderNo = @SdOrderNo
                        )
                        BEGIN
                            DELETE FROM BM_SdOrderM 
                            WHERE CorpId = @CorpId 
                              AND SdOrderNo = @SdOrderNo;
                        END
                    END
                    ">
              <SelectParameters>
                <asp:ControlParameter ControlID="txtCorpId" Name="pCorpId" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtCust" Name="pCust" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtStartDate" Name="pStartDate" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtEndDate" Name="pEndDate" PropertyName="Text" />
            </SelectParameters>
               	  <InsertParameters>
				  <asp:ControlParameter ControlID="txtCorpId" Name="CorpId" PropertyName="Text" />
				  <asp:ControlParameter ControlID="txtSdOrderNo" Name="SdOrderNo" PropertyName="Text" />
				  <asp:ControlParameter ControlID="txtSdOrderSeq" Name="SdOrderSeq" PropertyName="Text" />
				  <asp:Parameter Name="SoldToCode" />
				  <asp:Parameter Name="ItemCode" />
				  <asp:Parameter Name="Price" />
				  <asp:Parameter Name="VatPrice" />
				  <asp:Parameter Name="TotalPrice" />
				  <asp:Parameter Name="Qty" />
				  <asp:Parameter Name="ObtainDate" />
				  <asp:Parameter Name="DeliveryDate" />
				  <asp:Parameter Name="HouseID" />
				  <asp:Parameter Name="Remark" />
			  </InsertParameters>
        	  <UpdateParameters>
				  <asp:ControlParameter ControlID="txtCorpId" Name="CorpId" PropertyName="Text" />
				  <asp:ControlParameter ControlID="txtSdOrderNo" Name="SdOrderNo" PropertyName="Text" />
				  <asp:ControlParameter ControlID="txtSdOrderSeq" Name="SdOrderSeq" PropertyName="Text" />
				  <asp:Parameter Name="NewYn" />
				  <asp:Parameter Name="SoldToCode" />
				  <asp:Parameter Name="ItemCode" />
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
