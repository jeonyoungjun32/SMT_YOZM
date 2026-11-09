<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="MobileViewer.Controls.Footer" %>
<div class="appBottomMenu">
		<a href="Main.aspx" class="item active">
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

				<%--         <li id="li11001" style="display:none">
                    <a href="Document/KSMEngSearch.aspx?id=11001&pid=1&table=fieldd0100" class="item">
                        <div class="icon-box bg-primary">
                            <ion-icon name="receipt-outline"></ion-icon>
                        </div>
                        <div class="in">
                            <div>KSM Eng</div>
                        </div>
                    </a>
                </li>
                <li id="li10706" style="display:none">
                    <a href="Document/KSMDocSearch.aspx?id=11002&pid=2&table=fieldd0200" class="item">
                        <div class="icon-box bg-primary">
                            <ion-icon name="receipt-outline"></ion-icon>
                        </div>
                        <div class="in">
                            <div>KSM Doc</div>
                        </div>
                    </a>
                </li>--%>
				<%--납품--%>
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
				<li id="li10707" style="display: none">
					<a href="Document/SalesView.aspx?id=10707&pid=4&table=fieldd0400" class="item">
						<div class="icon-box bg-primary">
							<ion-icon name="receipt-outline"></ion-icon>
						</div>
						<div class="in">
							<div>납품확인</div>
						</div>
					</a>
				</li>
				<li id="li10708" style="display: none">
					<a href="Document/Monitoring_SalesProgress.aspx?id=10708&pid=5&table=fieldd0500" class="item">
						<div class="icon-box bg-primary">
							<ion-icon name="receipt-outline"></ion-icon>
						</div>
						<div class="in">
							<div>납품진행상황모니터링</div>
						</div>
					</a>
				</li>
				<li id="li10709" style="display: none">
					<a href="Document/Monitoring_Operation.aspx?id=10709&pid=6&table=fieldd0600" class="item">
						<div class="icon-box bg-primary">
							<ion-icon name="receipt-outline"></ion-icon>
						</div>
						<div class="in">
							<div>가공 가동/비가동 모니터링</div>
						</div>
					</a>
				</li>
				<li id="li10710" style="display: none">
					<a href="Document/ProcessingInspection.aspx?id=10710&pid=7&table=fieldd0700" class="item">
						<div class="icon-box bg-primary">
							<ion-icon name="receipt-outline"></ion-icon>
						</div>
						<div class="in">
							<div>가공공정검사</div>
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