
function getRandomClass() {
    var rand = (Math.floor(Math.random() * 10)) % 4;
    switch (rand) {
        case 0:
            return "alert-success";
        case 1:
            return "alert-info";
        case 2:
            return "alert-warning";
        case 3:
            return "alert-danger";
    }
}

function getMessageClass() {
    return "alert alert-dismissable " + getRandomClass();
}

function createMessageDiv(message) {
    var div = $('<div></div>', {
        class: getMessageClass()
    });
    div.append('<button type="button" class="close" data-dismiss="alert">×</button>')
    div.append('<strong>' + message.Sendee +'</strong> ')
    div.append(message.Content);
    $(div).hide();
    
    return div;
}

function showMessage(message) {
    var div = createMessageDiv(message);
    $('#messages').prepend(div);
    $(div).show(1000);
}