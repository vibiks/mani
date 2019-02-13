function queryJsonData(url, type, callback, data) {
    $.ajax({
        url: url,
        type: 'POST',
        data: JSON.stringify(data),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        error: function (xhr) {
            alert('Error: ' + xhr.statusText);
        },
        success: callback,
        async: true,
        processData: false
    });
}