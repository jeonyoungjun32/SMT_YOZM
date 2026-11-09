<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="MobileViewer.View.Main" %>
<%@ Register Src="~/View/CommonCtrl/SideMenu.ascx" TagPrefix="uc" TagName="SideMenu" %> <%--사이드메뉴 설정--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link rel="WebImageKey" type="image/png" href="assets/img/slogo.png" sizes="32x32" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport"
		content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, viewport-fit=cover" />
	<meta name="apple-mobile-web-app-capable" content="yes" />
	<meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
	<meta name="theme-color" content="#000000" />
	<title>SMT</title>
	<meta name="description" content="Sejong Mobile" />
	<meta name="keywords" content="bootstrap 5, mobile template, cordova, phonegap, mobile, html" />

	<link rel="apple-touch-icon" sizes="180x180" href="assets/img/icon/192x192.png" />
	<link rel="stylesheet" href="assets/css/style.css" />
	<link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200&icon_names=check_circle" />
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


		.top-navigation {
			background-color: #007BFF;
			padding: 10px;
			color: white;
		}

		.user-label {
			font-size: 16px;
			margin-right: 20px;
		}

		.theme-switch {
			margin-right: 20px;
		}

		.close-button {
			background-color: transparent;
			color: white;
			border: none;
			font-size: 18px;
		}

		.menu-container {
			display: flex;
			flex-wrap: wrap;
			justify-content: space-around;
			margin-top: 20px;
		}

		.menu-item {
			width: 150px;
			height: 150px;
			margin: 10px;
			text-align: center;
		}

		.menu-icon img {
			width: 50px;
			height: 50px;
			margin-bottom: 10px;
		}


		.container {
			margin: 0 auto;
			max-width: 100%;
			width: 1200px;
			overflow: hidden;
		}

		.inner {
			padding: 16px;
			float: left;
			width: calc(100% / 4);
			box-sizing: border-box;
		}

		.container .inner {
			padding: 16px;
			width: calc(100% / 4);
			box-sizing: border-box;
		}

		h2,
		p {
			margin: 0;
			text-align: center;
		}

		.card-item {
			width: 100%;
			background-color: #fff;
			border-radius: 8px;
			overflow: hidden;
			box-shadow: 2px 2px 8px rgba(0, 0, 0, 0.1);
		}

			.card-item:hover {
				background-color: lightgray;
				color: white !important;
			}

			.card-item a {
				text-decoration: none;
				color: #29363d;
				display: block;
			}

			.card-item img {
				width: 100%;
				aspect-ratio: 288/196;
				object-fit: cover;
				vertical-align: top;
			}

		.card-text {
			padding: 24px 16px 16px;
		}

			.card-text .title {
				font-size: 16px;
				white-space: nowrap;
				overflow: hidden;
				text-overflow: ellipsis;
				margin-bottom: 12px;
			}

				.card-text .title + p {
					overflow: hidden;
					display: -webkit-box;
					-webkit-box-orient: vertical;
					-webkit-line-clamp: 2;
					word-break: break-all;
					font-size: 12px;
					color: #5a6a72;
					line-height: 20px;
					margin-bottom: 16px;
				}

		.underline {
			text-decoration: underline;
			font-weight: bold;
			font-size: 14px;
			padding-left: 6px;
		}

		@media (max-width: 1000px) {
			.container {
				max-width: 900px;
			}

				.container .inner {
					width: calc(100% / 3);
				}
		}

		@media (max-width: 900px) {
			.container {
				max-width: 600px;
			}

				.container .inner {
					width: calc(100% / 2);
				}
		}

		@media (max-width: 767px) {
			.container {
				max-width: 300px;
			}

				.container .inner {
					width: 100%;
				}
		}

		.btn_mainmenu {
			height: 95px;
			border-radius: 12px;
			background: #fff !important;
			border: 1px solid #E5E7EB !important;
			color: #313645 !important;
			box-shadow: 0 2px 8px rgba(0,0,0,.08);

			display: flex;
			flex-direction: column;
			align-items: center;
			justify-content: center;
		}

		.btn_mainmenu:hover {
			color: white !important;
		}

		.btn_icon_mainmenu {
			font-size: 34px;
			width: 34px;
			height: 34px;
			margin-bottom: 12px;
		}

		.material-symbols-outlined {
			font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24
		}

		.card-menu{
			display:flex;
			align-items:center;
			width:100%;
			min-height:85px;

			background:#fff;
			border:1px solid #E5E7EB;
			border-radius:12px;

			padding:18px;

			text-decoration:none;
			color:#313645;

			box-shadow:0 2px 8px rgba(0,0,0,.08);
		}

		.card-menu-icon{
			width:27px;       
			text-align:center;
			margin-right:10px;
			flex-shrink:0;
		}

		.card-menu-icon i{
			font-size:26px;    
			color:#65748B;
		}

		.card-menu-text{
			flex:1;
		}

		.card-menu-text .title{
			font-size:20px;    
			font-weight:700;
			color:#313645;
			line-height:1.3;
		}

		.card-menu-text .desc{
			font-size:14px;
			color:#8A94A6;
			margin-top:4px;
		}

		.card-menu-arrow{
			font-size:18px;
			color:#9CA3AF;
		}

		.menu-col {
			width: 100%;
		}

		@media (min-width: 360px) {
			.menu-col {
				width: 50%;
			}
		}

	</style>

	<script>
		function btn1_onclick() {
			window.location.href = "Document/OutRequest.aspx?id=10706&pid=3&table=fieldd0300";
		}

	</script>
