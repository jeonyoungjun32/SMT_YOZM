<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.ascx.cs" Inherits="MobileViewer.Controls.LeftMenu" %>

<!-- App Sidebar -->
<div class="offcanvas offcanvas-start" tabindex="-1" id="sidebarPanel">
    <div class="offcanvas-body">
        <!-- profile box -->
        <div class="profileBox">
            <div class="image-wrapper">
                <img src="../Images/sejongrmc_logo.png" alt="image" class="imaged rounded" />
            </div>
            <div class="in">
                <strong><span runat="server" id="txtEmpName"></span></strong>
                <div class="text-muted">
                    <ion-icon name="location"></ion-icon>
                    <span runat="server" id="txtCorpName"></span>
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
            <%-- 수주 --%>
            <li id="li10709" style="display: none">
                <a href="Document/BusinessOrder.aspx?id=10709&pid=9&table=fieldd0600" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="document-text-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>수주</div>
                    </div>
                </a>
            </li>
            
            <%-- 수주현황 --%>
            <li id="li10710" style="display: none">
                <a href="Document/BusinessOrderList.aspx?id=10710&pid=10&table=fieldd0600" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="list-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>수주현황</div>
                    </div>
                </a>
            </li>
            
            <%-- 출하현황 --%>
            <li id="li10715" style="display: none">
                <a href="Document/SalesView.aspx?id=10715&pid=15&table=fieldd0400" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="cube-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>출하현황</div>
                    </div>
                </a>
            </li>
            
            <%-- 자재입고현황 --%>
            <li id="li10720" style="display: none">
                <a href="Document/PurchaseView.aspx?id=10720&pid=20&table=fieldd0700" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="enter-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>자재입고현황</div>
                    </div>
                </a>
            </li>
            
            <%-- 자재출고현황 --%>
            <li id="li10725" style="display: none">
                <a href="Document/MaterialOutView.aspx?id=10725&pid=25&table=fieldd0600" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="exit-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>자재출고현황</div>
                    </div>
                </a>
            </li>
            
            <%-- 매출현황 --%>
            <li id="li10716" style="display: none">
                <a href="Document/SalesViewAmt.aspx?id=10716&pid=16&table=fieldd0700" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="trending-up-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>매출현황</div>
                    </div>
                </a>
            </li>
            
            <%-- 매입현황 --%>
            <li id="li10721" style="display: none">
                <a href="Document/PurchaseViewAmt.aspx?id=10721&pid=21&table=fieldd0700" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="trending-down-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>매입현황</div>
                    </div>
                </a>
            </li>
            
            <%-- 생산실적현황 --%>
            <li id="li10724" style="display: none">
                <a href="Document/OrderResultList.aspx?id=10724&pid=31&table=fieldd0700" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="bar-chart-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>생산실적현황</div>
                    </div>
                </a>
            </li>
            
            <%-- 생산진행 모니터링 --%>
            <li id="li10730" style="display: none">
                <a href="Document/Monitoring_ProductionProgress.aspx?id=10730&pid=30&table=fieldd0600" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="pulse-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>생산진행 모니터링</div>
                    </div>
                </a>
            </li>
            
            <%-- 공정 모니터링 --%>
            <li id="li10735" style="display: none">
                <a href="Document/ProcessingInspection.aspx?id=10735&pid=35&table=fieldd0700" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="settings-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>공정 모니터링</div>
                    </div>
                </a>
            </li>
            
            <%-- 포밍프레스공정모니터링 --%>
            <li id="li10740" style="display: none">
                <a href="Document/Monitoring_Operation_Press_JSG.aspx?id=10735&pid=35&table=fieldd0700" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="construct-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>포밍프레스공정모니터링</div>
                    </div>
                </a>
            </li>
            
            <%-- 기존 메뉴들 (숨김 처리) --%>
            <%-- 납품 --%>
            <li id="li10706" style="display: none">
                <a href="Document/OutRequest.aspx?id=10706&pid=3&table=fieldd0300" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="receipt-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>납품</div>
                    </div>
                </a>
            </li>
            
            <%-- 납품확인 --%>
            <li id="li10707" style="display: none">
                <a href="Document/SalesView.aspx?id=10707&pid=4&table=fieldd0400" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="checkmark-circle-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>납품확인</div>
                    </div>
                </a>
            </li>
            
            <%-- 납품진행상황모니터링 --%>
            <li id="li10708" style="display: none">
                <a href="Document/Monitoring_SalesProgress.aspx?id=10708&pid=5&table=fieldd0500" class="item">
                    <div class="icon-box bg-primary">
                        <ion-icon name="analytics-outline"></ion-icon>
                    </div>
                    <div class="in">
                        <div>납품진행상황모니터링</div>
                    </div>
                </a>
            </li>
        </ul>
    </div>
    
    <!-- sidebar buttons -->
    <div class="sidebar-buttons">
        <a href="Login.aspx" class="button">
            <ion-icon name="log-out-outline" title="로그아웃"></ion-icon>
        </a>
    </div>
    <!-- * sidebar buttons -->
</div>
<!-- * App Sidebar -->

<script type="text/javascript">
	var gsAuthor = "<%=pstrAuthor%>";
	$(window).on('load', function () {
		if (gsAuthor != "") {
			var arr = gsAuthor.split('|');
			for (var i = 0; i < arr.length; i++) {
				$('#li' + arr[i]).css('display', 'block');
			}
		}
	})
</script>