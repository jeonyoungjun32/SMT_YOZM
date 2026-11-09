<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebViewer.View.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <title>로그인</title>
    <link rel="shortcut icon" href="../Images/ksm_icon.ico" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/style.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .input-placeholder {
            color:#6c757d;
            opacity:1
        }
    </style>
    

    <!-- / JAVASCRIPT -->

    <script type="text/javascript">
        function OnLoginClick(s, e) {
            if (ASPxClientEdit.ValidateGroup('Login')) {
                cbPnlLogin.PerformCallback();
            };
        }

        function CbPnlLogin_Init(s, e) {
            cboUserId.SetValue(getCookie("ksm_loginid"));
        }

        function CbPnlLogin_EndCallback(s, e) {
            if (s.cpLogin) {
                if (s.cpLogin == "true") {
                    if ($("#chkRemember").is(":checked")) {
                        setCookie("ksm_loginid", cboUserId.GetValue(), 7);
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
<body>
    <!-- Begin page -->
    <div class="accountbg"></div>
    <div class="home-btn d-none d-sm-block">
        <a href="../index.html" class="text-white"><i class="fas fa-home h2"></i></a>
    </div>
    <div class="wrapper-page">
        <div class="card card-pages shadow-none">

            <div class="card-body">
                <div class="text-center m-t-0 m-b-15">
                    <a href="index.html" class="logo logo-admin">
                        <img src="../Images/login_logo.png" alt="" height="65" /></a>
                </div>
                <h2 class="font-600 text-center" style="font-family: 'Segoe UI'">IMAGE Viewer</h2>
                <form id="form2" runat="server">
                    <dx:BootstrapCallbackPanel ID="CbPnlLogin" runat="server" ClientInstanceName="cbPnlLogin" OnCallback="CbPnlLogin_Callback">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <div class="form-group">
                                    <div class="col-12">
                                        <label>아이디</label>
                                        <dx:BootstrapComboBox ID="cboUserId" runat="server" ClientIDMode="Static" ClientInstanceName="cboUserId" NullText="아이디" DataSourceID="employeeSqlDataSource" ValueField="EmployeeId" TextField="EmployeeName" EnableMultiColumn="True" DropDownRows="5">
                                            <CssClasses NullText="input-placeholder" />
                                            <Fields>
                                                <dx:BootstrapListBoxField FieldName="EmployeeId">
                                                </dx:BootstrapListBoxField>
                                                <dx:BootstrapListBoxField FieldName="EmployeeName">
                                                </dx:BootstrapListBoxField>
                                            </Fields>
                                            <ValidationSettings ValidationGroup="Login" ErrorDisplayMode="ImageWithText">
                                                <RequiredField IsRequired="true" ErrorText="아이디를 입력하세요." />
                                            </ValidationSettings>
                                        </dx:BootstrapComboBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-12">
                                        <label>비밀번호</label>
                                        <dx:BootstrapTextBox runat="server" Password="True" ID="txtPassword" ClientIDMode="Static" ClientInstanceName="txtPassword" NullText="비밀번호">
                                            <CssClasses NullText="input-placeholder" />
                                            <ValidationSettings ValidationGroup="Login" ErrorDisplayMode="ImageWithText">
                                                <RequiredField IsRequired="true" ErrorText="비밀번호를 입력하세요." />
                                            </ValidationSettings>
                                        </dx:BootstrapTextBox>
                                    </div>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                        <ClientSideEvents EndCallback="CbPnlLogin_EndCallback" Init="CbPnlLogin_Init" />
                    </dx:BootstrapCallbackPanel>
                    <div class="form-group">
                        <div class="col-12">
                            <div class="checkbox checkbox-primary">
                                <div class="custom-control custom-checkbox">
                                    <input type="checkbox" class="custom-control-input" id="chkRemember" />
                                    <label class="custom-control-label" for="chkRemember">기억하기</label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-group text-center m-t-20">
                        <div class="col-12">
                            <dx:BootstrapButton runat="server" AutoPostBack="False" Text="로그인" ID="btnLogin" ClientInstanceName="btnLogin">
                                <CssClasses Control="btn btn-primary btn-block btn-lg waves-effect waves-light" />
                                <SettingsBootstrap RenderOption="Primary" />
                                <ClientSideEvents Click="OnLoginClick" />
                            </dx:BootstrapButton>
                        </div>
                    </div>

                    <div class="form-group row m-t-30 m-b-0">
                        <div class="col-sm-12">
                            <a href="RecoverPassword.aspx" style="font-family:Scriptina" class="text-muted"><i class="fa fa-lock m-r-5"></i>비밀번호 변경</a>
                        </div>
                    </div>
                    <asp:SqlDataSource ID="employeeSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:KSMImageConnectionString %>"
                        SelectCommand="Select EmployeeId, EmployeeName From CodeUser Where CorpId = @pCorpId">
                        <SelectParameters>
                            <asp:SessionParameter Name="pCorpId" SessionField="CorpId" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </form>
            </div>

        </div>
    </div>
    <!-- END wrapper -->

    <!-- jQuery  -->
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/js/bootstrap.bundle.min.js"></script>
    <script src="assets/js/jquery.slimscroll.js"></script>
    <script src="assets/js/waves.min.js"></script>


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

    </script>
</body>
</html>

