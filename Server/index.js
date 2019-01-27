const WebSocketServer = require('websocket').server;
const http = require('http');
const Game = require('./Game/game')
const Room = require('./room')
const Config = require('./config')

const game = new Game();
const room = new Room(game);

setInterval(() => {
    room.tick();
}, Config.tickInterval);

const server = http.createServer(function(request, response) {
    console.log((new Date()) + ' Received request for ' + request.url);
    response.writeHead(404);
    response.end();
});

server.listen(Config.port, function() {
    console.log((new Date()) + ' Server is listening on port ' + Config.port);
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
    console.log("Connecting");

    if (!originIsAllowed(request.origin)) {
      request.reject();
      console.log((new Date()) + ' Connection from origin ' + request.origin + ' rejected.');
      return;
    }

    let connection = {};
    try{
        connection = request.accept(Config.protocol, request.origin);
    }
    catch(err){
        console.log((new Date()) + ' Connection accepting failed.');
        console.log(err);
        return;
    }
    console.log((new Date()) + ' Connection accepted.');

    const human = room.join((obj) => connection.sendUTF(JSON.stringify(obj)))

    connection.on('message', function(message) {
        const isUtf = message.type === 'utf8';
        const isBinary = message.type === 'binary';

        if (isUtf) {
            console.log('Received UTF Message: ' + message.utf8Data);
            try {
                const obj = JSON.parse(message.utf8Data);
                human.receiveMessage(obj);
            } catch (error) {
                console.error('Error parsing message', error, message.utf8Data);
            }
        } else if (isBinary) {
            console.warn('Received Binary Message of ' + message.binaryData.length + ' bytes');
        } else {
            console.error("Unsupported message type", message.type);
        }
    });

    connection.on('close', function(reasonCode, description) {
        console.log((new Date()) + ' Peer ' + connection.remoteAddress + ' disconnected.');
        connections.splice(connections.indexOf(connection));
        room.leave(human);
    });
});
