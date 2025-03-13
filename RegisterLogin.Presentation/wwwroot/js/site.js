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
function verifyCodePost() {
    var formData = $("#verifyCodeForm").serialize();
    $.ajax({
        url: "/Login/VerifyCode",
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
function logoutform() {
    $.ajax({
        url: "/Login/LoadLogout",
        type: "Get",
        data: {}
    }).done(function (result) {
        $('#mymodal').modal('show');
        $('#mymodallable').html('آیا برای خروج اطمینان دارید ؟');
        $('#bodymodal').html(result);
    });
}
function logoutPost() {
    $.ajax({
        url: "/Login/Logout",
        type: "POST",
        data: {},
    }).done(function (result) {
        console.log(result);
        $('#mymodal').modal('hide');
        window.location.href = '/Login/Login';
    });
}

function moveFocus(current, nextId) {
    if (current.value.length == current.maxLength) {
        document.getElementById(nextId).focus();
    }
}

function convertPersianToEnglishNumbers(input) {
    const persianNumbers = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
    const englishNumbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

    // تبدیل هر عدد فارسی به معادل انگلیسی آن
    for (let i = 0; i < persianNumbers.length; i++) {
        input = input.replace(new RegExp(persianNumbers[i], 'g'), englishNumbers[i]);
    }

    return input;
}
function submitForm() {
    // ارسال کد تایید به سرور یا بررسی فرم
    var code = '';
    for (let i = 5; i >= 1; i--) {
        code += document.getElementById('code' + i).value;
    }
    code = convertPersianToEnglishNumbers(code);
    console.log('Verification code: ' + code);
    // در اینجا می‌توانید کد تایید را به سرور ارسال کنید
    document.getElementById('VerificationCode').value = code;
}
function validateNumber(event) {
    // فقط اعداد مجاز هستند
    var keyCode = event.keyCode || event.which;
    var key = String.fromCharCode(keyCode);

    // اگر کلید وارد شده عدد نیست، از وارد کردن جلوگیری کن
    if (!/^[0-9]$/.test(key)) {
        event.preventDefault(); // جلوگیری از وارد کردن غیر عدد
    }
}