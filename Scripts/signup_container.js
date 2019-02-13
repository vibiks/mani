$(document).ready(function () {
    $(".sales_submit").click(function () {

        var s_name = $("#s_username").val();

        var s_email = $("#s_email").val();

        var s_mobile = $("#s_mobile").val();

        var s_pincode = $("#s_pincode").val();

        $(".lead").LoadingOverlay("show");

        $.post("http://www.fetchto.com/Home/contact_sales", { name: s_name, email: s_email, mobile: s_mobile, pincode: s_pincode }, function (data) {

            $(".msg").html(data);

            $(".lead").LoadingOverlay("hide");
        })
    })

    //shipping request
    $(".ship_request").click(function () {

        var ship_country = $("#ship_country").val();

        var ship_weight = $("#ship_weight").val();

        var ship_product = $("#ship_product").val();

        var ship_email = $("#ship_email").val();

        var ship_mobile = $("#ship_mobile").val();

        $(".ship_request_holder").LoadingOverlay("show");

        $.post("http://www.fetchto.com/Home/ship_request", { country: ship_country, weight: ship_weight, product: ship_product, email: ship_email, mobile: ship_mobile }, function (data) {

            $(".msg").html(data);

            $(".ship_request_holder").LoadingOverlay("hide");
        })
    })



    $("#sign").click(function () {

        var email = $("#email_l").val();

        var pwd = $("#pwd_l").val();

        if (email != "" && pwd != "") {
            //$(".login_form").submit();
            var data = "";
            $.ajax({
                url: '/UserManage/sign_exe?username=' + email + ' &password=' + pwd,
                type: 'POST',
                //data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                error: function (xhr) {
                    alert('Error: ' + xhr.statusText);
                },
                success: function (result) {
                    alert(result);
                    if (result == "Login Success") {
                        location.href = "~/Dashboard/Index";
                    }
                    else {
                        $(".msg").html("Login Failed");
                    }

                },
                async: true,
                processData: false
            });

        } else {

            $("#msg").html("Invalid inputs");

        }
    })

    //get customized quotes

    $("#signup").click(function () {

        var username = $("#username_r").val();

        var email = $("#email").val();

        var phone = $("#mobile").val();

        var pwd = $("#password").val();

        var email_valid = validateEmail(email);

        var confirm_pwd = $("#cpassword").val();

        if (username != "" && email_valid == true && email != "" && phone != "")
        {

            if (pwd == confirm_pwd) {

                $.LoadingOverlay("show");

                $.ajax({
                    url: '/UserManage/Validateuseremail?email=' + email,
                    type: 'POST',
                    //data: JSON.stringify(data),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    error: function (xhr) {
                        alert('Error: ' + xhr.statusText);
                    },
                    success: function (data) {
                        if (data == "true") {
                            $.LoadingOverlay("hide");
                            $("#signup_msg").html("email already exist");

                        }
                        else {
                            if ($('input[name="term"]').is(':checked')) {

                                $.LoadingOverlay("hide");
                                $.ajax({
                                    url: '/UserManage/Usesrregistration?username=' + username + ' &password=' + pwd + ' &email=' + email + ' &mobile=' + phone ,
                                    type: 'POST',
                                    //data: JSON.stringify(data),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    error: function (xhr) {
                                        alert('Error in UserRegistration: ' + xhr.statusText);
                                    },
                                    success: function (data) {
                                            $.LoadingOverlay("hide");
                                            window.location.href = '/Home/index?login=verified';

                                    },
                                    async: true,
                                    processData: false
                                });

                            }
                            else {
                                $.LoadingOverlay("hide");
                                $("#signup_msg").html("Please agree our policy");
                            }
                        }

                    },
                    async: true,
                    processData: false
                });
            }
            else
            {
                 $("#signup_msg").html("password is not match");
            }
        }
        else
        {

            if (email_valid == true)
            {
                $("#signup_msg").html("please fill all the fields");

            }
            else
            {
                $("#signup_msg").html("Enter valid email");
            }
        }

    });

    $(".custom_quote").click(function () {

        var c_name = $("#c_name").val();

        var c_dispatch = $("#c_dispatch").val();

        var c_email = $("#c_email").val();

        var c_mobile = $("#c_mobile").val();

        $(".custom_quote_holder").LoadingOverlay("show");

        $.post("http://www.fetchto.com/Home/custom_quote", { name: c_name, dispatch: c_dispatch, email: c_email, mobile: c_mobile }, function (data) {

            $(".msg").html(data);

            $(".custom_quote_holder").LoadingOverlay("hide");
        })
   
        //$("#signup_block").show();

        //$("#msg_block").hide();
    })
    function validateEmail($email) {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        return emailReg.test($email);
    }
});