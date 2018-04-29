$(document).ready(function () {

    var websocket;

    $('#userConnectionForm').on('submit', function (e) {
        e.preventDefault();
        var username = $('#userNameToBeEnteredBox').val();

        var uri = 'ws://localhost:60393/api/Messages' + '?username=' + username;
        websocket = new WebSocket(uri);

        $('#userConnectionForm').hide();
         $('#sendButton').on('click', function () {
        websocket.send($('#inputbox').val());
    });
    

    websocket.onopen = function () {
        $('#messages').prepend('Connected.');
    };

    websocket.onerror = function (event) {
        //if the websocket cannot connect
        $('#messages').prepend('ERROR!');
    };

    websocket.onmessage = function (event) {
        $('#messages').prepend('<div>' + event.data + '</div>');
    };
    });

});
