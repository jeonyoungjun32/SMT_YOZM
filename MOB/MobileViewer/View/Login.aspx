<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MobileViewer.View.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport"
        content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, viewport-fit=cover" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="theme-color" content="#000000" />
    <title>로그인</title>
    <meta name="description" content="로그인" />
    <meta name="keywords" content="bootstrap 5, mobile template, cordova, phonegap, mobile, html" />
    <link rel="icon" type="image/png" href="assets/img/Slogo.png" sizes="32x32" />
    <link rel="apple-touch-icon" sizes="180x180" href="assets/img/icon/192x192.png" />
    <link rel="stylesheet" href="assets/css/style.css" />

    <script type="text/javascript">
        function OnLoginClick() {
            if (txtID.value == "") {
                alert("아이디를 입력하세요");
                return;
            }
            if (txtPW.value == "") {
                alert("비밀번호를 입력하세요");
                return;
            }

            cbLogin.PerformCallback(txtID.value + "|" + txtPW.value);
        }

        function cbLogin_EndCallback(s, e) {
            if (s.cpLogin) {
                if (s.cpLogin == "true") {
                    if ($("#chkRemember").is(":checked")) {
                        setCookie("ksm_loginid", txtID.value, 7);
                    };

                    delete s.cpLogin;
                    window.location = "Main.aspx";
                }
                else {
                    delete s.cpLogin;
                }

            };

            if (s.cpMsg) {
                alert(s.cpMsg);
                delete s.cpMsg;
            };
        }



    </script>


</head>
<body class="bg-white">

    <!-- loader -->
    <div id="loader">
        <div class="spinner-border text-primary" role="status"></div>
    </div>
    <!-- * loader -->


    <!-- App Capsule -->
    <div id="appCapsule" class="pt-0 d-flex align-items-center justify-content-center" style="min-height:100vh;">
    <div class="login-form w-100">
            <div class="section mt-5 mb-5">
                <img src="../Images/YOZM_Logo_Mint.png" alt="image" class="form-image" style="width:250px; height:auto;"/>
            </div>
            <div class="section mt-1">         
               <h5 style="color:#999;">로그인 정보를 입력해 주세요.</h5>
            </div>
            <div class="section mt-1 mb-5">
                    <div class="form-group boxed">
                        <div class="input-wrapper">
                            <input type="text" class="form-control" id="txtID" placeholder="아이디를 입력하세요" />
                            <i class="clear-input">
                                <ion-icon name="close-circle"></ion-icon>
                            </i>
                        </div>
                    </div>

                    <div class="form-group boxed">
                        <div class="input-wrapper">
                            <input type="password" class="form-control" id="txtPW" placeholder="비밀번호를 입력하세요." autocomplete="off" />
                            <i class="clear-input">
                                <ion-icon name="close-circle"></ion-icon>
                            </i>
                        </div>
                    </div>

                    <div class="form-check mt-2 d-flex">
                        <input type="checkbox" class="form-check-input" id="chkRemember" />
                        <label class="form-check-label" for="chkRemember">아이디 기억하기</label>
                    </div>

                    <div class="form-button-group  mb-3">
                        <button type="button" onclick="OnLoginClick();" class="btn btn-primary btn-block btn-lg">로그인</button>
                    </div>
            </div>
        </div>
        <form id="form1" runat="server">
            <dx:ASPxCallback ID="cbLogin" runat="server" ClientInstanceName="cbLogin" OnCallback="cbLogin_Callback">
                <ClientSideEvents EndCallback="cbLogin_EndCallback"></ClientSideEvents>
            </dx:ASPxCallback>
        </form>

    </div>
    <!-- * App Capsule -->

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
        function setCookie(cookie_name, value, days) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + days);
            // 설정 일수만큼 현재시간에 만료값으로 지정

            var cookie_value = escape(value) + ((days == null) ? '' : ';    expires=' + exdate.toUTCString());
            document.cookie = cookie_name + '=' + cookie_value;
        }

        function getCookie(cookie_name) {
            var x, y;
            var val = document.cookie.split(';');

            for (var i = 0; i < val.length; i++) {
                x = val[i].substr(0, val[i].indexOf('='));
                y = val[i].substr(val[i].indexOf('=') + 1);
                x = x.replace(/^\s+|\s+$/g, ''); // 앞과 뒤의 공백 제거하기
                if (x == cookie_name) {
                    return unescape(y); // unescape로 디코딩 후 값 리턴
                }
            }
        }


        $(window).on('load', function () {
            if (typeof (getCookie("ksm_loginid")) != "undefined") {
                txtID.value = getCookie("ksm_loginid");
            }
        })

    </script>

</body>
</html>
