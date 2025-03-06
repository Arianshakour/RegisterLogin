function forgotPass() {
    $.ajax({
        url: "/Login/ForgotPassword",
        type: "Get",
        data: {}
    }).done(function (result) {
        $('#Container').html(result);
        $('#Container').show();
        $('#logincontainer').hide();
    });
}
function forgotPassPost() {
    var formData = $("#ForgotPasswordForm").serialize();
    $.ajax({
        url: "/Login/ForgotPassword",
        type: "POST",
        data: formData,
    }).done(function (result) {
        console.log(result);
        if (result.success) {
            $('#Container').empty();
            var Content = result.html.replace(/(\r\n|\n|\r)/g, "");
            $('#Container').html(Content);
            $('#Container').show();
            $('#resetEmail').val(result.email);
        } else {
            alert(result.message);
        }
    });
}
function resetPasswordPost() {
    var formData = $("#resetPasswordForm").serialize();
    $.ajax({
        url: "/Login/ResetPassword",
        type: "POST",
        data: formData,
    }).done(function (result) {
        console.log(result);
        if (result.success) {
            //$('#Container').empty();
            //var Content = result.html.replace(/(\r\n|\n|\r)/g, "");
            //$('#Container').html(Content);
            //$('#Container').show();
            alert('رمز عبور با موفقیت تغییر یافت');
            var Content = result.html.replace(/(\r\n|\n|\r)/g, "");
            $("main").empty();
            $("main").html(Content);
        } else {
            alert(result.message);
        }
    });
}

function registerUserPost() {
    var formData = $("#registerForm").serialize();
    $.ajax({
        url: "/Login/Register",
        type: "POST",
        data: formData,
    }).done(function (result) {
        console.log(result);
        if (result.success) {
            $('#registercontainerr').empty();
            console.log(result);
            var Content = result.html.replace(/(\r\n|\n|\r)/g, "");
            $('#registercontainerr').html(Content);
            $('#registercontainerr').show();
        } else {
            $('#registercontainerr').html(result.view);
            alert(result.message);
        }
    });
}
//2ta func paein alan dg bela estefade shodan
function registerUser() {
    $.ajax({
        url: "/Login/Register",
        type: "Get",
        data: {}
    }).done(function (result) {
        $("main").empty();
        $("main").html(result);
        history.replaceState(null, null, '/Login/Register');
    });
}
function loginUser() {
    $.ajax({
        url: "/Login/Login",
        type: "Get",
        data: {}
    }).done(function (result) {
        //$('#Container').empty();
        //$('#yy').html(result);
        //$('#yy').show();
        $("main").empty();
        $("main").html(result);
        history.replaceState(null, null, '/Login/Login');
    });
}
function loginUserPost() {
    var formData = $("#loginForm").serialize();
    $.ajax({
        url: "/Login/Login",
        type: "POST",
        data: formData,
    }).done(function (result) {
        console.log(result);
        if (result.isGrid == 1) {
            $('#yy').empty();
            $('#yy').html(result.view);
            $('#Container').remove();
            alert(result.message);
        } else {
            window.location.href = '/Home/Index';
        }
    });
}
//$(document).ready(function () {
//    $("#showLoginBtn").click(function () {
//        $.ajax({
//            url: '/Login/Login',
//            type: 'GET',
//            success: function (result) {
//                $("main").html(result);
//            }
//        });
//    });
//});
function loadLoginPage() {
    $.ajax({
        url: '/Login/Login',
        type: 'GET',
        success: function (response) {
            $("main").empty();
            $("main").html(response);
            history.replaceState(null, null, '/Login/Login');
        }
    });
}
function loadRegisterPage() {
    $.ajax({
        url: '/Login/Register',
        type: 'GET',
        success: function (response) {
            $("main").empty();
            $("main").html(response);
            history.replaceState(null, null, '/Login/Register');
        }
    });
}
function loadChangePasswordPage() {
    $.ajax({
        url: '/Login/ChangePassword',
        type: 'GET',
        success: function (response) {
            $("main").empty();
            $("main").html(response);
            history.replaceState(null, null, '/Login/ChangePassword');
        }
    });
}
function changePasswordPost() {
    var formData = $("#ChangePasswordForm").serialize();
    $.ajax({
        url: "/Login/ChangePassword",
        type: "POST",
        data: formData,
    }).done(function (result) {
        console.log(result);
        if (result.isGrid == 1) {
            $("main").empty();
            $("main").html(result.view);
        } else {
            alert('عملیات با موفقیت انجام گردید');
            window.location.href = '/Home/Index';
        }
    });
}
