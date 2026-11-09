<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionError.aspx.cs" Inherits="MobileViewer.View.SessionError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <title>세션 만료</title>

    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="framestyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="assets/css/style.css" />

    <style>
        html, body {
            width: 100%;
            height: 100%;
            margin: 0;
        }

        body {
            background: #f5f7fb;
        }

        .account-pages {
            width: 100%;
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .card {
            border: 0;
            border-radius: 12px;
        }

        .card-body,
        .card-body h3,
        .card-body p,
        .card-body div {
            text-align: center !important;
        }

        .session-icon {
            font-size: 72px;
            color: #f0ad4e;
            display: block;
            margin: 0 auto 20px;
        }

        .btn-primary {
            border-radius: 30px;
            padding: 10px 28px;
        }
    </style>

    <script type="text/javascript">
        function GoToLogin() {
            parent.window.location.href = 'Login.aspx';
        }
    </script>
</head>

<body>

<div class="account-pages">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-5">

                <div class="card shadow-lg">
                    <div class="card-body text-center p-5">

                        <i class="mdi mdi-timer-off session-icon mb-4"></i>

                        <h3 class="mb-3">로그인 세션이 종료되었습니다.</h3>

                        <p class="text-muted mb-5">
                            장시간 사용이 없어 자동으로 로그아웃되었습니다.<br />
                            다시 로그인하여 서비스를 계속 이용해 주세요.
                        </p>

                        <button type="button"
                                class="btn btn-primary btn-lg btn-block"
                                onclick="GoToLogin();">
                            <i class="mdi mdi-login-variant mr-2"></i>
                            로그인 화면으로 이동
                        </button>

                    </div>
                </div>

            </div>
        </div>
    </div>
</div>

</body>
</html>