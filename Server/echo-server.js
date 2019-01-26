var WebSocketServer = require('websocket').server;
var http = require('http');

// var rooms = {};
// function roomList(){
//     var roomList = {};
//     for (var roomName in rooms) {
//         roomList[roomName] = rooms[roomName].connections.length;
//     }
//     return roomList;
// }

const connections = [];

var server = http.createServer(function(request, response) {
    // if (request.url == "/room_list.json") {
    //     response.setHeader('Content-Type', 'application/json');
    //     response.end(JSON.stringify(roomList()));
    //     return;
    // }

    console.log((new Date()) + ' Received request for ' + request.url);
    response.writeHead(404);
    response.end();
});

server.listen(2048, function() {
    console.log((new Date()) + ' Server is listening on port 2048');
});

wsServer = new WebSocketServer({
    httpServer: server,
    autoAcceptConnections: false
});

function originIsAllowed(origin) {
  return true;
}

wsServer.on('error', function(evt) { console.error("Received error", evt) });

wsServer.on('request', function(request) {
    console.log("Connecting " +  request.remoteAddress);

    if (!originIsAllowed(request.origin)) {
      request.reject();
      console.log((new Date()) + ' Connection from origin ' + request.origin + ' rejected.');
      return;
    }

    // console.log(request);
    // var roomName = request.httpRequest.url.substring(1);
    // var newRoom = false;

    // if (rooms[roomName] == null) {
    //     var room = {};
    //     room.connections = [];
    //     newRoom = true;
    //     rooms[roomName] = room;
    // } else {
    //     if (rooms[roomName].connections.length >= 2) {
    //         request.reject();
    //         console.log((new Date()) + ' Connection to ' + roomName + ' rejected. Room is full.');
    //         return;
    //     }
    // }

    var connection = {};
    try{
        connection = request.accept('echo-protocol', request.origin);
    }
    catch(err){
        console.log((new Date()) + ' Connection accepting failed.');
        return;
    }
    console.log((new Date()) + ' Connection accepted.');

    connections.push(connection);
    // rooms[roomName].connections.push(connection);

    connection.on('message', function(message) {
        const isUtf = message.type === 'utf8';
        const isBinary = message.type === 'binary';

        if (isUtf) {
            console.log('Received UTF Message: ' + message.utf8Data);
        } else if (isBinary) {
            console.log('Received Binary Message of ' + message.binaryData.length + ' bytes');
        } else {
            console.error("Unsupported message type", message.type);
        }

        connections.forEach(con => {
            if (con == connection) {
                // Skipping sending to self
                return;
            }

            if (isUtf) {
                con.sendUTF(message.utf8Data);
            } else if (isBinary) {
                con.sendBytes(message.binaryData);
            }

        });
    });

    connection.on('close', function(reasonCode, description) {
        console.log((new Date()) + ' Peer ' + connection.remoteAddress + ' disconnected.');
        connections.splice(connections.indexOf(connection));
    });
});