/*
Really simple script for polling and tracking messages received by this client.
*/
(function () {    
    //For tracking messages on this client
    var currentMessagesGuids = [];
    function lastIdOrEmptyString(){
        var last = currentMessagesGuids[currentMessagesGuids.length - 1];
        return last === undefined ? '' : last;
    }

    function poll(callback) {
        $.ajax({
            'url': '/messages/' + lastIdOrEmptyString(),
        }).done(function (result) {
            callback(result);
            setTimeout(function () {
                poll(callback);
            }, 1000);
        });
    }


    function start(callback) {
        poll(function (messages) {
            for (var i in messages) {
                var exists = currentMessagesGuids.some(function (msgGuid) {
                    return msgGuid === messages[i].Guid;
                });

                if (!exists) {
                    currentMessagesGuids.push(messages[i].Guid);
                    if (callback)
                        callback(messages[i]);
                }
            }
        });
    }

    window.MessageBus = {
        start: start
    }
})();