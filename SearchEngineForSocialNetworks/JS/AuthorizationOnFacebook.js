var loginInput = document.getElementById('email');
var passwordInput = document.getElementById('pass');
if (loginInput && passwordInput) {
    loginInput.value = '{{login}}';
    passwordInput.value = '{{password}}';
    var loginForm = document.getElementById('login_form');
    if (loginForm) {
        loginForm.submit();
    }
}