</head>
<body>

	<!-- loader -->
	<div id="loader">
		<div class="spinner-border text-primary" role="status"></div>
	</div>
	<!-- * loader -->

	<!-- App Header -->
	<div class="appHeader bg-primary scrolled">
		<div class="left">
			<a href="#" class="headerButton" data-bs-toggle="offcanvas" data-bs-target="#sidebarPanel">
				<ion-icon name="menu-outline"></ion-icon>
			</a>
		</div>
		<div class="pageTitle">
			KSM
		</div>
		<div class="right">
			<input class="form-check-input dark-mode-switch" type="checkbox" id="darkmodecontent" />
			<label class="form-check-label" for="darkmodecontent"></label>
		</div>

	</div>
	<!-- * App Header -->

	<!-- Search Component -->
	<div id="search" class="appHeader">
		<form class="search-form">
			<div class="form-group searchbox">
				<input type="MenuName" class="form-control" placeholder="Search..." />
				<i class="input-icon">
					<ion-icon name="search-outline"></ion-icon>
				</i>
				<a href="#" class="ms-1 close toggle-searchbox">
					<ion-icon name="close-circle"></ion-icon>
				</a>
			</div>
		</form>
	</div>
	<!-- * Search Component -->

	<form class="" runat="server">

		<!-- App Capsule -->
		<div id="appCapsule">

		<%--메인 메뉴 버튼--%>
		<div class="section mt-1 mb-1">
			<div class="row input-wrapper">
				<asp:Repeater ID="rptMainMenu" runat="server">
					<ItemTemplate>
						<div class="col-12 menu-col mb-3">
							<a href='<%# Eval("Url") %>' class="card-menu">
								<div class="card-menu-icon">
									<i class='bi <%# Eval("WebImageKey") %>'></i>
								</div>

								<div class="card-menu-text">
									<div class="title"><%# Eval("MenuName") %></div>
								</div>

								<div class="card-menu-arrow">
									<i class="bi bi-chevron-right"></i>
								</div>
							</a>
						</div>
					</ItemTemplate>
				</asp:Repeater>
			</div>

		</div>

			</div>

	</form>

	<!-- * App Capsule -->


	<!-- App Bottom Menu -->
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
		<uc:SideMenu ID="SideMenu1" runat="server" />
	<!-- * App Sidebar -->

	<!-- ============== Js Files ==============  -->
	<!-- Bootstrap -->
	<script src="../Scripts/jquery.min.js"></script>
	<script src="assets/js/lib/bootstrap.min.js"></script>
	<!-- Ionicons -->
	<script type="module" src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js"></script>
	<!-- Splide -->
	<script src="assets/js/plugins/splide/splide.min.js"></script>
	<!-- ProgressBar js -->
	<script src="assets/js/plugins/progressbar-js/progressbar.min.js"></script>
	<!-- Base Js File -->
	<script src="assets/js/base.js"></script>

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
</body>
</html>
