var ResetPasswordData = {
    "Email": "",
    "Password": "",
    "ConfirmPassword": "",
    "Code": ""
};


$(function () {
    $("#submit").click(function (event) {
        event.preventDefault();

        if ($('#resetPasswordForm').valid()) {

            var formData = $('form').serializeArray().reduce(function (obj, item) {
                obj[item.name] = item.value;
                return obj;

            }, {});
            /* Validating formData */
            if (validateControls(formData)) {
                ResetPasswordData.Email = formData.Email;
                ResetPasswordData.Password = formData.Password;
                ResetPasswordData.ConfirmPassword = formData.ConfirmPassword;
                ResetPasswordData.Code = formData.Code;

                var action = $('form').attr("action");

                $.ajax({
                    type: "POST",
                    url: action,
                    contentType: "application/json",
                    async: true,
                    headers: {
                        'X-XSRF-TOKEN': getCookie("XSRF-TOKEN"),
                        RequestVerificationToken:
                            $("[name='__RequestVerificationToken']").val()
                    },
                    data: JSON.stringify(ResetPasswordData),
                    success: function (result) {
                        Swal.fire({
                            title: 'PasswordReset',
                            html: "<div class='card'>" +
                                "<div class='box'>" +
                                //"<div class='img'>" +
                                "<div>" +
                                "<h1><b>Windo</b></h1>"+
                                "</div>" +
                                "<h2><span class='bold'> הסיסמה שלך עודכנה<br></span>את יכולה להיכנס איתה לאתר <br> כבר ברגע זה</h2>" +     
                                "<a class='btn btn-primary btn-block' href='/home'>יופי, זה בדיוק מה שאני רוצה לעשות</a>" +
                                "</div>" +
                                "</div>",
                            background: 'none',
                            showConfirmButton: false
                        })
                    },
                    error: function (error) {
                        Swal.fire({
                            type: 'error',
                            title: '...אופס',
                            text: 'לא ניתן להשלים את הבקשה. בבקשה נסה שוב מאוחר יותר.'
                        })
                    }
                });
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: '...אופס',
                    text: 'בבקשה תקן את השגיאה ונסה מאוחר שוב'
                })
            }
        }
    });
});

function validateControls(formData) {
    var formIsValid = true;

    if (formData.Email === "") { formIsValid = false; }
    if (formData.Password === "") { formIsValid = false; }
    if (formData.Code === "") { formIsValid = false; }
    if (formData.__RequestVerificationToken === "") { formIsValid = false; }

    if (formData.Email !== "") {
        var reEmail = /^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$/;
        if (!reEmail.test(formData.Email)) {
            $("#emailValidation").text('אנא הכנס כתובת מייל תקינה');
            formIsValid = false;
        }
    }
    if (formData.Password !== "") {
        var rePassword = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$/;

        if (!rePassword.test(formData.Password)) {
            $("#passValidation").text('אנא הכנס סיסמה תקינה');
        }
        if (formData.Password !== formData.ConfirmPassword) {
            formIsValid = false;
        }
    }
    return formIsValid;
}

/*
* ^	The password string will start this way
* (?=.*[a-z])	The string must contain at least 1 lowercase alphabetical character
* (?=.*[A-Z])	The string must contain at least 1 uppercase alphabetical character
* (?=.*[0-9])	The string must contain at least 1 numeric character
* (?=.[!@#\$%\^&])	The string must contain at least one special character, but we are escaping reserved RegEx characters to avoid conflict
* (?=.{6,})	The string must be eight characters or longer
*/

/* EXTENSION METHOD TO GET BROWSER COOKIE */
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}