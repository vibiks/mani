
$("#resetpassword").click(function () {
    $("#newpassword").val('');
    $("#confirmpassword").val('');
})

$("#updatepassword").click(function () {
    if ($("#newpassword").val() != $("#confirmpassword").val())
        alert("Passowrd not match!!")
    else {
        $("#new_pwd").val($("#confirmpassword").val());
        $("#password").val($("#confirmpassword").val());
        $("#pwd_reset").modal('hide');
        return false;
    }
})

$('#upload').click(function () {

    // Checking whether FormData is available in browser  
    if (window.FormData !== undefined) {

        var fileUpload = $("#imagefile").get(0);
        var files = fileUpload.files;

        // Create FormData object  
        var fileData = new FormData();

        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $.ajax({
            url: '/Dashboard/UploadFiles',
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (result) {
                alert(result);
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }
}); 



$("#upload1").click(function () {
    var filename = $("#imagefile").get(0);

    var objRequest = { userid: '100', username:'mani' };
    $.ajax({
        url: '/Dashboard/saveprofile',
        type: 'POST',
        data: JSON.stringify(objRequest),
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
})

$(document).ready(function () {
    // Prepare the preview for profile picture
    $("#profileimagefile").change(function () {
        readURL(this);
    });
});
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#profilepreview').attr('src', e.target.result).fadeIn('slow');
            if (window.FormData !== undefined) {

                var fileUpload = $("#profileimagefile").get(0);
                var files = fileUpload.files;

                // Create FormData object  
                var fileData = new FormData();

                // Looping over all files and add it to FormData object  
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }

                $.ajax({
                    url: '/Dashboard/UploadFiles',
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: fileData,
                    success: function (result) {
                        //alert(result);
                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
            } else {
                alert("FormData is not supported.");
            }
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$("#save1").click(function () {
    var userid = $("#hiddenUserid").val();
    var email = $("#hiddenEmailId").val();
    var phone = $("#hiddenPhone").val();
    var username = $("#hiddenName").val();
    var pwd = $("#new_pwd").val();
    var companyname = $("#companyname").val();
    var gst = $("#gst").val();
    var accname = $("#accname").val();
    var accno = $("#accno").val();
    var ifsc = $("#ifsc").val();
    var branch = $("#branch").val();
    var logo = $("#file").val().split('\\').pop();;
    var profilepic = "";
    var warehousename = $('#warehousename').val();
    var address = $('#address').val();
    var lankmark = $('#lankmark').val();
    var pincode = $('#pincode').val();
    var city = $('#city').val();
    var state = $('#state').val();
    var contactno = $('#contactno').val();

    //var inputdata = { Action: "Save", UserName: username, Email: email, Phone: phone, Password: pwd, CompanyName: companyname, GST: gst, Accname: accname, Accno: accno, IFSC: ifsc, Branch: branch, Logo: logo, ProfilePic: profilepic, WareHouseName: warehousename, Address: address, LandMark: lankmark, PinCode: pincode, City: city, State: state, ContactNo: contactno };
    //var objRequest = { RequestType: "SaveProfile", RequestInput: inputdata, ResponseType: 'JSON' };
    //queryJsonData("/ProfileManage/profile_save", "POST", fncCallbackSaveProfile, objRequest);


   if (companyname != "") {
        var data = "";
       $.ajax({
           url: '/ProfileManage/profile_save?userid=' + userid + '&username=' + username + '&email=' + email + '&phone=' + phone + '&pwd=' + pwd + ' &companyname=' + companyname + ' &gst=' + gst + ' &accname=' + accname + ' &accno=' + accno + ' &ifsc=' + ifsc + ' &branch=' + branch + ' &warehousename=' + warehousename + ' &address=' + address + ' &lankmark=' + lankmark + ' &pincode=' + pincode + ' &city=' + city + ' &state=' + state + ' &contactno=' + contactno + ' &logo=' + logo,
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


var fncCallbackSaveProfile = function (data, msg) {
    if (msg = "Success") {
        
    }
}