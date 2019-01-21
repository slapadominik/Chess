import {
  HubConnectionBuilder
} from '@aspnet/signalr';

export function buildConnection() {
  return new HubConnectionBuilder()
    .withUrl(`https://localhost:44384/Hubs/Games`)
    .build();
}

export function startConnection(hubConnection) {
  hubConnection
    .start()
    .then(() => console.log('Connection started'))
    .catch((err) => console.log('Error while establishing connection ' + err));
}

export function receiveUserInfo(hubConnection, callback) {
  hubConnection.on('UserCreated_Caller', callback);
}

export function receiveUsersInfo(hubConnection, callback) {
  hubConnection.on('UserCreated_All', callback);
}

export function onDisconnection(hubConnection, callback) {
  hubConnection.on('UserDisconnected', callback);
}