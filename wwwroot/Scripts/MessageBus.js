(function () {
    function poll(callback) {
        $.ajax({
            'url': '/messages/poll',
        }).done(function (result) {
            callback(result);
            setTimeout(function () {
                poll(callback);
            }, 1000);
        });
    }

    //For tracking messages on this client
    var currentMessagesGuids = [];

    function start(callback) {
        poll(function (messages) {
            for (var i in messages) {
                var exists = currentMessages.some(function (msgGuid) {
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
        messages: function () { return currentMessages; },
        start: start
    }
})();